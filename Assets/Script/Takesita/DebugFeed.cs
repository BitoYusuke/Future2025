using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugFeed : MonoBehaviour
{
    public GameObject FeedPrefab; // ������G�T�̃v���n�u��ݒ�
    public Transform spawnPoint; // �G�T�̐����ʒu��ݒ�
    public GameObject DebugMode;
    public GameObject Target;
    static public bool DebugFlg = false;
    void Start()
    {

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            if(DebugFlg)
            {
                DebugFlg = false;
                DebugMode.SetActive(false);
                Target.SetActive(false);
            }
            else
            {
                DebugFlg = true;
                DebugMode.SetActive(true);
                Target.SetActive(true);
            }
        }
        // �L�[���͂ŃG�T�𓊂���
        if(DebugFlg)
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                Throw();
            }
        }    
    }

    public void Throw()
    {
        if (FeedPrefab != null && spawnPoint != null)
        {
            // �v���n�u�𐶐�
            GameObject feedInstance = Instantiate(FeedPrefab, spawnPoint.position, spawnPoint.rotation);

            // Rigidbody �ɗ͂������đO���ɔ�΂�
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
