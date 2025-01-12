using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameButtonNavigation : MonoBehaviour
{
    [SerializeField, Label("������@���")]
    public GameObject Panel_Button_4;

    [SerializeField, Label("�Q�[���ɖ߂�")]
    public Button m_StartButton;
    [SerializeField, Label("�}��")]
    public Button m_BookButton;
    [SerializeField, Label("������@")]
    public Button m_OperationButton;
    [SerializeField, Label("�^�C�g���ɖ߂�")]
    public Button m_TitleButton;
    [SerializeField, Label("�I��")]
    public Button m_EndButton;

    //[SerializeField, Label("�����ʂ̃{�^��")]
    //public Button m_OpOFFButton;
    [SerializeField, Label("�}�Ӊ�ʂ̃{�^��")]
    public Button m_OpOFFBookButton;
    //[SerializeField, Label("�I�����Ȃ�")]
    //public Button m_OFFButton;
    //[SerializeField, Label("�I������")]
    //public Button m_ONButton;

    private List<Button> buttons; // �{�^�����X�g
    private int currentIndex = 0; // ���݂̃t�H�[�J�X�ʒu

    // �\���E��\���ɂ���p�l���̎Q��
    [SerializeField, Label("���C���̃p�l��")]
    public GameObject currentPanel;
    [SerializeField, Label("�I���p�̃p�l��")]
    public GameObject targetPanel;
    [SerializeField, Label("�}�ӂ̃p�l��")]
    public GameObject BookPanel;

    // �R���g���[���[����
    ControllerState m_State;

    [SerializeField, Label("�|�[�Y��ʎw��")]
    public GameObject canvas;
   
    private void Start()
    {
        // �R���g���[���[
        m_State = GetComponent<ControllerState>();

        //=============================================
        // �{�^�����X�g��������
        buttons = new List<Button> { m_StartButton, m_BookButton, m_OperationButton, m_TitleButton, m_EndButton };

        // �ŏ��̃{�^���Ƀt�H�[�J�X�𓖂Ă�
        currentIndex = 0;
        buttons[currentIndex].Select();   
        //=============================================

        // �{�^���N���b�N��
        m_StartButton.onClick.AddListener(MenuClose);
        m_EndButton.onClick.AddListener(OnEnd_ONButtonClick);
        m_OperationButton.onClick.AddListener(OperationOpen);
        //m_ONButton.onClick.AddListener(OnEnd_ONButtonClick);
        //m_OFFButton.onClick.AddListener(OnEnd_OFFButtonClick);

        //m_OpOFFButton.onClick.AddListener(OperationClose);
        m_BookButton.onClick.AddListener(OpenBook);
        m_OpOFFBookButton.onClick.AddListener(CloseBook);
    }

    void Update()
    {
        // �\���L�[�̓��͂��擾
        if (m_State.GetButtonUp())
        {
            MoveFocus(-1); // ��L�[�Ńt�H�[�J�X���ړ�
        }
        else if (m_State.GetButtonDown())
        {
            MoveFocus(1); // ���L�[�Ńt�H�[�J�X���ړ�
        }

    }

    // �t�H�[�J�X�ړ����W�b�N
    void MoveFocus(int direction)
    {
        // ���݂̃C���f�b�N�X���X�V
        currentIndex = (currentIndex + direction + buttons.Count) % buttons.Count;

        // �V�����{�^���Ƀt�H�[�J�X���ړ�
        buttons[currentIndex].Select();
    }

    // �|�[�Y��ʂ����
    public void MenuClose()
    {
        // currentPanel���\��
        Time.timeScale = 1.0f; // �Q�[�����Ԃ��ĊJ

        canvas.SetActive(false);
    }

    // �p�l�����J���ۂ̃t�H�[�J�X�������\�b�h
    //private void FocusOnPanelButton(GameObject panel)
    //{
    //    if (panel != null)
    //    {
    //        Button firstButton = panel.GetComponentInChildren<Button>();
    //        if (firstButton != null)
    //        {
    //            EventSystem.current.SetSelectedGameObject(firstButton.gameObject); // �t�H�[�J�X�ړ�
    //        }
    //        else
    //        {
    //            Debug.Log($"�t�H�[�J�X����{�^���� {panel.name} �Ɍ�����܂���ł����B");
    //        }
    //    }
    //}

    // �����ʂ��J��
    public void OperationOpen()
    {
        // currentPanel���\��
        currentPanel.SetActive(false);

        // Panel_Button_4��\��
        Panel_Button_4.SetActive(true);

        //FocusOnPanelButton(Panel_Button_4);
    }

    //�����ʂ����
    //public void OperationClose()
    //{
    //    Panel_Button_4.SetActive(false);

    //    currentPanel.SetActive(true);

    //    //FocusOnPanelButton(currentPanel);
    //}

    // �I���{�^������
    //void OnEndButtonClick()
    //{
    //    // ���݂̃p�l�����\��
    //    if (currentPanel != null)
    //    {
    //        currentPanel.SetActive(false);
    //    }

    //    // �^�[�Q�b�g�p�l����\��
    //    if (targetPanel != null)
    //    {
    //        targetPanel.SetActive(true);

    //        //FocusOnPanelButton(targetPanel);
    //    }
    //}

    void OnEnd_ONButtonClick()
    {
        // �Q�[�����I��
        Application.Quit();

        // Unity�G�f�B�^��Ŏ��s���Ă���ꍇ�͒�~������
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif    
    }

    public void OnEnd_OFFButtonClick()
    {
        if (targetPanel != null)
        {
            targetPanel.SetActive(false);
        }

        if (currentPanel != null)
        {
            currentPanel.SetActive(true);

            //FocusOnPanelButton(currentPanel);
        }
    }
    void OpenBook()
    {
        // currentPanel���\��
        currentPanel.SetActive(false);

        // Panel_Button_4��\��
        BookPanel.SetActive(true);

       // FocusOnPanelButton(BookPanel);
    }
    void CloseBook() 
    {
        BookPanel.SetActive(false);

        currentPanel.SetActive(true);

        // ���C���p�l���̍ŏ��̃{�^���Ƀt�H�[�J�X��߂�
        if (currentPanel != null)
        {
           // FocusOnPanelButton(currentPanel);
        }
    }
}
