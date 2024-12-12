using UnityEngine;
using UnityEngine.UI;

public class FeedGauge : MonoBehaviour
{
    public Image Gauge;  // UI�Q�[�W�iImage�j
    public static bool fFeedFlg; // �G�T���������邩�ǂ����̃t���O
    public GameObject FeedShine;
    public float gaugeSpeed = 0.2f; // �Q�[�W�̂��܂鑬�x

    void Start()
    {
        Gauge.fillAmount = 0; // �����l��0�ɐݒ�
        fFeedFlg = false;
    }

    void Update()
    {
        // �Q�[�W�����܂��Ă��Ȃ��ꍇ�AfillAmount�𑝉�������
        if (!fFeedFlg)
        {
            Gauge.fillAmount += Time.deltaTime * gaugeSpeed;
            
            // �Q�[�W���ő�l�i1�j�ɂȂ�����G�T�𓊂�����悤�ɂ���
            if (Gauge.fillAmount >= 1)
            {
                Gauge.fillAmount = 1; // �����1�ɌŒ�
                fFeedFlg = true;
                FeedShine.SetActive(true);
            }
            else
            {
                FeedShine.SetActive(false);
            }
        }
    }
}