using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainyManager : MonoBehaviour
{
    private DayWeatherManager manager;
    void Start()
    {
        //manager = GameObject.FindWithTag("Manager").GetComponent<DayWeatherManager>();
        manager = GetComponent<DayWeatherManager>();
        if(manager == null)
        {
            Debug.Log("Manager��������܂���");
        }
    }

    public GameObject RainEffect; // �J�̃G�t�F�N�g�p��GameObject
    public ParticleSystem RainParticle; // �J�̃p�[�e�B�N���V�X�e��

    // Update is called once per frame
    void Update()
    {
        if(manager.GetCurrentWeather() == DayWeatherManager.Weather.Rainy)
        {
            // �V�C��Rainy�̏ꍇ�Ƀp�[�e�B�N�����Đ�
            if (RainParticle != null && !RainParticle.isPlaying)
            {
                RainParticle.Play();
                Debug.Log("Rainy���: �J�̃p�[�e�B�N�����Đ�");
            }
        }

        else
        {
            if (RainParticle != null && RainParticle.isPlaying)
            {
                RainParticle.Stop();
                Debug.Log("Rainy�ł͂Ȃ�: �J�̃p�[�e�B�N�����~");
            }
        }   
    }
}