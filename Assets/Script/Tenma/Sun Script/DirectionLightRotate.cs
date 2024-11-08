using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DirectionLightRotate : MonoBehaviour
{
    [Header("��]�p�x")]
    public float rotate = 0.5f;

    float rot = 1.0f;

    //���true
    bool bNight = false;

    public LightShaft lightShaft;

    public WaterEffect effect;

    [Header("�G�t�F�N�g����")]
    [SerializeField]
    private float EffectStrength = 0.001f;
    Quaternion startangle;
    Color sunColor;
    float defIntensity;
    Light sun;

    public static DirectionLightRotate instance;
    

    private void Awake()
    {
        //Debug.Log(this.transform.eulerAngles.x);
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
        sun = this.GetComponent<Light>();
        defIntensity = sun.intensity;

        sunColor = this.GetComponent<Light>().color;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game" || SceneManager.GetActiveScene().name == "Demo 1")
        {
            rot = 1.0f;
        }
        else
        {
            rot = 0.0f;
        }

        transform.Rotate(new Vector3(rotate * rot, 0, 0) * Time.deltaTime);

        //���锻�� ��ł����true
        if (this.transform.eulerAngles.x > 0.0f && this.transform.eulerAngles.x < 180.0f)
        {
            bNight = false;
            sun.intensity = defIntensity;
            effect.strength = EffectStrength;
        }
        else //��̊Ԃ�Intensity��0�ɂ���
        {
            bNight = true;
            sun.intensity = 0;
            effect.strength = 0f;
        }

        //�F���l����
        if (this.transform.eulerAngles.y > 90f && this.transform.eulerAngles.y < 200f)
        {
            if (sunColor.r != lightShaft.TargetColor.r) sunColor.r -= lightShaft.SubtractionSpeed;
            if (sunColor.g != lightShaft.TargetColor.g) sunColor.g -= lightShaft.SubtractionSpeed;
            if (sunColor.b != lightShaft.TargetColor.b) sunColor.b -= lightShaft.SubtractionSpeed;
        }
        else sunColor = lightShaft.retentionColor;
    }

    //���z���Ԏ~�߂�p�̊֐�
    //��:���Ԃ��ꎞ�I�Ɏ~�߂�����
    public void StopRotate()
    {
        rot = 0.0f;
    }


    //���z���ԍĊJ�p�̊֐�
    //��:���Ԏ~�߂���Ɏg�p
    public void StartRotate()
    {
        rot = 1.0f;
    }


    //���z�̉�]�p�x�ݒ�@��f�{��
    //��:��̎��Ԃ𑁂߂����Ƃ�
    public void SetRotate(float f)
    {
        rot = f;
    }

    //��̎���true��Ԃ�
    public bool GetNight()
    {
        return bNight;
    }

    public void ResetRotate()
    {
        Debug.Log("���Z�b�gaaaaaaaaaaaaaaaaa");
        this.transform.Rotate(90.0f,0.0f,0.0f);

    }
}
