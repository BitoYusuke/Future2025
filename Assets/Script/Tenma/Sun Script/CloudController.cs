using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : DayWeatherManager
{
    public Material mat;


    private void Start()
    {
        
    }


    void Update()
    {
        //天気毎で雲を変化
        switch(GetCurrentWeather())
        {

        }
    }
}
