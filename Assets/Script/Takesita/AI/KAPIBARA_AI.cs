using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class KAPIBARA_AI : MonoBehaviour
{
    // --- AIState�p�ϐ�
    public enum AIState { Idle, Walking, Eating } // AIState::AI�̏�Ԃ��`
                                                  // Idle::�ҋ@
                                                  // Walking::����
                                                  // Eating::�H�ׂ�
    private AIState currentState;                 // ���݂̃X�e�[�g�i�[�p

    // �X�e�[�g�̏o���m��
    [Range(0, 100)] public int idleWeight = 50; // Idle�̏d��
    [Range(0, 100)] public int walkingWeight = 30; // Walking�̏d��
    [Range(0, 100)] public int eatingWeight = 20; // Eating�̏d��


    public float idleDuration = 2.0f;// Idle�X�e�[�g�̑ҋ@����
    public float eatingDuration = 3.0f;// Eating�X�e�[�g�̃A�j���[�V�����Đ�����

    public float  IdleRotationSpeed = 10f;
    private float IdletargetRotation; // ���̖ڕW��]�p�x
    private float IdlenextRotationTime = 0f; // ���̃����_���ȕ������莞��

    private NavMeshAgent agent; // NavMesh�R���|�[�l���g
    private Animator animator;  // Animator�R���|�[�l���g

    private Vector3 currentDestination;
    public float minRange = 5f; // Walking���̖ړI�n�܂ł̍ŏ��͈�
    public float maxRange = 10f; // Walking���̖ړI�n�܂ł̍ő�͈�

    [SerializeField] private GameObject destinationMarkerPrefab; // �ڈ�Prefab���w�肷��
    private GameObject currentMarker; // ���݂̖ڈ�I�u�W�F�N�g��ێ�


    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // NavMeshAgent�R���|�[�l���g�̎擾
        animator = GetComponent<Animator>();  // Animator�R���|�[�l���g�̎擾
        agent.stoppingDistance = 0;
        agent.autoBraking = true;
        agent.updateRotation = false;
        currentState = AIState.Idle; // �����X�e�[�g��Idle�ɐݒ�

        if (agent == null)
        {
            Debug.LogError("NavMeshAgent���A�^�b�`����Ă��܂���");
        }
        else if (!agent.enabled)
        {
            Debug.LogError("NavMeshAgent���L��������Ă��܂���");
        }
        StartCoroutine(StateMachine());
    }
    void Update()
    {
        switch (currentState)
        {
            case AIState.Idle:
                Idle();
                break;
            case AIState.Walking:
                Walking();
                break;
            case AIState.Eating:
                Eating();
                break;
        }
    }

    private IEnumerator StateMachine()
    {
        while (true)
        {
            switch (currentState)
            {
                case AIState.Idle:
                    Debug.Log("���: Idle");
                    yield return new WaitForSeconds(idleDuration);
                    // �����_���Ɏ��̃X�e�[�g������
                    currentState = GetWeightedRandomState(); // �d�ݕt�������_���Ŏ��̃X�e�[�g������
                    break;

                case AIState.Walking:
                    Debug.Log("���: Walking");
                    animator.SetInteger("state", 1); // Walking�A�j���[�V����
                    SetNewDestination(); // �ړI�n��ݒ肷��
                    yield return new WaitUntil(() => !agent.pathPending && agent.remainingDistance < 0.5f); // �ړI�n�����܂őҋ@
                    currentState = AIState.Idle; // Walking����Idle�֑J��
                    break;

                case AIState.Eating:
                    Debug.Log("���: Eating");
                    // Eating���I�������Idle�ɖ߂�
                    yield return new WaitForSeconds(eatingDuration);
                    currentState = AIState.Idle; // Eating����Idle�֑J��
                    break;
            }
        }
    }

    public void Idle()
    {
        animator.SetInteger("state", 0); // Idle�A�j���[�V����

        // ��莞�Ԃ��ƂɃ����_���ȕ��������߂�
        if (Time.time > IdlenextRotationTime)
        {
            // �V���������_���ȕ���������
            IdletargetRotation = Random.Range(-180f, 180f); // �����_���Ȋp�x�i-180������180���j
            IdlenextRotationTime = Time.time + Random.Range(1f, 3f); // ����̃����_���ȕ�������܂ł̎��Ԃ�ݒ�i1�`3�b���Ɓj
        }

        // ������肻�̕����Ɍ������ĉ�]
        float step = IdleRotationSpeed * Time.deltaTime;
        float currentYRotation = transform.eulerAngles.y; // ���݂�Y����]�p�x
        float newYRotation = Mathf.MoveTowardsAngle(currentYRotation, IdletargetRotation, step); // ���X�ɖڕW�p�x�Ɍ�����

        // ��]��K�p
        transform.rotation = Quaternion.Euler(0, newYRotation, 0);
    }
    public void Walking()
    {
        animator.SetInteger("state", 1); // Walking�A�j���[�V�����ɐ؂�ւ�
        // ���݂̈ʒu����w��͈͓��Ń����_���ȖړI�n��ݒ�
        if (agent.remainingDistance < 0.1f)
        {
            currentDestination = RandomNavSphere(transform.position, Random.Range(minRange, maxRange));
            agent.SetDestination(currentDestination);

            Debug.Log("New Destination Set: " + currentDestination); // �f�o�b�O�p
        }

        // �ړI�n�Ɍ���������������
        Vector3 direction = (currentDestination - transform.position).normalized;
        if (direction.magnitude > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }
    public void Eating()
    {
        animator.SetInteger("state", 2); // Eating�A�j���[�V�������Đ�
    }

    private AIState GetWeightedRandomState()
    {
        // �d�݂̍��v���v�Z
        int totalWeight = idleWeight + walkingWeight + eatingWeight;
        int randomValue = Random.Range(0, totalWeight);

        // �����_���Ȓl�Ɋ�Â��ăX�e�[�g������
        if (randomValue < idleWeight)
        {
            return AIState.Idle;
        }
        else if (randomValue < idleWeight + walkingWeight)
        {
            return AIState.Walking;
        }
        else
        {
            return AIState.Eating;
        }
    }

    private void SetNewDestination()
    {
        // �����_���ȖړI�n�𐶐�
        currentDestination = RandomNavSphere(transform.position, Random.Range(minRange, maxRange));
        agent.SetDestination(currentDestination);
        Debug.Log("New Destination Set: " + currentDestination);

        // �����̃}�[�J�[������ꍇ�͍폜
        if (currentMarker != null)
        {
            Destroy(currentMarker);
        }

        // �V�����ړI�n�}�[�J�[�𐶐����Ĕz�u
        currentMarker = Instantiate(destinationMarkerPrefab, currentDestination, Quaternion.identity);
    }

    public Vector3 RandomNavSphere(Vector3 origin, float distance)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, NavMesh.AllAreas);

        return navHit.position;
    }
}
