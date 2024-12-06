using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RayVisualizer : MonoBehaviour
{
    public float rayLength = 10f; // Ray�̒���
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2; // LineRenderer��2�̓_��ݒ�
        lineRenderer.enabled = true; // �ŏ��ɗL�������Ă���
    }

    void FixedUpdate()
    {
        Vector3 rayOrigin = transform.position; // Ray�̎n�_�i�I�u�W�F�N�g�̈ʒu�j
        Vector3 rayDirection = transform.forward; // Ray�̕����i�I�u�W�F�N�g�̑O���j

        // �n�_�ƃf�t�H���g�̏I�_��ݒ�
        lineRenderer.SetPosition(0, rayOrigin);
        lineRenderer.SetPosition(1, rayOrigin + rayDirection * rayLength);

        Ray ray = new Ray(rayOrigin, rayDirection);
        RaycastHit hit;

        // Ray���q�b�g�����ꍇ
        if (Physics.Raycast(ray, out hit, rayLength))
        {
            // �q�b�g�����ʒu�܂łɒ���
            lineRenderer.SetPosition(1, hit.point);
        }
    }
}
