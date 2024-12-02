using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;


/// <summary>
/// Directional Light�ɃA�^�b�`����X�N���v�g�B
/// </summary>
[DisallowMultipleComponent]
[RequireComponent(typeof(Light))]
public class WeatherController : DayWeatherManager
{
    [Serializable]
    public class WeatherWeight
    {
        public Weather weather;  // �V�C�̎��
        public float weight;     // �m���i�d�݁j
    }

    [Header("�V�C�Ǝ��Ԃ�\������UI�e�L�X�g")]
    [SerializeField]
    private Text text;

    [Header("�V�C���Ƃ̊m��")]
    public List<WeatherWeight> weatherWeights = new List<WeatherWeight>
    {
        new WeatherWeight { weather = Weather.Sunny, weight = 50.0f },
        new WeatherWeight { weather = Weather.Cloudy, weight = 30.0f },
        new WeatherWeight { weather = Weather.Rainy, weight = 20.0f }
    };

    private TimeOfDay beforeTimeOfDay;

    public static new WeatherController instance;

    private void Awake()
    {
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

        //UI�e�L�X�g
        weatherTimeText = text;
    }

    void Start()
    {
        beforeTimeOfDay = currentTimeOfDay;
    }


    void Update()
    {
        //Debug.Log("Current time of day: " + currentTimeOfDay);
        //Debug.Log("Before time of day: " + beforeTimeOfDay);

        //�邩�璩�ɕς��^�C�~���O�̂�
        if (beforeTimeOfDay == TimeOfDay.Night && currentTimeOfDay == TimeOfDay.Morning)
        {
            //Debug.Log("�V�C�\��");
            SetRandomWeather();
            //Debug.Log(currentWeather);
        }

        beforeTimeOfDay = currentTimeOfDay;
    }

    // �V�C�������_���ɐݒ�
    public void SetRandomWeather()
    {
        // �ݐϏd�݂̍��v���v�Z
        float totalWeight = weatherWeights.Sum(w => w.weight);

        // 0���獇�v�d�݂͈̔͂ŗ����𐶐�
        float randomValue = UnityEngine.Random.Range(0, totalWeight);

        // �d�݂͈̔͂Ɋ�Â��ēV�C��I�o
        float cumulativeWeight = 0;
        foreach (var weatherWeight in weatherWeights)
        {
            cumulativeWeight += weatherWeight.weight;
            if (randomValue < cumulativeWeight)
            {
                currentWeather = weatherWeight.weather;
                break;
            }
        }

        UpdateEnvironment();
    }
}
