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

    void Start()
    {
        if (textComponent != null)
        {
            //textComponent.text = "�V�����e�L�X�g";  // ������ύX
            NameChange();
        }
    }

    void NameChange()
    {
        textComponent.text = KapiName;  // ������ύX
    }
}
