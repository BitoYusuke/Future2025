using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feed : MonoBehaviour
{
    public bool isTargeted = false; // ���̃G�T���^�[�Q�b�g�ɂȂ��Ă��邩�ǂ���
    public bool isEaten = false; // �G�T���H�ׂ�ꂽ���ǂ���

    // �G�T���n�ʂɓ��B�������ǂ����̔���
    public bool HasLanded()
    {
        return transform.position.y <= 0.1f; // �n�ʂɐG�ꂽ�Ɖ���
    }
}
