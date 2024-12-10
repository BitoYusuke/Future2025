using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class tesuto : MonoBehaviour
{
    [SerializeField, Label("�|�[�Y���")]
    public GameObject MainPanel;
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
    ShowCanvasOnEnter CanvasOnEnter;

    // �R���g���[���[����
    ControllerState m_State;


    private void Start()
    {
        // �R���g���[���[
        m_State = GetComponent<ControllerState>();

        // �ŏ��̃{�^���Ƀt�H�[�J�X�𓖂Ă�
        currentButton = m_StartButton;
        m_StartButton.Select();
        m_StartButton.onClick.AddListener(OnButtonClick);
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
    public void OnButtonClick()
    {
        // MainPanel���\��
        Time.timeScale = 1.0f; // �Q�[�����Ԃ��ĊJ
        MainPanel.SetActive(false);
    }
}
