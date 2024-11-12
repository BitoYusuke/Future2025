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

    public float resetInterval = 4f; // ���Z�b�g�Ԋu�i�b���j
    private bool canReset = true; // ���Z�b�g�\���ǂ����̃t���O
    private int originalCullingMask; // ���̃J�����O�}�X�N��ێ�
    public string hiddenLayerName = "Rain"; // ��\���ɂ��郌�C���[��
    private Camera parentCamera; // �e�J�����ւ̎Q��

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
                canvas.PaintUVDirect(brush, new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f)));
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
}
