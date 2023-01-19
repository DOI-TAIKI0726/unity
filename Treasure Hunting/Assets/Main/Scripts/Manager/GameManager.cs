//ゲームシーン管理クラス
//Author:寺下琉生

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Manager
{
    //カウントダウンSE
    [SerializeField]
    private AudioClip SE_0;

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
    //Audio
    private AudioSource audioSource;
    //カウントダウン用
    private bool isCnt3 = false;
    private bool isCnt2 = false;
    private bool isCnt1 = false;
    private bool isCnt0 = false;
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
        audioSource = GameObject.Find("SE_AudioSource").GetComponent<AudioSource>();
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
            if (countDownTime <= 3.0f && isCnt3 == false)
            {
                Debug.Log("3");
                audioSource.PlayOneShot(SE_0);
                countDownText.text = "3";
                isCnt3 = true;
            }

            if (countDownTime <= 2.0f && isCnt2 == false)
            {
                Debug.Log("2");
                audioSource.PlayOneShot(SE_0);
                countDownText.text = "2";
                isCnt2 = true;
            }
            if (countDownTime <= 1.0f && isCnt1 == false)
            {
                Debug.Log("1");
                audioSource.PlayOneShot(SE_0);
                countDownText.text = "1";
                isCnt1 = true;
            }
            if (countDownTime <= 0.0f && isCnt0 == false)
            {
                Debug.Log("0");
                audioSource.PlayOneShot(SE_0);
                countDownText.text = "0";
                isCnt0 = true;
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
