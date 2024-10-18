using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSwitcher : MonoBehaviour
{
    public GameObject MainPanel;      // MainPanel��GameObject
    public GameObject Panel_Button_3; // Panel_Button_3��GameObject

    // Button_3���N���b�N���ꂽ���ɌĂяo����郁�\�b�h
    public void SwitchPanels()
    {
        // MainPanel���\��
        MainPanel.SetActive(false);

        // Panel_Button_3��\��
        Panel_Button_3.SetActive(true);
    }
}
