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
