using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Directional Light�ɃA�^�b�`����X�N���v�g�B
/// </summary>
[DisallowMultipleComponent]
[RequireComponent(typeof(Light))]
public class DayController : DayWeatherManager
{
    [Header("��]���x")]
    [SerializeField]
    private float rotate = 0.5f;

    [Header("���z�̌��̋���")]
    [SerializeField]
    private float[] TimeIntensity = { 1f, 1f, 1f, 1f };

    [Header("�܂ƉJ�̌��̌����l")]
    [SerializeField]
    private float[] DecayRateIntencity = { 0.8f, 0.5f };

    public static new DayController instance;
    private Light DirectionLight;
    private float totalIntencity = 1.0f;
    private float currentAngle;

    private void Awake()
    {
        //Debug.Log(currentAngle);
        //startangle = this.transform.rotation;
        // �C���X�^���X�����݂���ꍇ�͐V�����I�u�W�F�N�g���폜
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // �C���X�^���X�����݂��Ȃ��ꍇ�́A���̃I�u�W�F�N�g���C���X�^���X�Ƃ��Đݒ�
        instance = this;

        // ���[�g�I�u�W�F�N�g��DontDestroyOnLoad��K�p
        if (transform.parent == null) // ���[�g�I�u�W�F�N�g���m�F
        {
            DontDestroyOnLoad(this);
        }
        else
        {
            // ���[�g�I�u�W�F�N�g�łȂ���΃��[�g���擾
            GameObject rootObject = transform.root.gameObject;
            DontDestroyOnLoad(rootObject); // ���[�g�I�u�W�F�N�g�ɓK�p
        }
    }

    private void Start()
    {
        DirectionLight = this.GetComponent<Light>();
    }


    void Update()
    {
        //���z�̉�]
        currentAngle += rotate * rot * Time.deltaTime;
        if (currentAngle >= 360f) currentAngle -= 360f;

        transform.rotation = Quaternion.Euler(new Vector3(currentAngle, 0, 0));

        //����15�x��] //���̏o��0�x(6��)  //�^����90�x(12��)  //���̓��肪180�x(18��)
        if (currentAngle > 0.0f && currentAngle < 60.0f)
        {//6~10
            currentTimeOfDay = TimeOfDay.Morning;
            UpdateEnvironment();
        }
        else if (currentAngle > 60.0f && currentAngle < 150.0f)
        {//10~16
            currentTimeOfDay = TimeOfDay.Afternoon;
            UpdateEnvironment();
        }
        else if (currentAngle > 150.0f && currentAngle < 180.0f)
        {//16~18
            currentTimeOfDay = TimeOfDay.Evening;
            UpdateEnvironment();
        }
        else if (currentAngle > 180.0f && currentAngle < 360.0f)
        {//18~6
            currentTimeOfDay = TimeOfDay.Night;
            UpdateEnvironment();
        }

        //���z�̌��̋���
        DirectionLightIntencity();

        //���̋����̍X�V
        DirectionLight.intensity = totalIntencity;

        //Debug.Log(currentTimeOfDay);
        //Debug.Log(currentAngle);
        //Debug.Log(currentWeather);
        //Debug.Log(DirectionLight.intensity);
    }



    private void DirectionLightIntencity()
    {
        //���ԑѕʂɐݒ�
        switch (currentTimeOfDay)
        {
            //��
            case TimeOfDay.Morning:
                totalIntencity = TimeIntensity[0];
                break;

            //��
            case TimeOfDay.Afternoon:
                totalIntencity = TimeIntensity[1];
                break;

            //�[
            case TimeOfDay.Evening:
                totalIntencity = TimeIntensity[2];
                break;

            //��
            case TimeOfDay.Night:
                totalIntencity = TimeIntensity[3];
                break;
        }

        //�V�C�ʂɐݒ�
        switch (currentWeather)
        {
            //��
            case Weather.Sunny:
                break;

            //��
            case Weather.Cloudy:
                totalIntencity /= DecayRateIntencity[0];
                break;

            //�J
            case Weather.Rainy:
                totalIntencity /= DecayRateIntencity[1];
                break;
        }
    }
}
