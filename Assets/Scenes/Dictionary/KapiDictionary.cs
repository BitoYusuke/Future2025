using UnityEngine;
using UnityEngine.UI;

public class KapiDictionary : MonoBehaviour
{
    public Image[] imageSlots;      // 8��Image�I�u�W�F�N�g
    public Sprite[] images;         // �S�摜�̃��X�g
    public Button nextButton;       // ���̃y�[�W�{�^��
    public Button prevButton;       // �O�̃y�[�W�{�^��

    public bool[] RegistrationKapi; //�J�s���o�^����Ă��邩�̏��



    //�J�s�ԍ�
    public int imageIndex = 0;


    private int currentPage = 0;    // ���݂̃y�[�W�ԍ�
    private int imagesPerPage = 8;  // 1�y�[�W�ɕ\������摜��

    private bool IsOnce = false;

    KapiDicData KapiData;
    KapinputMG KapiName;

    void Start()
    {
        // �{�^���Ƀ��X�i�[��ǉ�
        nextButton.onClick.AddListener(NextPage);
        prevButton.onClick.AddListener(PreviousPage);

        // �ŏ��̃y�[�W�ł́u�O�ցv�{�^���𖳌��ɂ���
        prevButton.interactable = false;

        KapiData = new KapiDicData();
        //�f�o�b�O�p
        for (int i = 0; i < 24; i++)
        {
            if (KapiData.IsEntryKapi(i))
            {
                RegistrationKapi[i] = true;
            }
            else
            {
                RegistrationKapi[i] = false;
            }
        }

        UpdatePage();
    }

    void Update()
    {
        if (KapiData == null)
            KapiData = FindObjectOfType<KapiDicData>();
        if (KapiName == null)
            KapiName = FindObjectOfType<KapinputMG>();
        if (!IsOnce)
        {
            for (int i = 0; i < 24; i++)
            {
                if (KapiData.IsEntryKapi(i))
                {
                    RegistrationKapi[i] = true;
                }
                else
                {
                    RegistrationKapi[i] = false;
                }
            }
            UpdatePage();
        }
    }

    //�y�[�W�ړ���̕\��
    public void UpdatePage()
    {
        //�f�o�b�O�p
        for (int i = 0; i < 24; i++)
        {
            if (KapiData.IsEntryKapi(i))
            {
                RegistrationKapi[i] = true;
            }
            else
            {
                RegistrationKapi[i] = false;
            }
        }
        // ���݂̃y�[�W�ɑΉ�����摜���Z�b�g
        for (int i = 0; i < imagesPerPage; i++)
        {
            imageIndex = currentPage * imagesPerPage;
            imageIndex += i;


            imageSlots[i].sprite = images[imageIndex];
            Color color = imageSlots[i].color;

            if (imageIndex < images.Length && RegistrationKapi[imageIndex] == true) //�摜�����݂��Ă��āA�J�s���o�^����Ă����
            {
                color.r = 1.0f;
                color.g = 1.0f;
                color.b = 1.0f;
                imageSlots[i].gameObject.SetActive(true);
            }
            else
            {
                // �摜���Ȃ��ꍇ�̓X���b�g���\����
                color.r = 0.0f;
                color.g = 0.0f;
                color.b = 0.0f;
            }
            imageSlots[i].color = color;
        }




            // �{�^���̏�Ԃ��X�V
            prevButton.interactable = currentPage > 0;
        nextButton.interactable = (currentPage + 1) * imagesPerPage < images.Length;
    }

    //�o�^��̕\��
    public void UpdateKapi()
    {
        // ���݂̃y�[�W�ɑΉ�����摜���Z�b�g
        //�f�o�b�O�p
        for (int i = 0; i < 24; i++)
        {
            if (KapiData.IsEntryKapi(i))
            {
                RegistrationKapi[i] = true;
            }
            else
            {
                RegistrationKapi[i] = false;
            }
        }

        for (int i = 0; i < imagesPerPage; i++)
        {
            Color color = imageSlots[i].color;

            imageIndex = currentPage * imagesPerPage;
            imageIndex += i;

            if (imageIndex < images.Length && RegistrationKapi[imageIndex] == true) //�摜�����݂��Ă��āA�J�s���o�^����Ă����
            {
                color.r = 1.0f;
                color.g = 1.0f;
                color.b = 1.0f;
            }
            else
            {
                // �摜���Ȃ��ꍇ�̓X���b�g���\����
                color.r = 0.0f;
                color.g = 0.0f;
                color.b = 0.0f;
            }
            imageSlots[i].color = color;
        }
    }

    void NextPage()
    {
        if ((currentPage + 1) * imagesPerPage < images.Length)
        {
            currentPage++;
            UpdatePage();
            KapiName.UpdatePageName();
        }
    }

    void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            UpdatePage();
            KapiName.UpdatePageName();
        }
    }

    public int GetKapiPage()
    {
        int num;
        num = currentPage * imagesPerPage;
        return num;
    }

    public void ImageSlotsSort(bool Sort)
    {
        int Kapis = 24;
        Sprite C;

        if(Sort)
        {
            for (int i = 0; i < Kapis / 2; i++)
            {
                Debug.Log(i);
                Debug.Log((Kapis - 1) - i);
                C = images[i];
                images[i] = images[(Kapis - 1) - i];
                images[(Kapis - 1) - i] = C;
            }
        }
        //else
        //{
        //    int k;
        //    for (int i = 0; i < Kapis; i++)
        //    {
        //        if (images[i] == 0)
        //        {
        //            for (int j = i + 1; j < Kapis; j++)
        //            {
        //                if (IsKapi[j] == 1)
        //                {
        //                    images[i] = 1;
        //                    images[j] = 0;
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //}
    }
}
