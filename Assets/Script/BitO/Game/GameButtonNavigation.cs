using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameButtonNavigation : MonoBehaviour
{
    [SerializeField, Label("�|�[�Y���")]
    public GameObject MainPanel;
    [SerializeField, Label("�Z�[�u���")]
    public GameObject Panel_Button_2;
    [SerializeField, Label("������@���")]
    public GameObject Panel_Button_4;

    [SerializeField, Label("�Q�[���ɖ߂�")]
    public Button m_StartButton;
    [SerializeField, Label("�Z�[�u")]
    public Button m_SaveButton;
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
    }

    void Update()
    {
        // ���E�̖��L�[�̓��͂��`�F�b�N
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentButton == m_StartButton)
            {
                currentButton = m_SaveButton;
                m_SaveButton.Select();
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentButton == m_SaveButton)
            {
                currentButton = m_StartButton;
                m_StartButton.Select();
            }
        }
    }

    // Button_3���N���b�N���ꂽ���ɌĂяo����郁�\�b�h
    public void SwitchPanels()
    {
        // MainPanel���\��
        MainPanel.SetActive(false);

        // Panel_Button_3��\��
        Panel_Button_2.SetActive(true);
    }

    public void OperationSelect()
    {
        // MainPanel���\��
        MainPanel.SetActive(false);

        // Panel_Button_3��\��
        Panel_Button_4.SetActive(true);
    }

}
