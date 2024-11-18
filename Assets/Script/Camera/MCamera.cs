using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class MCamera : MonoBehaviour
{
    [Header("�J����"), SerializeField]
    GameObject m_CameraObj;
    [Header("�^�[�Q�b�g"), SerializeField]
    GameObject m_TargetObj;
    [Header("��]�̑���"), SerializeField]
    private float m_Speed = 1;
    [Header("�Y�[���C���Y�[���A�E�g�̑���"), SerializeField]
    private float m_ZoomSpeed = 2;
    [Header("�^�[�Q�b�g�ƃJ�����̋���"), SerializeField]
    private float m_Distance = 2;
    [Header("�^�[�Q�b�g�ƃJ�����̃Y�[���̋߂�"), SerializeField]
    private float m_NearDistance = 1;
    [Header("�^�[�Q�b�g�ƃJ�����̃Y�[���̉���"), SerializeField]
    private float m_FarDistance = 5;

    [Header("�^�[�Q�b�g�܂ňړ����鑬��"), SerializeField]
    private float m_TargetSpeed = 0.5f;

    ControllerState m_State;
    ControllerBase m_controller;
    ControllerBase.ControllerButton m_button;

    Transform m_MainCamera;

    static public int m_Camera = 0;       //�J�����̔ԍ����i�[
    static public int m_Count = 0;
    //�␳�p
    private float m_Time = 0.0f;
    private float m_Delay = 0.1f;
    //���X�g
    private List<GameObject> m_CameraList;
    //�^�[�Q�b�g
    private List<GameObject> m_TargetList;
    //�J�����̏����l���i�[
    private List<GameObject> m_CameraStorageList;
    //�q�I�u�W�F�N�g�̐����i�[
    private int m_ChildCount;
    //�J�����̐؂�ւ��𒷉����ōs��Ȃ����߂̃t���O
    private bool m_CameraFlg = true;
    //�J�����̉�]
    private Vector2 m_Rotiton;
    //��]�����Ȃ��t���O
    private bool m_NoRotitonFlg;
    // ���݃J�����������Ă���^�[�Q�b�g
    private Transform m_CurrentTarget;
    //���Ɍ����^�[�Q�b�g
    private Transform m_NextTarget;
    //�^�[�Q�b�g�̍��W
    private Vector3 m_TargetPos;
    private bool m_LeapFlg = true;
    //�X�e�B�b�N�̏����i�[
    Vector2 m_Stick;
    void Start()
    {
        m_State = GetComponent<ControllerState>();
        m_controller = GetComponent<ControllerBase>();
        //���X�g������
        m_CameraList = new List<GameObject>();
        m_TargetList = new List<GameObject>();
        m_CameraStorageList = new List<GameObject>();
        //�q�I�u�W�F�N�g�̏������X�g�Ɋi�[
        foreach (Transform chlid in m_CameraObj.transform)
        {
            m_CameraList.Add(chlid.gameObject);
            m_CameraStorageList.Add(chlid.gameObject);
        }
        foreach (Transform chlid in m_TargetObj.transform)
        {
            m_TargetList.Add(chlid.gameObject); 
        }

        m_MainCamera  = m_CameraList[0].transform.GetChild(0);

        m_ChildCount = m_TargetObj.transform.childCount;
        m_CurrentTarget = m_TargetList[0].transform;
        m_NextTarget = m_TargetList[0].transform;
        m_NoRotitonFlg = true;
    }
    void Update()
    {
        if (m_CameraList == null)
            return;
        m_button = m_controller.GetButton();
        m_Stick = m_controller.GetStick();
        //�J�s�o��������������s(�d���Ȃ�����ʂ̕��@���l����)
        if(m_TargetObj.transform.childCount > m_ChildCount)
        {
            m_TargetList.Clear();
            foreach (Transform chlid in m_TargetObj.transform)
            {
                m_TargetList.Add(chlid.gameObject);
            }
            m_ChildCount = m_TargetObj.transform.childCount;
        }

        if (m_Delay <= m_Time && m_CameraFlg)
        {
            //�ړ����Ȃ瑀����󂯕t���Ȃ�
            if (m_LeapFlg)
            {
                if (m_State.GetButtonR())
                {
                    m_Count++;
                    //�J�����̐��𒴂�����O�ɕ␳
                    if (m_Count >= m_CameraList.Count)
                        m_Count = 0;
                    //CameraReSet();
                }
                if (m_State.GetButtonL())
                {
                    m_Count--;
                    //-1�ɂȂ�����J�����̐��ɕ␳
                    if (m_Count <= -1)
                        m_Count = m_CameraList.Count - 1;
                    //CameraReSet();
                }
            }
            //���̃^�[�Q�b�g������
            m_NextTarget = m_TargetList[m_Count].transform;
            m_Time = 0.0f;
            m_CameraFlg = false;
        }
        //�{�^����DoNot�ɖ߂�����t���O��true�ɂ���
        if (m_button == ControllerBase.ControllerButton.DoNot)
            m_CameraFlg = true;

       

        //�^�[�Q�b�g�܂ŃJ�������ړ�
        TargetSwitch();

        //�J��������]������
        CameraRotation();
        //�Y�[���C���Y�[���A�E�g
        TargetScaling();
        //�J�����ɐ؂�ւ�
        //CameraSwitch();
        m_Time += Time.deltaTime;
        Debug.Log(m_Count);
    }
    private void CameraSwitch()
    {
        for (int i = 0; i < m_CameraList.Count; i++)
        {
            m_CameraList[i].SetActive(false);
        }
        m_CameraList[m_Count].SetActive(true);
    }
    private void TargetScaling()
    {
        //�Y�[���C��
        if(m_State.GetButtonX() && m_Distance >= m_NearDistance)
        {
            m_Distance -= m_ZoomSpeed * Time.deltaTime;
        }
        //�Y�[���A�E�g
        if(m_State.GetButtonY() && m_Distance <= m_FarDistance)
        {
            m_Distance += m_ZoomSpeed * Time.deltaTime;
        }
    }
    private void TargetSwitch()
    {
        //�^�[�Q�b�g�؂�ւ����ɃX���[�Y�Ɉړ�����
        if(m_CurrentTarget != m_NextTarget)
        {
            m_TargetList.Clear();
            foreach (Transform chlid in m_TargetObj.transform)
            {
                m_TargetList.Add(chlid.gameObject);
            }

            //�J�����̈ʒu���^�[�Q�b�g�Ɉړ�
            if (m_LeapFlg)
            {
                m_TargetPos = m_NextTarget.transform.position
                - (Quaternion.Euler(m_Rotiton.x, m_Rotiton.y, 0.0f)
                * Vector3.forward
                /** m_Distance*/);
                m_LeapFlg = false;
            }

            //Vector3 direction = (m_TargetPos - m_CameraList[0].transform.position).normalized;
            //m_CameraList[0].transform.position += direction * m_Speed * Time.deltaTime;

            m_CameraList[0].transform.position = 
                Vector3.MoveTowards(m_CameraList[0].transform.position,
                m_TargetPos, m_TargetSpeed * Time.deltaTime);

            
            //�^�[�Q�b�g�ɋ߂Â�����^�[�Q�b�gif�����ʂ���
            if (Vector3.Distance(m_CameraList[0].transform.position, m_TargetPos) <=  0.1f)
            {
                m_CurrentTarget = m_NextTarget;
                m_LeapFlg = true;
            }
        }

    }
    void CameraRotation()
    {
        //�ړ����Ȃ瑀����󂯕t���Ȃ�
        if (!m_LeapFlg)
            return;

        if (m_Stick.magnitude > 0.1f)
            Debug.Log("�����Ă�");
        if(m_NoRotitonFlg)
        {
            m_Rotiton.x += m_Stick.y * m_Speed * Time.deltaTime;
            m_Rotiton.y += m_Stick.x * m_Speed * Time.deltaTime;
        }

        //���������݂���
        m_Rotiton.x = Mathf.Clamp(m_Rotiton.x, -75.0f, 75.0f);
        Quaternion rotationQuaternion = Quaternion.Euler(-m_Rotiton.x, -m_Rotiton.y, 0.0f);
       
        Debug.Log(m_Rotiton);
        m_MainCamera.transform.rotation = rotationQuaternion;

        Vector3 Pos = m_TargetList[m_Count].transform.position -(rotationQuaternion * Vector3.forward * m_Distance);
        //�J�������^�[�Q�b�g�Ɍ�����
        m_MainCamera.transform.position = Pos;
        m_MainCamera.transform.LookAt(m_TargetList[m_Count].transform);
    }
    void CameraReSet()
    {
        //�J�����̉�]����������
        for (int i = 0; i < m_CameraList.Count; i++)
        {
            m_CameraList[i].gameObject.transform.rotation = m_CameraStorageList[i].gameObject.transform.rotation;
        }
    }

    public void GetCursorCamera(GameObject gameObject)
    {
        for(int i = 0; i < m_TargetList.Count;i++)
        {
            if (m_TargetList[i].gameObject == gameObject)
            {
                m_Count = i;
                //���̃^�[�Q�b�g������
                m_NextTarget = m_TargetList[m_Count].transform;
                m_Time = 0.0f;
                m_CameraFlg = false;
            }
            
        }


    }
}


