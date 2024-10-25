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
    [SerializeField, Label("��������")]
    public Button m_ContinuationButton;
    [SerializeField, Label("�}��")]
    public Button m_BookButton;
    [SerializeField, Label("�I��")]
    public Button m_EndButton;

    private Button currentButton;

    void Start()
    {
        // �ŏ��̃{�^���Ƀt�H�[�J�X�𓖂Ă�
        currentButton = m_StartButton;
        m_StartButton.Select();

        // �{�^���ɃN���b�N�C�x���g��ǉ�
        m_ContinuationButton.onClick.AddListener(OnContinuationButtonClick);
        m_BookButton.onClick.AddListener(OnBookButtonClick);
        m_EndButton.onClick.AddListener(OnEndButtonClick);
    }

    void Update()
    {
        // ���E�̖��L�[�̓��͂��`�F�b�N
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentButton == m_StartButton)
            {
                currentButton = m_ContinuationButton;
                m_ContinuationButton.Select();
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentButton == m_ContinuationButton)
            {
                currentButton = m_StartButton;
                m_StartButton.Select();
            }
        }
    }

    // ��������{�^���̏���
    void OnContinuationButtonClick()
    {
        // ��������̏����������ɁI�I

    }

    // �}�Ӄ{�^���̏���
    void OnBookButtonClick()
    {
        // �}�ӈړ��̏����������ɁI�I
    }

    // �I���{�^������
    void OnEndButtonClick()
    {
        // �Q�[�����I��
        Application.Quit();

        // Unity�G�f�B�^��Ŏ��s���Ă���ꍇ�͒�~������
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
