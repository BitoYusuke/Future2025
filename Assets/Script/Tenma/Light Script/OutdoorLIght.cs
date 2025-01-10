using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutdoorLIght : MonoBehaviour
{
    private DayWeatherManager Manager;

    public new Light light;

    public Material material;

    [Header("�_�ŉ�")]
    [Range(0,5)]
    public int blinkCount = 3;

    [Header("�_�ŊԊu")]
    [Range(0,1)]
    public float blinkInterval = 0.5f;

    [Header("���̋��x")]
    [Range(0, 5)]
    public float intensityIncrement = 1.0f;

    //���锻��p
    private bool bDecision = false;

    private Color emissionColor;
    private Color defaultColor;

    private void Start()
    {
        Manager = FindObjectOfType<DayWeatherManager>();

        ChangeEmissionColor(Color.white);
        defaultColor = emissionColor = material.GetColor("_EmissionColor");
        emissionColor *= Mathf.Pow(2.0f, intensityIncrement);
    }

    void Update()
    {
        //�������ɕς������
        if (Manager.GetCurrentTimeOfDay() == DayWeatherManager.TimeOfDay.Night 
            && Manager.GetBeforeTimeOfDay() != DayWeatherManager.TimeOfDay.Night)
        {
            //Light��_�ł����Ă���_����
            StartCoroutine(BlinkAndTurnOn());
        }

        //���̓��C�g������
        if(Manager.GetCurrentTimeOfDay() != DayWeatherManager.TimeOfDay.Night)
        {
            light.enabled = false;
            ChangeEmissionColor(defaultColor);
        }

        //�t���O�̍X�V
        //bDecision = Manager.GetNight();
    }

    private IEnumerator BlinkAndTurnOn()
    {
        for (int i = 0; i < blinkCount; i++)
        {
            light.enabled = !light.enabled;
            ChangeEmissionColor(emissionColor);
            yield return new WaitForSeconds(blinkInterval);
            light.enabled = !light.enabled;
            ChangeEmissionColor(defaultColor);
            yield return new WaitForSeconds(blinkInterval);
        }
        light.enabled = true;
        ChangeEmissionColor(emissionColor);
    }

    private void ChangeEmissionColor(Color c)
    {
        material.SetColor("_EmissionColor", c);
        material.EnableKeyword("_EMISSION");
    }
}
