using UnityEngine;

public class ThrowFeed : MonoBehaviour
{
    public GameObject FeedPrefab; // ������G�T�̃v���n�u��ݒ�
    public Transform spawnPoint; // �G�T�̐����ʒu��ݒ�

    private FeedGauge feedGauge;

    void Start()
    {
        // FeedGauge �R���|�[�l���g���擾
        feedGauge = FindObjectOfType<FeedGauge>();
    }

    void Update()
    {
        // �L�[���͂ŃG�T�𓊂���
        if (Input.GetKeyDown(KeyCode.P))
        {
            ThrowFeedPrefab();
        }
    }

    // �G�T�𓊂��鏈��
    void ThrowFeedPrefab()
    {
        // �G�T�����������Ԃ��m�F
        if (FeedGauge.fFeedFlg)
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

                // �t���O�����Z�b�g���A�Q�[�W��0�ɖ߂�
                FeedGauge.fFeedFlg = false;
                feedGauge.Gauge.fillAmount = 0;
            }
            else
            {
                Debug.LogWarning("FeedPrefab �܂��� spawnPoint ���ݒ肳��Ă��܂���B");
            }
        }
    }
}