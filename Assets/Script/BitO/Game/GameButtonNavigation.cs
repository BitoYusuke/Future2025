using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameButtonNavigation : MonoBehaviour
{
    [SerializeField, Label("�|�[�Y���")]
    public GameObject MainPanel;
    [SerializeField, Label("������@���")]
    public GameObject Panel_Button_4;

    [SerializeField, Label("�Q�[���ɖ߂�")]
    public Button m_StartButton;
    [SerializeField, Label("�}��")]
    public Button m_BookButton;
    [SerializeField, Label("������@")]
    public Button m_OperationButton;
    [SerializeField, Label("�I��")]
    public Button m_EndButton;

    [SerializeField, Label("�I�����Ȃ�")]
    public Button m_OFFButton;
    [SerializeField, Label("�I������")]
    public Button m_ONButton;

    private Button currentButton;

    // �\���E��\���ɂ���p�l���̎Q��
    [SerializeField, Label("���C���̃p�l��")]
    public GameObject currentPanel;
    [SerializeField, Label("�I���p�̃p�l��")]
    public GameObject targetPanel;

    // �R���g���[���[����
    ControllerState m_State;

    [SerializeField, Label("�|�[�Y��ʎw��")]
    public GameObject canvas;
   
    private void Start()
    {
        // �R���g���[���[
        m_State = GetComponent<ControllerState>();

        // �ŏ��̃{�^���Ƀt�H�[�J�X�𓖂Ă�
        currentButton = m_StartButton;
        m_StartButton.Select();
        m_BookButton.onClick.AddListener(OnBookButtonClick);
        m_EndButton.onClick.AddListener(OnEnd_ONButtonClick);
        //m_ONButton.onClick.AddListener(OnEnd_ONButtonClick);
        m_OFFButton.onClick.AddListener(OnEnd_OFFButtonClick);

    }

    void Update()
    {
        // ���E�̖��L�[�̓��͂��`�F�b�N
        if (m_State.GetButtonDown())
        {
            if (currentButton == m_StartButton)
            {
                currentButton = m_BookButton;
                m_BookButton.Select();
            }
        }
        else if (m_State.GetButtonUp())
        {
            if (currentButton == m_BookButton)
            {
                currentButton = m_StartButton;
                m_StartButton.Select();
            }
        }
    }

    // �|�[�Y��ʂ����
    public void MenuClose()
    {
        // MainPanel���\��
        Time.timeScale = 1.0f; // �Q�[�����Ԃ��ĊJ
        canvas.SetActive(false);
    }

    // �}�ӕ\��
    void OnBookButtonClick()
    {
        // �����ɐ}�ӏ����I�I
    }

    // �����ʂ��J��/����
    public void OperationSelect()
    {
        // MainPanel���\��
        MainPanel.SetActive(false);

        // Panel_Button_3��\��
        Panel_Button_4.SetActive(true);
    }

    // �I���{�^������
    void OnEndButtonClick()
    {
        // ���݂̃p�l�����\��
        if (currentPanel != null)
        {
            currentPanel.SetActive(false);
        }

        // �^�[�Q�b�g�p�l����\��
        if (targetPanel != null)
        {
            targetPanel.SetActive(true);

            // �^�[�Q�b�g�p�l�����̍ŏ��̃{�^���Ƀt�H�[�J�X���ړ�
            Button targetButton = targetPanel.GetComponentInChildren<Button>();
            if (targetButton != null)
            {
                targetButton.Select();
            }
        }
    }

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

            // �^�[�Q�b�g�p�l�����̍ŏ��̃{�^���Ƀt�H�[�J�X���ړ�
            Button currentButton = currentPanel.GetComponentInChildren<Button>();
            if (currentButton != null)
            {
                currentButton.Select();
            }
        }
    }

}
