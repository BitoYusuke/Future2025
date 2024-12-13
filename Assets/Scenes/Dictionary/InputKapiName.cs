using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputKapiName : MonoBehaviour
{
    [SerializeField] InputField nameInputField; // InputField (TextMeshPro��)
    [SerializeField] Text displayNameText;      // ���O��\������Text
    private string playerNameKey = "PlayerName";           // PlayerPrefs�̃L�[

    private KapiDicData KapiData;
    private SerectKapi SKapi;
    private KapiDictionary KapiNum;

    public int SaveNum = 0;
    public int PageNum = 0; //�y�[�W�ɂ�����Image�̔ԍ�



    // �Q�[���J�n���ɕۑ����ꂽ���O��\��
    private void Start()
    {
        //�C���X�^���X�̎擾
        KapiData = new KapiDicData();
    }

    void Update()
    {
        if (KapiData == null)
        {
            KapiData = FindObjectOfType<KapiDicData>();
        }
        if (SKapi == null)
        {
            SKapi = FindObjectOfType<SerectKapi>();
        }
        if (KapiNum == null)
        {
            KapiNum = FindObjectOfType<KapiDictionary>();
            UpdateDisplayedName();
        }
    }

    // ���O��ۑ�����
    public void SaveName()
    {
        string enteredName = nameInputField.text;

        if (!string.IsNullOrEmpty(enteredName))
        {
            KapiData.SaveCharacterName(KapiNum.GetKapiPage() + PageNum, enteredName); // ���O��ۑ�
            UpdateDisplayedName();
        }
        else
        {
            Debug.LogWarning("���O�����͂���Ă��܂���I");
        }
    }



    // �ۑ����ꂽ���O��ǂݍ���ŕ\��
    public void UpdateDisplayedName()
    {
        SaveNum = KapiNum.GetKapiPage() + PageNum;

        // PlayerPrefs���疼�O���擾 (�f�t�H���g�l�́u�L�����N�^�[ + �C���f�b�N�X�v)
        string characterName = KapiData.LoadCharacterName(SaveNum);

        // �e�L�X�g�ɖ��O��\��
        if (characterName != null)
        {
            nameInputField.text = $"{characterName}";
            Debug.Log(characterName);
        }
    }
}
