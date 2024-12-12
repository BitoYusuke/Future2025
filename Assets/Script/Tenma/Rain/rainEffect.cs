using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Es.InkPainter;

public class rainEffect : MonoBehaviour
{
    [SerializeField]
    private InkCanvas canvas;

    [SerializeField]
    private Brush brush;

    [SerializeField]
    private float minSize;

    [SerializeField]
    private float maxSize;

    [SerializeField, Range(0, 100)]
    private float aboutCallPercentOnFrame = 10;

    [SerializeField]
    private float radius = 0.2f;

    public float resetInterval = 4f; // ���Z�b�g�Ԋu�i�b���j
    private bool canReset = true; // ���Z�b�g�\���ǂ����̃t���O
    private int originalCullingMask; // ���̃J�����O�}�X�N��ێ�
    public string hiddenLayerName = "Rain"; // ��\���ɂ��郌�C���[��
    private Camera parentCamera; // �e�J�����ւ̎Q��
    private Material mat;
    private float IncrementCount = 0f;
    private float DecrementCount = 50f;
    private DayWeatherManager manager;

    private void Awake()
    {
        mat = this.gameObject.GetComponent<Renderer>().material;

        if(mat == null)
        {
            Debug.Log("Material��ǂݍ��߂܂���ł���");
        }
    }

    private void Start()
    {
        // �e�̃J�����R���|�[�l���g���擾
        parentCamera = GetComponentInParent<Camera>();

        // �e�J������������Ȃ��ꍇ�̓G���[���O��\��
        if (parentCamera == null)
        {
            Debug.LogError("�J������������܂���I");
            return;
        }

        // ���C���[��ݒ�
        gameObject.layer = LayerMask.NameToLayer(hiddenLayerName);
        originalCullingMask = parentCamera.cullingMask; // ���̃}�X�N��ۑ�

        manager = GameObject.FindWithTag("Manager").GetComponent<DayWeatherManager>();
        if(manager == null)
        {
            Debug.Log("Manager��������܂���ł���");
        }
    }

    private void Update()
    {
        //Debug.Log("��" + manager.GetCurrentWeather());
        //Debug.Log("����" + manager.GetFutureWeather());

        //�ŏ��̓J�E���g�A�b�v���Ė��Ȃ�
        //StartCoroutine(IncrementCountOverTime(1f));

        ////
        //if (manager.GetCurrentTimeOfDay() == DayWeatherManager.TimeOfDay.Night)
        //{
        //    StartCoroutine(DecrementCountOverTime(2f));
        //}

        ////�J�łȂ���΃J�E���g���Z�b�g
        //if (manager.GetCurrentWeather() != DayWeatherManager.Weather.Rainy)
        //{
        //    IncrementCount = 0;
        //    DecrementCount = 50;
        //}
    }


    private void OnWillRenderObject()
    {
        if (Random.Range(0f, 100f) > aboutCallPercentOnFrame)
            return;

        // ���Z�b�g�\�ȏꍇ�̂݉�ʃ��Z�b�g���J�n
        if (canReset)
        {
            StartCoroutine(ResetScreen());
        }
        else
        {
            brush.Color = new Color(255, 255, 255, 0);

            // ���Z�b�g����Ȃ��ꍇ�͒ʏ�̕`�揈�������s
            for (int i = Random.Range(1, 10); i > 0; --i)
            {
                brush.Scale = Random.Range(minSize, maxSize);

                // �����_���Ȉʒu�𐶐�
                Vector2 randomPosition = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));

                // ���S������̔��a���Ɉʒu���Ȃ��悤�ɂ���
                if (Vector2.Distance(randomPosition, new Vector2(0.5f, 0.5f)) > radius)
                {
                    canvas.PaintUVDirect(brush, randomPosition);
                }
                else
                {
                    i++; // �ēx�ʒu�𐶐�����
                }
            }
        }
    }



    private IEnumerator ResetScreen()
    {
        // ��ʂ̃��Z�b�g���������s
        brush.Scale = 1;
        canvas.PaintUVDirect(brush, new Vector2(0, 0));
        canvas.PaintUVDirect(brush, new Vector2(0, 1));
        canvas.PaintUVDirect(brush, new Vector2(0.5f, 0.5f));
        canvas.PaintUVDirect(brush, new Vector2(1, 0));
        canvas.PaintUVDirect(brush, new Vector2(1, 1));

        canReset = true; // ���Ԋu���o�߂���܂Ń��Z�b�g�𖳌��ɂ���
        yield return new WaitForSeconds(resetInterval); // ���Z�b�g�Ԋu�ҋ@
       
        canReset = false; // �Ăу��Z�b�g�\�ɂ���
    }


    public void RainMood(bool b) //false�͐���Atrue�͉J
    {
        if(b == false) parentCamera.cullingMask &= ~(1 << LayerMask.NameToLayer(hiddenLayerName));
        else parentCamera.cullingMask = originalCullingMask;
    }


    private IEnumerator IncrementCountOverTime(float timeInterval)
    {
        //�����J�łȂ���ΕԂ�
        if (manager.GetCurrentWeather() != DayWeatherManager.Weather.Rainy)
            yield break;

        //count��50�܂ł��
        if(IncrementCount >= 50f)
            yield break;

        while (true)
        {
            // �w�肵�����Ԃ����ҋ@
            yield return new WaitForSeconds(timeInterval);

            Debug.Log("�C���N�������g");
            Debug.Log(IncrementCount);

            // count��1�i�߂�
            IncrementCount++;
            mat.SetFloat("_BumpAmt", IncrementCount);
            //Debug.Log("IncrementCount: " + IncrementCount);
        }
    }


    private IEnumerator DecrementCountOverTime(float timeInterval)
    {
        //�����J�łȂ���ΕԂ�
        if(manager.GetCurrentWeather() != DayWeatherManager.Weather.Rainy)
            yield break;

        //�����J�A�������J�ł���ΕԂ�
        if (manager.GetFutureWeather() == DayWeatherManager.Weather.Rainy)
            yield break;

        //count��0�܂ł��
        if (DecrementCount <= 0f)
            yield break;

        //�����J�̎��������s
        while (true)
        {
            // �w�肵�����Ԃ����ҋ@
            yield return new WaitForSeconds(timeInterval);

            Debug.Log("�f�N�������g");

            // count��1�߂�
            DecrementCount--;
            mat.SetFloat("_BumpAmt", DecrementCount);
            //Debug.Log("DecrementCount: " + DecrementCount);
        }
    }
}
