//ゲームシーン管理クラス
//Author:寺下琉生

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Manager
{
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
    //フェードスクリプト
    private Fade FadeScript;
    //フェードアウト中か
    private bool isFade = false;

    //カウントダウンが終わったかどうか
    [System.NonSerialized]
    public bool isEndCountDown = false;

    void Start()
    {
        StartManager();
         
        itemCheckScript = this.GetComponent<ItemCheck>();
        dDOLScript = GameObject.Find("DDOL").GetComponent<DDOL>();
        countDownText = GameObject.Find("CountDownText").GetComponent<Text>();
        timeUpText = GameObject.Find("TimeUpText").GetComponent<Text>();
        FadeScript = GameObject.Find("FadePanel").GetComponent<Fade>();
    }

    void FixedUpdate()
    {
        UpdateManager();

        //フェードインが終わったら処理開始
        if (FadeScript.fadeIn == false)
        {
            FadeScript.enabled = false;
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
        //タイムアップしたら
        if (timerScript.isTimeUp == true)
        {
            FadeScript.enabled = true;
            timeUpText.enabled = true;
            transitionTime += Time.deltaTime;
            this.GetComponent<AudioSource>().enabled = false;
            dDOLScript.getTreasurePercent = itemCheckScript.getTreasurePercent;
            if (transitionTime >= 2.0f)
            {
                //フェードアウト中じゃない場合
                if (isFade == false)
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
            if (FadeScript.fadeOut == false && isFade == true)
            {
                SceneManager.LoadScene("Result");
            }
        }
    }
}
