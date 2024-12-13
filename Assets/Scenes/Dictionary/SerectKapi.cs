using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SerectKapi : MonoBehaviour
{
    KapiDicData KapiData;
    KapiDictionary KapiNum;
    RectTransform rectTransform;
    KapinputMG Nadukeoya;

    public int SerectKapinum = 0;
    public Image targetImage;   //�I��UI
    public Vector2[] Vec;

    private int Kapidx = 0;

    // Start is called before the first frame update
    void Start()
    {
        //�C���X�^���X�̎擾
        KapiData = new KapiDicData();

        if (targetImage != null)
        {
            // RectTransform���擾
            rectTransform = targetImage.GetComponent<RectTransform>();

            // �V�����ʒu��ݒ� (�A���J�[��̃��[�J�����W)
            rectTransform.anchoredPosition = new Vector2(Vec[0].x,Vec[0].y);
        }
        SerectKapinum = 0;
    }

    void Update()
    {
        //�L�[���͂��ꂽ��
        bool IsInput = false;

        if (KapiData == null)
        {
            KapiData = FindObjectOfType<KapiDicData>();
        }
        if (KapiNum == null)
        {
            KapiNum = FindObjectOfType<KapiDictionary>();
        }
        if(Nadukeoya == null)
        {
            Nadukeoya = FindObjectOfType<KapinputMG>();
        }


        int PageNum = 0;
        //��
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            PageNum = KapiNum.GetKapiPage();
            //�I���J�s�̕ύX
            Kapidx++;
            if (Kapidx > 7)
                Kapidx = 0;
            //�N���b�N�����J�s�̔ԍ��ێ�
            SerectKapinum = PageNum + Kapidx;

            Debug.Log("���ݑI�𒆃J�s:" + SerectKapinum);
            IsInput = true;
        }
        //��
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PageNum = KapiNum.GetKapiPage();
            //�I���J�s�̕ύX
            Kapidx--;
            if (Kapidx < 0)
                Kapidx = 7;
            //�N���b�N�����J�s�̔ԍ��ێ�
            SerectKapinum = PageNum  + Kapidx;

            Debug.Log("���ݑI�𒆃J�s:" + SerectKapinum);
            IsInput = true;
        }
        //��
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            PageNum = KapiNum.GetKapiPage();
            //�I���J�s�̕ύX
            Kapidx -= 4;
            if (Kapidx < 0)
                Kapidx = 8 - (Kapidx * -1);
            //�N���b�N�����J�s�̔ԍ��ێ�
            SerectKapinum = PageNum + Kapidx;

            Debug.Log("���ݑI�𒆃J�s:" + SerectKapinum);
            IsInput = true;
        }
        //��
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            PageNum = KapiNum.GetKapiPage();
            //�I���J�s�̕ύX
            Kapidx += 4;
            if (Kapidx > 7)
                Kapidx = 0 + (Kapidx - 7) -1;
            //�N���b�N�����J�s�̔ԍ��ێ�
            SerectKapinum = PageNum + Kapidx;

            Debug.Log("���ݑI�𒆃J�s:" + SerectKapinum);
            IsInput = true;
        }


        if(Input.GetKeyDown(KeyCode.Return))//�G���^�[�����͂��ꂽ��I�𒆃J�s�̖��O���͂��\��
        {
            Nadukeoya.ActiveNameInput(SerectKapinum);
        }


        if (IsInput)
        {
            rectTransform.anchoredPosition = new Vector2(Vec[Kapidx].x, Vec[Kapidx].y);
        }
    }


    public int GetKapiNumber()
    {
        return SerectKapinum;
    }

}
