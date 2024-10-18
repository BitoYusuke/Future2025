using UnityEngine;

public class RayVisualizer : MonoBehaviour
{
    public LineRenderer lineRenderer; // LineRenderer �R���|�[�l���g
    public Camera mainCamera; // ���C���΂��J����
    public float rayLength = 100f; // ���C�̒���
    private Vector2 cursorPosition;

    void Start()
    {
        // LineRenderer �̐ݒ�
        lineRenderer.positionCount = 2; // ���C�̊J�n�_�ƏI���_��2��
    }

    void Update()
    {
        // �J�[�\���ʒu���烌�C���΂�
        Ray ray = mainCamera.ScreenPointToRay(cursorPosition);

        // LineRenderer �Ƀ��C�̊J�n�_�ƏI���_��ݒ�
        lineRenderer.SetPosition(0, ray.origin); // ���C�̎n�_
        lineRenderer.SetPosition(1, ray.origin + ray.direction * rayLength); // ���C�̏I�_
    }

    public void SetCursorPosition(Vector2 position)
    {
        cursorPosition = position;
    }
}
