using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCanvasOnEnter : MonoBehaviour
{
    public GameObject canvas; // ��\���ɂ��Ă���Canvas

    void Update()
    {
        // Enter�L�[�������ꂽ�Ƃ���Canvas��\������
        if (Input.GetKeyDown(KeyCode.A))
        {
            canvas.SetActive(!canvas.activeSelf); // Canvas�̃A�N�e�B�u��Ԃ�؂�ւ�
        }
    }
}
