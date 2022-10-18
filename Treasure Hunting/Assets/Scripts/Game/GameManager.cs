//ゲームシーン管理クラス
//Author:寺下琉生

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : QuitPanel
{
    void Start()
    {
        StartQuitPanel();
    }

    void Update()
    {
        SwitchQuitPanel();

        //QuitPanelが非アクティブなら
        if (quitPanel.activeSelf == false)
        {

        }
    }
}
