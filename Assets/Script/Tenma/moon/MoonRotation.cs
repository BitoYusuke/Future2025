using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonRotation : MonoBehaviour
{
    public Light directionalLight;  // �f�B���N�V�������C�g�i���z�j
    public Transform center;         // ������]���钆�S�i�n���j
    public float orbitDistance = 10f; // ���̋O�����a�i�n������̋����j

    void Update()
    {
        if (directionalLight != null && center != null)
        {
            // �f�B���N�V�������C�g�̉�]�������擾
            Vector3 lightDirection = directionalLight.transform.forward;

            // ���C�g�̌����̋t���Ɍ���z�u����ʒu���v�Z
            Vector3 moonPosition = center.position + lightDirection.normalized * orbitDistance;

            // ���̈ʒu���X�V
            transform.position = moonPosition;

            // ��Ɍ����n���������悤�ɉ�]���X�V
            transform.LookAt(center);
        }
    }
}

