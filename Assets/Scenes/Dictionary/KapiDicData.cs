using UnityEngine;
using System.Linq;

public class KapiDicData : MonoBehaviour
{
    static int Kapis = 24;  //�J�s�̐�
    private int[] IsKapi = new int[Kapis]; // �}�ӂ̔����� (0: ������, 1: �����ς�)
    private int[] IsNewKapi = new int[Kapis]; // �V�K�o�^�� (0: ���o�^, 1: �o�^�ς�)
    private int[] KapiNum = new int[Kapis]; //�J�s�Ɋ��蓖�Ă�ԍ�
    private int IsSortAorD = 0;    //�Z�[�u�ς݃f�[�^�����݂��Ă��邩�ǂ���


    
    //--------------------------------------------------------------------------------------
    //�O�\�[�g�@�z��̏���ێ�����֐��쐬��AKapiDitionary�Ɏ󂯓n���A�����ŉ摜���\�[�g
    //--------------------------------------------------------------------------------------

    private void Start()
    {
        // �f�[�^�����[�h����
        LoadData();
        if(IsSortAorD == 1)
            SortKapi(0);
    }

    // �f�[�^��ۑ����郁�\�b�h
    public void SaveData()
    {
        // IsDiscovered �z����J���}��؂�̕�����ɕϊ����ĕۑ�
        PlayerPrefs.SetString("IsKapi", string.Join(",", IsKapi));

        // IsNewlyRegistered �z����J���}��؂�̕�����ɕϊ����ĕۑ�
        PlayerPrefs.SetString("IsNewKapi", string.Join(",", IsNewKapi));

        PlayerPrefs.SetInt("IsSortAorD", IsSortAorD);

        // PlayerPrefs��ۑ� (�f�[�^���f�B�X�N�ɏ�������)
        PlayerPrefs.Save();
    }



    // �f�[�^��ǂݍ��ރ��\�b�h
    public void LoadData()
    {
        // IsKapi�̃f�[�^�𕶎���Ŏ擾
        string KapiData = PlayerPrefs.GetString("IsKapi", "");
        if (!string.IsNullOrEmpty(KapiData))
        {
            // �R���}��؂�̃f�[�^��z��ɕϊ�
            IsKapi = KapiData.Split(',').Select(int.Parse).ToArray();
        }

        // IsNewKapi�̃f�[�^�𕶎���Ŏ擾
        string NewKapiData = PlayerPrefs.GetString("IsNewKapi", "");
        if (!string.IsNullOrEmpty(NewKapiData))
        {
            // �J���}��؂�̃f�[�^��z��ɕϊ�
            IsNewKapi = NewKapiData.Split(',').Select(int.Parse).ToArray();
        }

        //IsSortAorD�̃f�[�^�𕶎���Ŏ擾
        IsSortAorD = PlayerPrefs.GetInt("IsSortAorD",0);
    }

    public void SaveCharacterName(int characterIndex,string Name)
    {
            PlayerPrefs.SetString($"CharacterName_{characterIndex}", Name);
            PlayerPrefs.Save();
            Debug.Log($"{characterIndex}�̖��O��ۑ����܂���: {Name}");
    }

    public string LoadCharacterName(int characterIndex)
    {
        return PlayerPrefs.GetString($"CharacterName_{characterIndex}", $"�����Ȃ��J�s");
    }

    //0:���� 1:�~�� 2:�o�^�σJ�s��O��
    public void SortKapi(int Sort)
    {
        int C = 0;
        switch(Sort)    
        {
            case 0:
                //�J�s�ԍ������~��(�؂�ւ��)
                for (int i = 0; i < Kapis / 2; i++)
                { 
                    C = IsKapi[i];
                    IsKapi[i]= IsKapi[(Kapis -1) - i];
                    IsKapi[(Kapis - 1) - i] = C;

                }
                if (IsSortAorD == 0)
                    IsSortAorD = 1;
                else
                    IsSortAorD = 0;

                break;
            case 2:
                //�o�^�σJ�s��O�Ƀ\�[�g
                int k;
                for (int i = 0; i < IsKapi.Length; i++)
                {
                    if (IsKapi[i] == 0)
                    {
                        for (int j = i +1; j < IsKapi.Length; j++)
                        {
                            if (IsKapi[j] == 1)
                            {
                                IsKapi[i] = 1;
                                IsKapi[j] = 0;
                                break;
                            }
                        }
                    }
                }
                break;
        }
        SaveData();
    }

    // �}�ӂ̃G���g���[�𔭌��Ƃ��ă}�[�N
    public void MarkAsKapi(int entryIndex)
    {
        if (entryIndex >= 0 && entryIndex < IsKapi.Length)
        {
            IsKapi[entryIndex] = 1; // �����ς݂Ƃ��ă}�[�N
            SaveData();
            Debug.Log("�J�s�ԍ�" + entryIndex + "��V�K�o�^���܂����B");

            //for (int i = 0; i < IsKapi.Length; i++)
                //Debug.Log("�J�s�ԍ�" + i + "�o�^��" +IsKapi[i]);
        }
    }

    // �}�ӂ̃G���g���[��V�K�o�^�Ƃ��ă}�[�N
    public void MarkAsNewlyKapi(int entryIndex)
    {
        if (entryIndex >= 0 && entryIndex < IsNewKapi.Length)
        {
            IsNewKapi[entryIndex] = 1; // �o�^�ς݂Ƃ��ă}�[�N
            SaveData();
        }
    }

    public void DestroyKapi()
    {
        //�S�Ă̓o�^�σJ�s�𖢓o�^��
        for (int i = 0; i < IsKapi.Length; i++)
        {
            if (IsKapi[i] == 1)
                IsKapi[i] = 0;
        }
        SaveData();

        //PlayerPrefs.DeleteAll();
        Debug.Log("�S�ẴJ�s�������܂���");
    }
    // �����ς݂��ǂ������`�F�b�N
    public bool IsEntryKapi(int entryIndex)
    {
        if (entryIndex >= 0 && entryIndex < IsKapi.Length)
        {
            return IsKapi[entryIndex] == 1;
        }
        return false;
    }

    // �V�K�o�^�ς݂��ǂ������`�F�b�N
    public bool IsEntryNewKapi(int entryIndex)
    {
        if (entryIndex >= 0 && entryIndex < IsNewKapi.Length)
        {
            return IsNewKapi[entryIndex] == 1;
        }
        return false;
    }
}
