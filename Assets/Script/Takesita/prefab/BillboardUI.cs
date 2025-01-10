using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardUI : MonoBehaviour
{
    private Transform cameraTransform;

    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        // �e�L�X�g�I�u�W�F�N�g����ɃJ�����̕����Ɍ�����
        transform.LookAt(cameraTransform);
        // Y���̉�]������ێ�
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y-180, 0);
    }
}
