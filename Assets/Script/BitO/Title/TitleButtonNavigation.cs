using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class TitleButtonNavigation : MonoBehaviour
{
    [SerializeField, Label("���߂���")]
    public Button m_StartButton;
    [SerializeField, Label("�}��")]
    public Button m_BookButton;
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

    void Start()
    {
        // �R���g���[���[
        m_State = GetComponent<ControllerState>();

        // �ŏ��̃{�^���Ƀt�H�[�J�X�𓖂Ă�
        currentButton = m_StartButton;
        m_StartButton.Select();

        // �{�^���ɃN���b�N�C�x���g��ǉ�
        m_BookButton.onClick.AddListener(OnBookButtonClick);
        m_EndButton.onClick.AddListener(OnEndButtonClick);
        m_ONButton.onClick.AddListener(OnEnd_ONButtonClick);
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
        Debug.Log(m_State);
    }

    // �}�Ӄ{�^���̏���
    void OnBookButtonClick()
    {
        // �}�ӈړ��̏����������ɁI�I
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
