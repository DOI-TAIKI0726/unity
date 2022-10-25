//エフェクトゲームシーン管理クラス
//Author:

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EffectGameManager : QuitPanel
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
