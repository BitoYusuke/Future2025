using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    private FadeController m_fade;

    void Start()
    {
        m_fade = GetComponent<FadeController>();

        // �V�[���J�n���Ƀt�F�[�h�C�����s��
        StartCoroutine(FadeInAtSceneStart());
    }

    IEnumerator FadeInAtSceneStart()
    {
        // �t�F�[�h�C������
        yield return StartCoroutine(m_fade.FadeIn());
    }
}
