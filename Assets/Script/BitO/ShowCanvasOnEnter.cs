using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowCanvasOnEnter : MonoBehaviour
{
    public GameObject canvas; // ��\���ɂ��Ă���Canvas
    public GameObject[] panels; // 4�̃p�l�����i�[����z��
    public GameObject firstButton; // �ŏ��Ƀt�H�[�J�X�𓖂Ă����{�^��


    public GameObject m_camera;
    // �R���g���[���[����
    ControllerState m_State;
    ControllerBase m_Controller;
    ControllerBase.ControllerButton m_Button;

    private FadeController fadeController;

    bool isActive = false;
    bool OnOFFFlg = false;
    private void Start()
    {
        m_State = m_camera.GetComponent<ControllerState>();
        m_Controller = m_camera.GetComponent<ControllerBase>();

        fadeController = FindObjectOfType<FadeController>();
    }

    void Update()
    {
        if (fadeController != null && fadeController.isFading) // �t�F�[�h���͏����𖳌���
        {
            return;
        }

        m_Button = m_Controller.GetButton();
        // Enter�L�[�������ꂽ�ꍇ�܂��̓R���g���[���[�̃{�^���������ꂽ�ꍇ
        if (m_State.GetButtonMenu())
        {
            OnOFFFlg = true;
        }
        ONToOFF();
    }
    private void ONToOFF()
    {
        if(OnOFFFlg)
        {
            if (m_State.GetButtonDoNot())
            {
                TogglePause();
                OnOFFFlg = false;
            }
        }
    }

    private void TogglePause()
    {
        if (canvas != null && panels.Length == 4) // �p�l����4�o�^����Ă���ꍇ
        {
            // ���݂̃A�N�e�B�u��Ԃ𔽓]������
            isActive = canvas.activeSelf;
            canvas.SetActive(!isActive);

            if (isActive) // �\����Ԃ�������
            {
                Time.timeScale = 1.0f; // ���Ԃ��ĊJ
            }
            else // ��\����������
            {
                Time.timeScale = 0.0f; // ���Ԃ��~

                // �p�l���̐���
                panels[0].SetActive(true); // 1�ڂ̃p�l����\��
                panels[1].SetActive(false); // 2�ڂ��\��
                panels[2].SetActive(false); // 3�ڂ��\��
                panels[3].SetActive(false); // 4�ڂ��\��

                // �t�H�[�J�X���ŏ��̃{�^���ɐݒ�
                SetButtonFocus(firstButton);
            }
        }
    }

    private void SetButtonFocus(GameObject button)
    {
        if (button != null)
        {
            // ���݂̑I�����������ĐV�����{�^����I��
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(button);
        }
    }
}
