using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCanvasOnEnter : MonoBehaviour
{
    public GameObject canvas; // ��\���ɂ��Ă���Canvas

    // �R���g���[���[����
    ControllerState m_State;

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
        if (canvas != null)
        {
            // ���݂̃A�N�e�B�u��Ԃ𔽓]������
            bool isActive = canvas.activeSelf;
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
