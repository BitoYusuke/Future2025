using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneTransition : MonoBehaviour
{
    [SerializeField, Label("�V�[���J�ڐ�̖��O")]
    public string SceneName = "Title";

    public GameObject menuCanvas;   // Canvas(menu)�������Ɋ��蓖�Ă܂�
    private FadeController m_fade;

    void Start()
    {
        m_fade = GetComponent<FadeController>();
    }

    public void HideMenuAndStartFade()
    {
        Time.timeScale = 1.0f; //�J�n

        menuCanvas.SetActive(false);
        StartCoroutine(m_fade.FadeOutAndChangeScene(SceneName));
    }
}
