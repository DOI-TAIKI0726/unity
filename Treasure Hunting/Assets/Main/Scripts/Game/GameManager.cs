//ゲームシーン管理クラス
//Author:寺下琉生

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : QuitPanel
{
    //timerスクリプト
    private timer timerScript;
    //ItemCheckスクリプト
    private ItemCheck itemCheckScript;
    //DDOLスクリプト
    private DDOL dDOLScript;

    void Start()
    {
        StartQuitPanel();

        timerScript = GameObject.Find("Timer").GetComponent<timer>();
        itemCheckScript = this.GetComponent<ItemCheck>();
        dDOLScript = GameObject.Find("DDOL").GetComponent<DDOL>();
    }

    void Update()
    {
        if (GameObject.Find("Password").GetComponent<Canvas>().enabled == false)
        {
            SwitchQuitPanel();
        }

        //タイムアップしたら
        if(timerScript.isTimeUp == true)
        {
            this.GetComponent<AudioSource>().enabled = false;
            dDOLScript.getTreasurePercent = itemCheckScript.getTreasurePercent;
            SceneManager.LoadScene("Result");
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
