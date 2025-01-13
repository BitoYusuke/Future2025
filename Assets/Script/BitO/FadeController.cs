using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    [SerializeField, Label("�t�F�[�h�p��Image")]
    public Image fadeImage;

    [SerializeField, Label("�t�F�[�h�ɂ����鎞��")]
    public float fadeDuration = 1.0f;

    private Button[] buttons;
    public bool isFading = false; // �t�F�[�h�����ǂ����������t���O

    private void Start()
    {
        buttons = FindObjectsOfType<Button>();

        // �Q�[���J�n���Ƀt�F�[�h�C�������s
        StartCoroutine(FadeIn());
    }

    // �{�^����L�����܂��͖��������郁�\�b�h
    private void SetButtonsInteractable(bool interactable)
    {
        foreach (var button in buttons)
        {
            button.interactable = interactable;
        }
    }

    // �t�F�[�h�C������
    public IEnumerator FadeIn()
    {
        isFading = true; // �t�F�[�h�J�n
        SetButtonsInteractable(false); // �t�F�[�h������Ƀ{�^����L����

        float timer = 0;
        Color color = fadeImage.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(1, 0, timer / fadeDuration); // Alpha�l��1����0�֏��X�ɕύX
            fadeImage.color = color;
            yield return null;
        }

        color.a = 0;
        fadeImage.color = color; // ���S�ɓ�����

        SetButtonsInteractable(true); // �t�F�[�h������Ƀ{�^����L����
        isFading = false; // �t�F�[�h�I��
    }

    // �t�F�[�h�A�E�g�����ƃV�[���J��
    public IEnumerator FadeOutAndChangeScene(string sceneName)
    {
        isFading = true; // �t�F�[�h�J�n
        SetButtonsInteractable(false); // �t�F�[�h���̓{�^���𖳌���

        float timer = 0;
        Color color = fadeImage.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(0, 1, timer / fadeDuration); // Alpha�l��0����1�֏��X�ɕύX
            fadeImage.color = color;
            yield return null;
        }

        color.a = 1;
        fadeImage.color = color; // ���S�ɕs������

        // �t�F�[�h�A�E�g������������V�[���J��
        SceneManager.LoadScene(sceneName);
        isFading = false; // �t�F�[�h�I��
    }
}
