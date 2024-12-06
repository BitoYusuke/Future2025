using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public List<GameObject> cameras = new List<GameObject>(); // �J�������i�[���郊�X�g
    private int camNum = 0;

    void Start()
    {
        // �ŏ��ɂ��ׂẴJ�������A�N�e�B�u��
        foreach (GameObject cam in cameras)
        {
            cam.SetActive(false);
        }

        // �ŏ��̃J�������A�N�e�B�u�ɐݒ�
        if (cameras.Count > 0)
        {
            cameras[camNum].SetActive(true);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // ���݂̃J�������A�N�e�B�u��
            cameras[camNum].SetActive(false);

            // ���̃J������
            camNum = (camNum + 1) % cameras.Count;

            // �V�����J�������A�N�e�B�u��
            cameras[camNum].SetActive(true);
        }
    }

    // �O������J������ǉ����邽�߂̃��\�b�h
    public void AddCamera(GameObject newCamera)
    {
        if (!cameras.Contains(newCamera))
        {
            cameras.Add(newCamera); // �V�����J���������X�g�ɒǉ�
            newCamera.SetActive(false); // �ǉ����ꂽ�J�������\���ɂ���
        }

        // �ŏ��̃J�������A�N�e�B�u��
        if (cameras.Count == 1)
        {
            cameras[0].SetActive(true);
        }
    }
}
