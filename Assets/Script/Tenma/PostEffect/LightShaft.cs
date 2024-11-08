using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PostEffect/LightShaft", order = 1)]
public class LightShaft : PostEffectBase
{
    public enum SunShaftsResolution
    {
        Low = 0,
        Normal = 1,
        High = 2,
    }

    public enum ShaftsScreenBlendMode
    {
        Screen = 0,
        Add = 1,
    }

    [Header("�𑜓x")]
    public SunShaftsResolution resolution = SunShaftsResolution.Normal;

    [Header("�u�����h���[�h")]
    public ShaftsScreenBlendMode screenBlendMode = ShaftsScreenBlendMode.Screen;

    [Header("���z�̋���")]
    [Range(50, 1000)]
    public float distance = 100f;

    [Header("���ˏ�ڂ����̌J��Ԃ���")]
    [Range(1, 4)]
    public int radialBlurIterations = 2;

    [Header("���z�̌��F")]
    public Color retentionColor = Color.white;

    private Color sunColor = Color.white;

    [Header("���z��臒l")]
    public Color sunThreshold = new Color(0.87f, 0.74f, 0.65f);

    [Header("�[���̐F")]
    public Color TargetColor = Color.white;

    [Header("���Z���鑬�x")]
    [Range(0, 2)]
    public float SubtractionSpeed = 0.1f;

    [Header("�ڂ����̔��a")]
    [Range(1, 10)]
    public float sunShaftBlurRadius = 2.5f;

    [Header("���ˏ�̋��x")]
    [Range(1, 5)]
    public float sunShaftIntensity = 1.15f;

    [Header("�ő唼�a")]
    [Range(0, 5)]
    public float maxRadius = 0.75f;

    [Header("�[�x�e�N�X�`�����g�p")]
    public bool useDepthTexture = true;

    GameObject MainCamera;
    GameObject DirectionalLight;
    Transform sun;
    Camera cam;

    public void OnEnable()
    {
        //�J�����擾
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        if (MainCamera != null) cam = MainCamera.GetComponent<Camera>();

        DirectionalLight = GameObject.FindGameObjectWithTag("DirectionalLight");

        if (DirectionalLight != null) sun = DirectionalLight.GetComponent<Transform>();

        //�F��ێ�
        sunColor = retentionColor;
    }

    public override void OnCreate()
    {
        //�}�e���A���𐶐�
        material = new Material(Resources.Load<Shader>("Shaders/LightShaft"));
    }

    public override void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        // �J�����̑��݂��m�F
        if (cam == null)
        {
            // null�̏ꍇ�A�Ď擾
            MainCamera = GameObject.FindGameObjectWithTag("MainCamera");

            if (MainCamera != null) cam = MainCamera.GetComponent<Camera>();
        }

        //���z�̑��݂��m�F
        if(sun == null)
        {
            //null�̏ꍇ�A�Ď擾
            DirectionalLight = GameObject.FindGameObjectWithTag("DirectionalLight");

            if (DirectionalLight != null) sun = DirectionalLight.GetComponent<Transform>();
        }

        //�}�e���A���̑���
        if (material == null)
        {
            Debug.LogError("LightShaft��Material���Ȃ��您");
            Graphics.Blit(source, destination);
            return;
        }

        // �𑜓x�Ɋ�Â��X�P�[�����O
        int divider = (resolution == SunShaftsResolution.High) ? 1 : (resolution == SunShaftsResolution.Normal) ? 2 : 4;
        int rtW = source.width / divider;
        int rtH = source.height / divider;

        // �[�x�e�N�X�`����L����
        if (useDepthTexture)
        {
            cam.depthTextureMode |= DepthTextureMode.Depth;
        }

        // ���z�̈ʒu���r���[�|�[�g���W�Ŏ擾
        Transform sunTransform = GameObject.FindGameObjectWithTag("DirectionalLight").transform;
        Vector3 NegativeNormal = -sunTransform.forward;//Z���̃}�C�i�X�@�����擾
        sunTransform.position = NegativeNormal.normalized * distance; //�}�C�i�X�@��*�����̍��W���擾
        sunTransform.position += sunTransform.position;
        Vector3 sunViewportPos = sunTransform ? cam.WorldToViewportPoint(sunTransform.position) : new Vector3(0.5f, 0.5f, 0.0f);
        //ebug.Log(sunViewportPos);

        //���z�̐F���l����
        if (sun.eulerAngles.y > 90f && sun.eulerAngles.y < 200f)
        {
            if (sunColor.r != TargetColor.r) sunColor.r -= SubtractionSpeed;
            if (sunColor.g != TargetColor.g) sunColor.g -= SubtractionSpeed;
            if (sunColor.b != TargetColor.b) sunColor.b -= SubtractionSpeed;

            //sunColor = Color.Lerp(sunColor, TargetColor, SubtractionSpeed * Time.deltaTime);
        }
        else sunColor = retentionColor;

        //Debug.Log(sunColor);

        // �[�x�o�b�t�@
        var lrDepthBuffer = RenderTexture.GetTemporary(rtW, rtH, 0);
        material.SetVector("_SunPosition", new Vector4(sunViewportPos.x, sunViewportPos.y, sunViewportPos.z, maxRadius));
        material.SetVector("_SunThreshold", sunThreshold);

        // �[�x�e�N�X�`�����g�p���邩
        if (!useDepthTexture)
        {
            var format = cam.allowHDR ? RenderTextureFormat.DefaultHDR : RenderTextureFormat.Default;
            var tmpBuffer = RenderTexture.GetTemporary(source.width, source.height, 0, format);
            RenderTexture.active = tmpBuffer;
            GL.ClearWithSkybox(false, cam);

            material.SetTexture("_Skybox", tmpBuffer);
            Graphics.Blit(source, lrDepthBuffer, material, 3);
            RenderTexture.ReleaseTemporary(tmpBuffer);
        }
        else
        {
            Graphics.Blit(source, lrDepthBuffer, material, 2);
        }

        // ���ˏ�̃u����K�p
        float ofs = sunShaftBlurRadius * (1.0f / 768.0f);
        material.SetVector("_BlurRadius4", new Vector4(ofs, ofs, 0.0f, 0.0f));

        var lrColorB = RenderTexture.GetTemporary(rtW, rtH, 0);

        for (int it = 0; it < radialBlurIterations; it++)
        {
            Graphics.Blit(lrDepthBuffer, lrColorB, material, 1);
            ofs = sunShaftBlurRadius * (((it * 2.0f + 1.0f) * 6.0f)) / 768.0f;
            material.SetVector("_BlurRadius4", new Vector4(ofs, ofs, 0.0f, 0.0f));

            RenderTexture.ReleaseTemporary(lrDepthBuffer);
            lrDepthBuffer = RenderTexture.GetTemporary(rtW, rtH, 0);

            Graphics.Blit(lrColorB, lrDepthBuffer, material, 1);
            ofs = sunShaftBlurRadius * (((it * 2.0f + 2.0f) * 6.0f)) / 768.0f;
            material.SetVector("_BlurRadius4", new Vector4(ofs, ofs, 0.0f, 0.0f));
        }

        // ���ˏ�̍ŏI����
        material.SetVector("_SunColor", new Vector4(sunColor.r, sunColor.g, sunColor.b, sunColor.a) * sunShaftIntensity);
        material.SetTexture("_ColorBuffer", lrDepthBuffer);

        int blendMode = (screenBlendMode == ShaftsScreenBlendMode.Screen) ? 0 : 4;
        Graphics.Blit(source, destination, material, blendMode);

        RenderTexture.ReleaseTemporary(lrDepthBuffer);
        RenderTexture.ReleaseTemporary(lrColorB);
    }
}
