using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMouseClick : MonoBehaviour
{
    // ���s���Ƀ}�E�X�̃N���b�N�𖳌��ɂ���t���O
    private bool disableMouse = true;

    void Update()
    {
        if (disableMouse)
        {
            // �}�E�X�̃N���b�N���͂𖳌���
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))
            {
                // �}�E�X���͂𖳎����邽�߁A�������Ȃ�
                Debug.Log("Mouse click disabled.");
            }
        }
    }

    // �K�v�ɉ����Ė�������؂�ւ�����悤�ɂ��郁�\�b�h
    public void SetMouseDisabled(bool disabled)
    {
        disableMouse = disabled;
    }
}
