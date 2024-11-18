using UnityEngine;
using Effekseer;

public class EffectLoop : MonoBehaviour
{
    // Effekseer Emitter ���A�^�b�`���� GameObject ���A�T�C��
    public EffekseerEmitter effect1;
    public EffekseerEmitter effect2;

    // �Đ��Ԋu�i�b�j
    public float interval = 2.0f;

    private float timer = 0.0f;
    private bool isPlayingEffect1 = true;

    void Update()
    {
        timer += Time.deltaTime;

        // ��莞�Ԃ��ƂɃG�t�F�N�g��؂�ւ���
        if (timer >= interval)
        {
            timer = 0.0f;

            if (isPlayingEffect1)
            {
                PlayEffect(effect2);
                //StopEffect(effect1);
            }
            else
            {
                PlayEffect(effect1);
                //StopEffect(effect2);
            }

            isPlayingEffect1 = !isPlayingEffect1;
        }
    }

    // �G�t�F�N�g���Đ�
    void PlayEffect(EffekseerEmitter emitter)
    {
        if (emitter != null)
        {
            emitter.Play();
        }
    }

    // �G�t�F�N�g���~
    void StopEffect(EffekseerEmitter emitter)
    {
        if (emitter != null)
        {
            emitter.Stop();
        }
    }
}
