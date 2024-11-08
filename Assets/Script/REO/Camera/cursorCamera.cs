using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class cursorCamera : MonoBehaviour
{
    ControllerState m_State;
    ControllerBase m_Stick;

    public GameObject cursor; // �J�[�\���Ƃ��Ďg�p����UI�I�u�W�F�N�g
    public float cursorSpeed = 100f; // �J�[�\���̈ړ����x
    public Camera mainCamera; // ���C���J����
    public float cameraDistance = 2f; // �I�u�W�F�N�g�̖ڂ̑O�̋���

    private bool isCursorVisible = false;
    private bool isCursorEnabled = true; // �J�[�\���̑����L���ɂ��邩�ǂ����̃t���O
    private Vector2 cursorPosition;
    private GameObject selectedObject = null; // �I�����ꂽ�I�u�W�F�N�g

    void Start()
    {
        cursor.SetActive(false); // �Q�[���J�n���̓J�[�\�����\����
        cursorPosition = new Vector2(Screen.width / 2, Screen.height / 2); // �J�[�\���̏����ʒu����ʒ����ɐݒ�
                                                                           // cursor���ݒ肳��Ă��邩�m�F
        m_State = GetComponent<ControllerState>();
        m_Stick = GetComponent<ControllerBase>();
    }

    void Update()
    {
        if (mainCamera == null)
        {
            Debug.LogError("mainCamera ���ݒ肳��Ă��܂���I");
        }

        if (cursor == null)
        {
            Debug.LogError("cursor ���ݒ肳��Ă��܂���I");
        }

        // �Q�[���p�b�h��B�{�^���������ꂽ��J�[�\���̕\���E��\����؂�ւ���
        if (Gamepad.current != null && m_State.GetButtonB() || Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            isCursorVisible = !isCursorVisible;
            cursor.SetActive(isCursorVisible); 
            isCursorEnabled = true; 
        }

        // �J�[�\�����\������Ă���ꍇ�A���X�e�B�b�N�ňړ�
        if (isCursorEnabled && isCursorVisible)
        {
            Vector2 leftStickInput = Vector2.zero;

            if (Gamepad.current != null)
            {
                leftStickInput = m_Stick.GetStick();
            }

            // �L�[�{�[�h��WASD����
            if (Keyboard.current != null)
            {
                if (Keyboard.current.wKey.isPressed)
                    leftStickInput.y += 1;
                if (Keyboard.current.sKey.isPressed)
                    leftStickInput.y -= 1;
                if (Keyboard.current.aKey.isPressed)
                    leftStickInput.x -= 1;
                if (Keyboard.current.dKey.isPressed)
                    leftStickInput.x += 1;
            }

            MoveCursor(leftStickInput);

            // �����ŃJ�[�\���̈ʒu�Ɋ�Â��ă��C���X�V
            SelectObjectUnderCursor();

            // A�{�^���܂���Enter�L�[�������ꂽ�ꍇ�A�J������I�����ꂽ�I�u�W�F�N�g�̑O�Ɉړ�
            if (selectedObject != null && (Gamepad.current != null && m_State.GetButtonA() || Keyboard.current.zKey.wasPressedThisFrame))
            {
                MoveCameraToSelectedObject();
                DisableCursorControl(); // �J�[�\������𖳌��ɂ���
            }
        }
    }

    void MoveCursor(Vector2 leftStickInput)
    {
        // ���X�e�B�b�N�̓��͂ɉ����ăJ�[�\�����ړ�
        cursorPosition += leftStickInput * cursorSpeed * Time.deltaTime;

        // ��ʂ̋��E���ɃJ�[�\���𐧌�
        cursorPosition.x = Mathf.Clamp(cursorPosition.x, 0, Screen.width);
        cursorPosition.y = Mathf.Clamp(cursorPosition.y, 0, Screen.height);

        // �J�[�\���̈ʒu��UI�I�u�W�F�N�g�ɔ��f
        cursor.transform.position = cursorPosition;
    }

    void SelectObjectUnderCursor()
    {
        // �J�[�\���ʒu���烌�C���΂��ăI�u�W�F�N�g��I��
        Ray ray = Camera.main.ScreenPointToRay(cursorPosition);

        // ���C�̔��ˈʒu��������ɒ����i�Ⴆ�΁A0.5�P�ʏ�Ɂj
        ray.origin += Vector3.up * 5.0f; // 0.5 �̕����͓K�X����

        RaycastHit hit;

        // ���C����ʏ�ɕ`��i����1, �F�͐j
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.blue);

        if (Physics.Raycast(ray, out hit))
        {
            selectedObject = hit.collider.gameObject;
        }
        else
        {
            selectedObject = null;
        }
    }

    void MoveCameraToSelectedObject()
    {
        if (selectedObject != null)
        {
            StartCoroutine(MoveCameraCoroutine());
        }
    }

    IEnumerator MoveCameraCoroutine()
    {
        Vector3 startPosition = mainCamera.transform.position;
        Vector3 direction = (mainCamera.transform.position - selectedObject.transform.position).normalized;
        Vector3 targetPosition = selectedObject.transform.position + direction * cameraDistance;

        float elapsedTime = 0f;
        float duration = 5f; // 5�b�Ԃ����Ĉړ�

        while (elapsedTime < duration)
        {
            mainCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            mainCamera.transform.LookAt(selectedObject.transform.position); // �I�u�W�F�N�g����ɒ���
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = targetPosition; // �ŏI�ʒu���m���ɐݒ�
    }

    void DisableCursorControl()
    {
        // �J�[�\�����\���ɂ��đ���𖳌���
        isCursorVisible = false;
        cursor.SetActive(false);
        isCursorEnabled = false; // �J�[�\���̑���𖳌��ɂ���
    }

    //void OnDrawGizmos()
    //{
    //    if (Camera.main != null)
    //    {
    //        // �J�[�\���ʒu���烌�C���΂��ĕ`��
    //        Ray ray = Camera.main.ScreenPointToRay(cursorPosition);
    //
    //        // ���C�̎n�_�ƌ����Ɋ�Â���Gizmo�̃��C����`��
    //        Gizmos.color = Color.red;
    //        Gizmos.DrawRay(ray.origin, ray.direction * 100f);
    //    }
    //}
}
