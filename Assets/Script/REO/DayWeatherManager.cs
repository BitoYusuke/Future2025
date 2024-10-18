using UnityEngine;
using UnityEngine.UI; // UI�e�L�X�g�̎g�p�ɕK�v

public class DayWeatherManager : MonoBehaviour
{
    public enum TimeOfDay
    {
        Morning,
        Afternoon,
        Evening,
        Night
    }

    public enum Weather
    {
        Sunny,
        Cloudy,
        Rainy
    }

    public TimeOfDay currentTimeOfDay { get; private set; }
    public Weather currentWeather { get; private set; }

    public Text weatherTimeText; // �V�C�Ǝ��Ԃ�\������UI�e�L�X�g

    void Start()
    {
        // �����l�̐ݒ�
        currentTimeOfDay = TimeOfDay.Morning;
        currentWeather = Weather.Sunny;

        UpdateEnvironment();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) // �L�[����Ŏ��ԑт�؂�ւ���
        {
            CycleTimeOfDay();
        }

        if (Input.GetKeyDown(KeyCode.W)) // �L�[����œV�C�������_���ɕύX����
        {
            SetRandomWeather();
        }
    }

    // ���ԑт����ԂɕύX
    public void CycleTimeOfDay()
    {
        currentTimeOfDay = (TimeOfDay)(((int)currentTimeOfDay + 1) % System.Enum.GetValues(typeof(TimeOfDay)).Length);
        UpdateEnvironment();
    }

    // �V�C�������_���ɐݒ�
    public void SetRandomWeather()
    {
        int weatherCount = System.Enum.GetValues(typeof(Weather)).Length;
        currentWeather = (Weather)Random.Range(0, weatherCount);
        UpdateEnvironment();
    }

    // ���̃X�N���v�g����V�C���Z�b�g���郁�\�b�h
    // ---------------�g�p��--------------------
    // ����ɐݒ�
    // DayWeaterManager weatherManager;
    // weatherManager.SetWeather(DayWeatherManager.Weather.Sunny);
    // -----------------------------------------
    public void SetWeather(Weather newWeather)
    {
        currentWeather = newWeather;
        UpdateEnvironment();
    }

    // ���̃X�N���v�g���玞�ԑт��Z�b�g���郁�\�b�h
    // ---------------�g�p��--------------------
    // ���Ԃ��ɐݒ�
    // DayWeaterManager weatherManager;
    // weatherManager.SetTimeOfDay(DayWeatherManager.TimeOfDay.Night);
    // -----------------------------------------
    public void SetTimeOfDay(TimeOfDay newTimeOfDay)
    {
        currentTimeOfDay = newTimeOfDay;
        UpdateEnvironment();
    }

    // ���݂̓V�C���擾���郁�\�b�h
    // ---------------�g�p��--------------------
    //  DayWeatherManager.Weather currentWeather = weatherManager.GetCurrentWeather();
    // -----------------------------------------
    public Weather GetCurrentWeather()
    {
        return currentWeather;
    }

    // ���݂̎��ԑт��擾���郁�\�b�h
    // ---------------�g�p��--------------------
    // DayWeatherManager.TimeOfDay currentTime = weatherManager.GetCurrentTimeOfDay();
    // -----------------------------------------
    public TimeOfDay GetCurrentTimeOfDay()
    {
        return currentTimeOfDay;
    }

    // �����X�V���郁�\�b�h
    void UpdateEnvironment()
    {
        Debug.Log("Current time of day: " + currentTimeOfDay);
        Debug.Log("Current weather: " + currentWeather);
        
        // �e�L�X�g�Ɍ��݂̓V�C�Ǝ��Ԃ�\��
        if (weatherTimeText != null)
        {
            weatherTimeText.text = "Time of Day: " + currentTimeOfDay + "\nWeather: " + currentWeather;
        }
        // ���ɉ������X�V�����i��F���C�g��G�t�F�N�g�̕ύX�Ȃǁj�������ɒǉ�

    }
}
