using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugKapiSpawn : MonoBehaviour
{
    public List<GameObject> aiPrefabs; // ��������AI�I�u�W�F�N�g��Prefab���X�g
    private List<GameObject> availablePrefabs; // �c��̐����\��Prefab���X�g
    public Transform parentObject; // �e�I�u�W�F�N�g���C���X�y�N�^�[����w��

    public float minScale = 0.5f; // �ŏ��X�P�[���i�C���X�y�N�^�[�Őݒ�\�j
    public float maxScale = 2f;  // �ő�X�P�[���i�C���X�y�N�^�[�Őݒ�\�j

    // Start is called before the first frame update
    void Start()
    {
        availablePrefabs = new List<GameObject>(aiPrefabs); // �v���n�u���X�g�𕡐����Ďg�p
    }

    // Update is called once per frame
    void Update()
    {
        if(DebugFeed.DebugFlg)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                SpawnAIObject();
                //nextSpawnTime = Time.time + spawnInterval; // ���̐������Ԃ��X�V
            }
        }      
    }

    void SpawnAIObject()
    {
        // �����\��Prefab���Ȃ��ꍇ�͉������Ȃ�
        if (availablePrefabs.Count == 0)
        {
            Debug.Log("No more unique prefabs available to spawn.");
            return;
        }

        // �����_���Ȉʒu�𐶐����邽�߂͈̔͂�ݒ�
        Vector3 randomPosition = new Vector3(
            Random.Range(-30.3f, -25.0f), // X���͈̔�
            -12.64f,  // Y����0�ɌŒ�
            Random.Range(10.3f, 12f)  // Z���͈̔�
        );

        // ��������ʒu��n�ʂɃX�i�b�v
        RaycastHit hit;
        if (Physics.Raycast(randomPosition + Vector3.up * 10, Vector3.down, out hit, 100f))
        {
            randomPosition.y = hit.point.y; // �n�ʂ̍����ɃX�i�b�v
        }

        // �����_����Prefab��I�����Đ������A�g�p�ς݂Ƃ��ă��X�g����폜
        int randomIndex = Random.Range(0, availablePrefabs.Count);
        GameObject selectedPrefab = availablePrefabs[randomIndex];
        GameObject spawnedObject = Instantiate(selectedPrefab, randomPosition, Quaternion.identity);
        //Instantiate(selectedPrefab, randomPosition, Quaternion.identity);
        Debug.Log("AI object spawned at: " + randomPosition + " using prefab: " + selectedPrefab.name);

        // �����_���ȃX�P�[�����v�Z���A�S���ɓK�p
        float randomScale = Random.Range(minScale, maxScale);
        spawnedObject.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

        // ���������I�u�W�F�N�g�̐e��ݒ�
        spawnedObject.transform.SetParent(parentObject);

        // �g�p����Prefab�����X�g����폜
        availablePrefabs.RemoveAt(randomIndex);
    }
}
