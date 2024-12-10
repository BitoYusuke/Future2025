using UnityEngine;
using Es.WaveformProvider;
public class FloatingSphere : MonoBehaviour
{
    public Transform waterSurface; // Plane��Transform���w��
    public float buoyancyForce = 10f; // ���͂̋���
    public float dampingFactor = 0.5f; // ��R��
    private Rigidbody rb;
    private float sphereRadius;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SphereCollider sphereCollider = GetComponent<SphereCollider>();
        sphereRadius = sphereCollider.radius * transform.localScale.y; // �X�P�[�����l���������a
    }

    void FixedUpdate()
    {
        // ���ʂ̍������擾
        float waterHeight = waterSurface.position.y;

        // Sphere�̒ꕔ�̈ʒu (���S - ���a)
        float sphereBottom = transform.position.y - sphereRadius;

        // Sphere�����ʉ��ɂ���ꍇ�ɕ��͂�������
        if (sphereBottom < waterHeight)
        {
            float depth = waterHeight - sphereBottom;

            // ���͌v�Z (�[���ɔ��)
            Vector3 buoyancy = Vector3.up * buoyancyForce * depth;

            // ��R��
            Vector3 drag = -rb.velocity * dampingFactor;

            // Rigidbody�ɗ͂�������
            rb.AddForce(buoyancy + drag, ForceMode.Force);
        }
    }
}
