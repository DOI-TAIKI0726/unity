﻿//ゲームシーン管理クラス
//Author:寺下琉生

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : QuitPanel
{
    //timerスクリプト
    private timer timerScript;
    //ItemCheckスクリプト
    private ItemCheck itemCheckScript;
    //DDOLスクリプト
    private DDOL dDOLScript;
    //シーン遷移にかける時間
    private float transitionTime = 0.0f;
    //最初のカウントダウンの秒数
    private float countDownTime = 3.0f;
    //CountDownText
    private Text countDownText;

    //カウントダウンが終わったかどうか
    [System.NonSerialized]
    public bool isEndCountDown = false;

    void Start()
    {
        StartQuitPanel();

        timerScript = GameObject.Find("Timer").GetComponent<timer>();
        itemCheckScript = this.GetComponent<ItemCheck>();
        dDOLScript = GameObject.Find("DDOL").GetComponent<DDOL>();
        countDownText = GameObject.Find("CountDownText").GetComponent<Text>();
    }

    void FixedUpdate()
    {
        if (GameObject.Find("Password").GetComponent<Canvas>().enabled == false)
        {
            SwitchQuitPanel();
        }

        countDownTime -= Time.deltaTime;

        if(countDownTime <= 2.0f)
        {
            countDownText.text = "2";
        }
        if (countDownTime <= 1.0f)
        {
            countDownText.text = "1";
        }
        if (countDownTime <= 0.0f)
        {
            countDownText.text = "0";
        }
        if (countDownTime <= -1.0f)
        {
            countDownText.gameObject.SetActive(false);
            isEndCountDown = true;
        }

        //タイムアップしたら
        if (timerScript.isTimeUp == true)
        {
            transitionTime += Time.deltaTime;
            this.GetComponent<AudioSource>().enabled = false;
            dDOLScript.getTreasurePercent = itemCheckScript.getTreasurePercent;
            if (transitionTime >= 2.0f)
            {
                SceneManager.LoadScene("Result");
            }
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
