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
    //タイムアップ表示時間
    private float timeUpTime;
    //タイムアップテキスト
    private Text timeUpText;

    //カウントダウンが終わったかどうか
    [System.NonSerialized]
    public bool isEndCountDown = true;

    //フェードスクリプト
    private Fade FadeScript;
    //フェードアウト中か
    private bool isFade = false;

    void Start()
    {
        StartQuitPanel();

        timerScript = GameObject.Find("Timer").GetComponent<timer>();
        itemCheckScript = this.GetComponent<ItemCheck>();
        if (SceneManager.GetActiveScene().name == "Game")
        {
            isEndCountDown = false;
            dDOLScript = GameObject.Find("DDOL").GetComponent<DDOL>();
            countDownText = GameObject.Find("CountDownText").GetComponent<Text>();
            timeUpText = GameObject.Find("TimeUpText").GetComponent<Text>();
            FadeScript = GameObject.Find("FadePanel").GetComponent<Fade>();
        }
    }

    void FixedUpdate()
    {
        if (GameObject.Find("Password").GetComponent<Canvas>().enabled == false)
        {
            SwitchQuitPanel();
        }

        if (SceneManager.GetActiveScene().name == "Game")
        {
            //フェードインが終わったら処理開始
            if(!FadeScript.fadeIn)
            {
                countDownTime -= Time.deltaTime;

                //countDownTimeの数値に応じてcountDownTextの内容を変更していく
                if (countDownTime <= 2.0f)
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
                    //非アクティブにする
                    countDownText.enabled = false;
                    //カウントダウンを終わらせる
                    isEndCountDown = true;
                }
            }
            
        }
        else
        {
            //ゲームシーン以外ではそもそもカウントダウンしないので最初からtrueにしておく
            isEndCountDown = true;
        }

        //タイムアップしたら
        if (timerScript.isTimeUp == true)
        {
            timeUpText.enabled = true;
            transitionTime += Time.deltaTime;
            this.GetComponent<AudioSource>().enabled = false;
            dDOLScript.getTreasurePercent = itemCheckScript.getTreasurePercent;
            if (transitionTime >= 2.0f)
            {
                //フェードアウト中じゃない場合
                if(!isFade)
                {
                    //フェードアウト開始
                    FadeScript.fadeOut = true;
                    //フェードアウト中
                    isFade = true;
                }
            }
            else
            {
                //フェードアウト中じゃない
                isFade = false;
            }

            //フェードアウト終了
            if (!FadeScript.fadeOut && isFade)
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
