using UnityEngine;
using UnityEngine.EventSystems;

public class ClickRoButton : MonoBehaviour
{
    public int KapiIdx = 0;
    SerectKapi DKapi;
    KapiDicData KapiData;
    KapiDictionary KapiDic;

    void Start()
    {
        DKapi = new SerectKapi();
        KapiDic = new KapiDictionary();
    }

    void Update()
    {
        if (DKapi == null)
            DKapi = FindObjectOfType<SerectKapi>();
        if (KapiData == null)
            KapiData = FindObjectOfType<KapiDicData>();
        if (KapiDic == null)
            KapiDic = FindObjectOfType<KapiDictionary>();

        //�y�[�W�ړ�
        if (Input.GetKeyDown(KeyCode.L))
        {
            KapiDic.NextPage();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            KapiDic.PreviousPage();
        }
        //�����~��
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            ASortKapi();
        }

        //�o�^(�f�o�b�O�p�Ȃ̂ŃR���g���[���Ή��s�K�v)
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            RegistrationKapi();
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            DestroyKapi();
        }

    }

    //�폜(��{����Ăт������ƂȂ�)
    public void DestroyKapi()
    {
        KapiData.DestroyKapi();
        KapiDic.UpdatePage();
    }

    //�o�^(Discord�ɓo�^�̎d��������)
    public void RegistrationKapi()
    {
        int num = 0;
        num = DKapi.GetKapiNumber();

        KapiData.MarkAsKapi(num);
        if (KapiDic.imageSlots != null)
        {
            KapiDic.UpdatePage();
        }
        else
        {
            Debug.LogError("UpdateKapi���Ăяo���O��imageSlots��null�ł��B");
        }
    }

    //����
    public void ASortKapi()
    {
        KapiData.SortKapi(0);
        KapiDic.ImageSlotsSort(true);
        KapiDic.UpdatePage();
    }

    //�~��
    public void DSortKapi()
    {
        KapiData.SortKapi(2);
        KapiDic.UpdatePage();
    }
}
