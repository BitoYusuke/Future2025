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
        // Enter�L�[�������ꂽ�Ƃ���Canvas��\������
        if (m_State.GetButtonMenu() || Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("������Ă��܂�");
            canvas.SetActive(true); // Canvas�̃A�N�e�B�u��Ԃ�؂�ւ�
        }
    }
}
