using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnGauge : MonoBehaviour
{
    public Image spawnGauge; // UI�Q�[�W�iImage�j
    private KapibaraSpawner spawner; // AI�I�u�W�F�N�g�X�|�[�i�[

    void Start()
    {
        spawner = FindObjectOfType<KapibaraSpawner>(); // �X�|�[�i�[���擾
        if (spawnGauge != null && spawner != null)
        {
            spawnGauge.fillAmount = 0; // �����l��0�ɐݒ�
        }
    }

    void Update()
    {
        if (spawner != null && spawnGauge != null)
        {
            // �X�|�[���܂ł̎��Ԃ��v�Z
            float remainingTime = spawner.spawnInterval - (Time.time - (spawner.nextSpawnTime - spawner.spawnInterval));
            // �Q�[�W��FillAmount���X�V�i0����1�͈̔͂ɕϊ��j
            spawnGauge.fillAmount = Mathf.Clamp01(1 - (remainingTime / spawner.spawnInterval));
        }
    }
}
