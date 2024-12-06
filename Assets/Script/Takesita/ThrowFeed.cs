using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowFeed : MonoBehaviour
{
    public GameObject FeedPrefab; // ������G�T�̃v���n�u��ݒ�
    public Transform spawnPoint; // �G�T�̐����ʒu��ݒ�i�Ⴆ�΁A�J�����̑O���Ȃǁj

    // Start is called before the first frame update
    void Start()
    {
        // �K�v�ɉ����ď���������
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ThrowFeedPrefab();
        }
    }

    // �G�T�𓊂��鏈��
    void ThrowFeedPrefab()
    {
        if (FeedPrefab != null && spawnPoint != null)
        {
            // �v���n�u�𐶐�
            GameObject feedInstance = Instantiate(FeedPrefab, spawnPoint.position, spawnPoint.rotation);

            // �K�v�ɉ����Đ�����̏����i��: Rigidbody�ŗ͂�������j
            Rigidbody rb = feedInstance.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(spawnPoint.forward * 500f); // �O���ɗ͂�������
            }
        }
        else
        {
            Debug.LogWarning("FeedPrefab �܂��� spawnPoint ���ݒ肳��Ă��܂���B");
        }
    }
}
