//各マネージャーの親クラス
//Autor:寺下琉生

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : QuitPanel
{
    //timerオブジェクト
    protected GameObject timerObj;
    //timerスクリプト
    protected timer timerScript;

    protected void StartManager()
    {
        StartQuitPanel();
        timerObj = GameObject.Find("Timer");

        if (SceneManager.GetActiveScene().name == "Tutorial"
            || SceneManager.GetActiveScene().name == "Game")
        {
            timerScript = timerObj.GetComponent<timer>();
        }
    }

    protected void UpdateManager()
    {
        if (GameObject.Find("Password").GetComponent<Canvas>().enabled == false)
        {
            SwitchQuitPanel();
        }
        //QuitPanelが非アクティブでパスワードパネルのキャンバスが非アクティブなら
        if (quitPanel.activeSelf == false && GameObject.Find("Password").GetComponent<Canvas>().enabled == false)
        {
            //マウスカーソルを非表示
            Cursor.visible = false;
        }
        else
        {
            //マウスカーソルを画面内に固定
            Cursor.lockState = CursorLockMode.Confined;

            //マウスカーソルを表示
            Cursor.visible = true;
        }
    }
}