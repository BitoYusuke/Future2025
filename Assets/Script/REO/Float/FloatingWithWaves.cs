using UnityEngine;
using Es.WaveformProvider;

public class FloatingWithWaves : MonoBehaviour
{
    public WaveConductor waveConductor; // �g�̃f�[�^��񋟂���X�N���v�g
    public Transform waterPlane;       // ���ʂ̊�i���ʃI�u�W�F�N�g�j
    public Rigidbody rb;               // �I�u�W�F�N�g�� Rigidbody
    public float floatHeight = 1f;     // ��������
    public float bounceDamp = 0.05f;   // �h��̌���

    private bool isAboveWater = true;  // ������Ԃł͐��ʂ̏�ɂ���Ɖ���

    void FixedUpdate()
    {
        if (waveConductor == null || waterPlane == null || rb == null)
            return;

        // ���݂̃I�u�W�F�N�g�ʒu
        Vector3 objectPos = transform.position;

        // ���ʂ̍������擾
        Vector3 waterPos = GetWaveHeightAtPosition(objectPos);

        if (isAboveWater)
        {
            // ���R�������iSphere�����ʂ���ɂ���ꍇ�j
            if (objectPos.y <= waterPos.y)
            {
                // ���ʂɓ��B�����畂�̓t�F�[�Y�Ɉڍs
                isAboveWater = false;
            }
        }
        else
        {
            // ���ʈȉ��ɂ���ꍇ�͕��͂�K�p
            float displacementMultiplier = Mathf.Clamp01((waterPos.y + floatHeight - objectPos.y) / floatHeight);
            Vector3 buoyancyForce = Vector3.up * displacementMultiplier * rb.mass * Physics.gravity.magnitude;
            rb.AddForce(buoyancyForce, ForceMode.Acceleration);

            // �h��̌���
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * (1f - bounceDamp), rb.velocity.z);
        }
    }

    Vector3 GetWaveHeightAtPosition(Vector3 position)
    {
        // ���[�J�����W���e�N�X�`��UV�ɕϊ�
        Vector3 localPos = waterPlane.InverseTransformPoint(position);
        float u = localPos.x / waterPlane.localScale.x + 0.5f;
        float v = localPos.z / waterPlane.localScale.z + 0.5f;

        // �g�f�[�^���獂�����擾
        RenderTexture.active = waveConductor.Output;
        Texture2D waveTexture = new Texture2D(waveConductor.Output.width, waveConductor.Output.height, TextureFormat.RGBA32, false);
        waveTexture.ReadPixels(new Rect(0, 0, waveTexture.width, waveTexture.height), 0, 0);
        waveTexture.Apply();

        int x = Mathf.Clamp((int)(u * waveTexture.width), 0, waveTexture.width - 1);
        int y = Mathf.Clamp((int)(v * waveTexture.height), 0, waveTexture.height - 1);

        float waveHeight = waveTexture.GetPixel(x, y).r; // �g�̍����͐ԃ`�����l���ŕ\�������Ɖ���
        Destroy(waveTexture);

        // �g�̍��������[���h���W�ɕϊ�
        return new Vector3(position.x, waveHeight * waterPlane.localScale.y, position.z);
    }
}
