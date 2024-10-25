using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    [SerializeField, Label("�V�[���J�ڐ�̖��O")]
    public string SceneName = "Game";

    private FadeController m_fade;

    void Start()
    {
        m_fade = GetComponent<FadeController>();
    }

    public void HideMenuAndStartFade()
    {
        StartCoroutine(m_fade.FadeOutAndChangeScene(SceneName));
    }
}
