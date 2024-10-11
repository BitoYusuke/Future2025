using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class KAPIBARA_AI : MonoBehaviour
{

    // --- AIState�p�ϐ�
    public enum AIState { Idle, Walking, Eating } // AIState::AI�̏�Ԃ��`
                                                  // Idle::�ҋ@
                                                  // Walking::����
                                                  // Eating::�H�ׂ�

    public AIState currentState = AIState.Idle;   // �����l��ҋ@�X�e�[�g�ɐݒ�
    private bool switchAction = false;            // �X�e�[�g�؂�ւ��p�t���O
    private float actionTimer = 0;                // ���̍s���܂ł̑ҋ@����

    // --- AI�ړ��Ɋւ���ϐ�
    public float walkingSpeed = 3.5f;             // �����X�s�[�h
    public float walkingProbability = 0.5f;       // AI�������_���ɕ�������
    public float minRange = 3.0f;                 // �ړ��͈͂̍ŏ��l
    public float maxRange = 7.0f;                 // �ړ��͈͂̍ő�l
    public float minIdleTime = 0.1f;              // Idle��Ԃ̍ŏ�����
    public float maxIdleTime = 2.0f;              // Idle��Ԃ̍ő厞��
    public float minEatingTime = 0.1f;            // Eating��Ԃ̍ŏ�����
    public float maxEatingTime = 2.0f;            // Eating��Ԃ̍ő厞��
    public float rotationSpeed = 0.5f;            // ��]���x
    public float actionInterval = 5f;              // �ړI�n��ݒ肷��Ԋu

    public Animator animator;                     // Animotor�p�ϐ�
    private NavMeshAgent agent;                    // �i�r���b�V���p�ϐ�

    // �O���Idle�|�C���g��ێ����邽�߂̃��X�g
    List<Vector3> previousIdlePoints = new List<Vector3>();
    private Vector3 currentDestination;            // ���݂̖ړI�n
    // ��������Prefab
    private List<GameObject> spawnedObjects = new List<GameObject>(); // ���������I�u�W�F�N�g��ێ����郊�X�g
    public GameObject objectToSpawn;               // ��������I�u�W�F�N�g��Prefab

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 0;
        agent.autoBraking = true;
        agent.updateRotation = false;

        // AI�̏�����Ԃ�Walking�ɐݒ�
        currentState = AIState.Walking;  // �����X�e�[�g�������I��Walking��
        actionTimer = actionInterval;    // �ŏ��̍s���^�C�}�[�����Z�b�g
        SwitchAnimationState(currentState); // �A�j���[�V������Walking�ɐݒ�

        // �ŏ��̖ړI�n��ݒ�
        SetNewDestination();
        // �ŏ��̃A�j���[�V�����ݒ�i�������s�����Ă���\���j
        if (currentState == AIState.Walking)
        {
            animator.SetBool("isWalking", true); // �ŏ��̈ړ����ɃA�j���[�V������ݒ�
        }
        // �ړI�n�ɃI�u�W�F�N�g�𐶐�
        SpawnObjectAtDestination(currentDestination);
    }

    // Update is called once per frame
    void Update()
    {
        // ���̍s���܂őҋ@
        if (actionTimer > 0)
        {
            actionTimer -= Time.deltaTime;
        }
        else
        {
            switchAction = true;
        }

        if (currentState == AIState.Idle)
        {
            if (switchAction)
            {
                // �����_���Ɂu�H�ׂ�v���u�����v�s����I��
                if (Random.value > walkingProbability)
                {
                    // �H�ׂ�
                    actionTimer = Random.Range(minEatingTime, maxIdleTime);
                    currentState = AIState.Eating;
                }
                else
                {
                    // ���� //agent.destination = RandomNavSphere(transform.position, Random.Range(3, 7));

                    SetNewDestination();            // �V�����ړI�n��ݒ�
                    // �ړI�n�ɃI�u�W�F�N�g�𐶐�
                    SpawnObjectAtDestination(currentDestination);
                    currentState = AIState.Walking; // �X�e�[�g�������ԂɕύX                   
                }

                SwitchAnimationState(currentState);
                // �ߋ��̃A�C�h���ʒu���L�^�i�K�v�ɉ����ĕێ�����j
                previousIdlePoints.Add(transform.position);
                if (previousIdlePoints.Count > 5)
                {
                    previousIdlePoints.RemoveAt(0);
                }

                switchAction = false; // �s�������������̂Ń��Z�b�g
            }
        }
        else if (currentState == AIState.Walking)
        {
            agent.speed = walkingSpeed;
            // ���x�Ɋ�Â��ăA�j���[�V�����𐧌�
            if (agent.velocity.sqrMagnitude > 0.1f) // �������l��I��ŁA�ړ������ǂ����m�F
            {
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
            // �ړI�n�ɓ��B�������ǂ������m�F
            if (DoneReachingDestination())
            {
                currentState = AIState.Idle;
                actionTimer = Random.Range(minIdleTime, maxIdleTime); // ���̍s���܂ł̃^�C�}�[�����Z�b�g
                SwitchAnimationState(currentState);
            }
            else
            {
                // �ړ����̉�]���X���[�Y�ɂ���
                Vector3 direction = (agent.steeringTarget - transform.position).normalized;
                if (direction != Vector3.zero)
                {
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
                }
            }
        }
        else if (currentState == AIState.Eating)
        {
            if (switchAction)
            {
                // �H�ׂ�A�j���[�V�������I��������Idle�ɖ߂�
                if (!animator || animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
                {
                    //agent.destination = RandomNavSphere(transform.position, Random.Range(3, 7));
                    currentState = AIState.Idle;
                    actionTimer = Random.Range(minIdleTime, maxIdleTime);
                    SwitchAnimationState(currentState);
                    //switchAction = false;
                }
            }
        }

        //Debug.Log(currentState);
    }

    void SetNewDestination()
    {
        currentDestination = RandomNavSphere(transform.position, Random.Range(minRange, maxRange));
        agent.destination = currentDestination;
        Debug.Log("New Destination Set: " + currentDestination); // �f�o�b�O�p
        actionTimer = actionInterval; // ���̖ړI�n�ݒ�܂ł̃^�C�}�[�����Z�b�g
    }

    bool DoneReachingDestination()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    // �ړI�n�ɓ��B������I�u�W�F�N�g������
                    foreach (GameObject obj in spawnedObjects)
                    {
                        Destroy(obj); // �I�u�W�F�N�g���폜
                    }
                    spawnedObjects.Clear(); // ���X�g���N���A
                    return true;
                }
            }
        }
        return false;
    }

    void SwitchAnimationState(AIState state)
    {
        // �A�j���[�V��������
        if (animator)
        {
            // ���̃X�e�[�g��S��false�ɂ��āA��Ԃ����Z�b�g����邱�Ƃ�ۏ�
            animator.SetBool("isWalking", false);
            animator.SetBool("isEating", false);

            // ���݂̃X�e�[�g�ɉ����ĊY������A�j���[�V������true�ɐݒ�
            if (state == AIState.Walking)
            {
                animator.SetBool("isWalking", true);
            }
            else if (state == AIState.Eating)
            {
                animator.SetBool("isEating", true);
            }
        }
    }

    Vector3 RandomNavSphere(Vector3 origin, float distance)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, NavMesh.AllAreas);

        return navHit.position;
    }

    // �ړI�n�ɃI�u�W�F�N�g�𐶐�
    void SpawnObjectAtDestination(Vector3 position)
    {
        if (objectToSpawn)
        {
            GameObject spawnedObject = Instantiate(objectToSpawn, position, Quaternion.identity); // �w�肵���ʒu�ɃI�u�W�F�N�g�𐶐�
            spawnedObjects.Add(spawnedObject); // ���������I�u�W�F�N�g�����X�g�ɒǉ�
            Debug.Log("Object spawned at: " + position); // �f�o�b�O�p
        }
        else
        {
            Debug.LogWarning("Object to spawn is not assigned!"); // �x�����b�Z�[�W
        }
    }

    void OnDrawGizmos()
    {
        // Gizmos���g���Ĉړ��͈͂�`��
        Gizmos.color = Color.green; // �ΐF�ŕ`��
        Gizmos.DrawWireSphere(transform.position, maxRange); // �ő�͈͂�`��
        Gizmos.color = Color.yellow; // ���F�ŕ`��
        Gizmos.DrawWireSphere(transform.position, minRange); // �ŏ��͈͂�`��

        // ���݂̖ړI�n������
        Gizmos.color = Color.red; // �ԐF�ŕ`��
        Gizmos.DrawSphere(currentDestination, 0.2f); // �ړI�n�̃}�[�J�[��`��

        // AI�ƖړI�n���Ȃ����C����`��
        Gizmos.color = Color.blue; // �F�ŕ`��
        Gizmos.DrawLine(transform.position, currentDestination); // AI����ړI�n�܂ł̃��C����`��
    }
}
