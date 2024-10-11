using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KapibaraSpawner : MonoBehaviour
{
    public GameObject aiPrefab;      // ��������AI�I�u�W�F�N�g��Prefab
    public float spawnInterval = 5f; // ��������Ԋu�i�b�j
    public int maxObjects = 10;      // ��������AI�I�u�W�F�N�g�̍ő吔
    public float nextSpawnTime;     // ���ɐ������鎞��

    void Start()
    {
        nextSpawnTime = Time.time + spawnInterval; // �ŏ��̐������Ԃ�ݒ�
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime && GameObject.FindGameObjectsWithTag("KAPIBARA").Length < maxObjects) // "AI"�^�O�����Ă���I�u�W�F�N�g�𐔂���
        {
            SpawnAIObject();
            nextSpawnTime = Time.time + spawnInterval; // ���̐������Ԃ��X�V
        }
    }

    void SpawnAIObject()
    {
        // �����_���Ȉʒu�𐶐����邽�߂͈̔͂�ݒ�
        Vector3 randomPosition = new Vector3(
            Random.Range(-5f, 5f), // X���͈̔�
            0,  // ��Uy����0�ɌŒ�
            Random.Range(-5f, 5f)  // Z���͈̔�
        );

        // ��������ʒu���V�[����̓K�؂Ȉʒu�i�n�ʁj�ɃX�i�b�v
        RaycastHit hit;
        if (Physics.Raycast(randomPosition + Vector3.up * 10, Vector3.down, out hit, 100f))
        {
            randomPosition.y = hit.point.y; // �n�ʂ̍����ɃX�i�b�v
        }

        // AI�I�u�W�F�N�g�𐶐�
        Instantiate(aiPrefab, randomPosition, Quaternion.identity);
        Debug.Log("AI object spawned at: " + randomPosition);
    }
}
