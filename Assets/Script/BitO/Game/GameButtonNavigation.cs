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

    private Button currentButton;

    private void Start()
    {
        // �ŏ��̃{�^���Ƀt�H�[�J�X�𓖂Ă�
        currentButton = m_StartButton;
        m_StartButton.Select();
        m_EndButton.onClick.AddListener(OnEndButtonClick);

    }

    void Update()
    {
        // ���E�̖��L�[�̓��͂��`�F�b�N
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentButton == m_StartButton)
            {
                currentButton = m_BookButton;
                m_BookButton.Select();
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
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
        MainPanel.SetActive(false);
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
        // �Q�[�����I��
        Application.Quit();

        // Unity�G�f�B�^��Ŏ��s���Ă���ꍇ�͒�~������
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

}
