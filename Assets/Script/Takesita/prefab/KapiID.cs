using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // UI�ɃA�N�Z�X���邽�߂̖��O���
public class KapiID : MonoBehaviour
{
    public int KapiId;
    public string KapiName;
    // Start is called before the first frame update
    public Text textComponent;  // �C���X�y�N�^�[����Text�R���|�[�l���g���w��
    KapiDicData KapiData;

    void Start()
    {
        if (textComponent != null)
        {
            //textComponent.text = "�V�����e�L�X�g";  // ������ύX
            NameChange(KapiName);
        }
    }

    void Update()
    {
        if (KapiData == null)
            KapiData = FindObjectOfType<KapiDicData>();


        textComponent.text = KapiData.LoadCharacterName(KapiId);
    }

    public void NameChange(string name)
    {
        textComponent.text = name;  // ������ύX
    }
}
