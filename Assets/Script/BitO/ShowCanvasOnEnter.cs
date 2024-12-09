using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCanvasOnEnter : MonoBehaviour
{
    public GameObject canvas; // ��\���ɂ��Ă���Canvas

    // �R���g���[���[����
    ControllerState m_State;

    private bool isPaused = false;

    private void Start()
    {
        // �R���g���[���[
        m_State = GetComponent<ControllerState>();
    }

    void Update()
    {
        // Enter�L�[�������ꂽ�ꍇ�܂��̓R���g���[���[�̃{�^���������ꂽ�ꍇ
        if (m_State.GetButtonMenu() || Input.GetKeyDown(KeyCode.A))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        if (!isPaused)
        {
            isPaused = true;

            // �|�[�Y��Ԃɂ���
            canvas.SetActive(true); // �|�[�Y��ʂ�\��
            Time.timeScale = 0.0f; // �Q�[�����Ԃ��~
        }
        else
        {
           isPaused = false;

            // �|�[�Y����
            canvas.SetActive(false); // �|�[�Y��ʂ��\��
            Time.timeScale = 1.0f; // �Q�[�����Ԃ��ĊJ
        }
    }

}
