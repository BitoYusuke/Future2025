using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndSelect : MonoBehaviour
{
    [SerializeField, Label("�I�����Ȃ�")]
    public Button m_ONButton;
    [SerializeField, Label("�I������")]
    public Button m_OFFButton;

    private Button currentButton;

    // �\���E��\���ɂ���p�l���̎Q��
    [SerializeField, Label("���C���̃p�l��")]
    public GameObject currentPanel;
    [SerializeField, Label("�I���p�̃p�l��")]
    public GameObject targetPanel;

    void Start()
    {
        // �ŏ��̃{�^���Ƀt�H�[�J�X�𓖂Ă�
        currentButton = m_ONButton;
        m_ONButton.Select();

        // �{�^���ɃN���b�N�C�x���g��ǉ�
        m_ONButton.onClick.AddListener(OnButtonClick);
        m_OFFButton.onClick.AddListener(OFFButtonClick);
    }

    void Update()
    {
        // ���E�̖��L�[�̓��͂��`�F�b�N
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentButton == m_ONButton)
            {
                currentButton = m_OFFButton;
                m_OFFButton.Select();
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentButton == m_OFFButton)
            {
                currentButton = m_ONButton;
                m_ONButton.Select();
            }
        }
    }

    void OnButtonClick()
    {
        // �Q�[�����I��
        Application.Quit();

        // Unity�G�f�B�^��Ŏ��s���Ă���ꍇ�͒�~������
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif    
    }


    public void OFFButtonClick()
    {
        if (targetPanel != null)
        {
            targetPanel.SetActive(false);
        }

        if (currentPanel != null)
        {
            currentPanel.SetActive(true);
        }        
    }

}
