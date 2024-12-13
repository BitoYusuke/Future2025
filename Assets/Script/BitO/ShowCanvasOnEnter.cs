using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCanvasOnEnter : MonoBehaviour
{
    public GameObject canvas; // ��\���ɂ��Ă���Canvas

    public GameObject m_camera;
    // �R���g���[���[����
    ControllerState m_State;
    ControllerBase m_Controller;
    ControllerBase.ControllerButton m_Button;

    bool isActive = false;
    bool OnOFFFlg = false;
    private void Start()
    {
        m_State = m_camera.GetComponent<ControllerState>();
        m_Controller = m_camera.GetComponent<ControllerBase>();
        // �R���g���[���[
        //m_State = GetComponent<ControllerState>();
    }

    void Update()
    {
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
        if (canvas != null)
        {
            
            // ���݂̃A�N�e�B�u��Ԃ𔽓]������
            isActive = canvas.activeSelf;
            canvas.SetActive(!isActive);
            // �Q�[�����̎��Ԃ��~�܂��͍ĊJ
            if (isActive) // �\����Ԃ�������
            {
                Time.timeScale = 1.0f; // ���Ԃ��ĊJ
            }
            else // ��\����������
            {
                Time.timeScale = 0.0f; // ���Ԃ��~
            }
            
            
        }
    }

}
