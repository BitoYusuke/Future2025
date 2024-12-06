using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonLight : MonoBehaviour
{
    public GameObject moonLight;
    public GameObject directionalLight;

    DirectionLightRotate obj;

    private void Start()
    {
        //�R���|�[�l���g�擾
        obj = directionalLight.GetComponent<DirectionLightRotate>();
    }

    void Update()
    {
        //�擾�o���Ă��Ȃ��Ȃ�ēx�擾
        if(obj == null)
            obj = directionalLight.GetComponent<DirectionLightRotate>();

        //��Ȃ�ON,���Ȃ�OFF
        if (obj.GetNight())
            moonLight.SetActive(true);
        else
            moonLight.SetActive(false);
    }
}
