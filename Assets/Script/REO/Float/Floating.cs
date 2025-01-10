using UnityEngine;

public class Floating : MonoBehaviour
{
    public float waterLevel = -12.25f;  // ���ʂ̍���
    public float floatStrength = 3.0f;  // ���͂̋���
    public float damping = 0.5f;  // �����ł̒�R
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // �I�u�W�F�N�g�����ʂ�艺�ɂ���ꍇ�ɕ��͂�������
        if (transform.position.y < waterLevel)
        {
            float force = (waterLevel - transform.position.y) * floatStrength;
            rb.AddForce(Vector3.up * force, ForceMode.Acceleration);

            // �����ł̌��������i�����h�~�j
            rb.velocity = new Vector3(rb.velocity.x * (1 - damping), rb.velocity.y, rb.velocity.z * (1 - damping));
        }
    }
}
