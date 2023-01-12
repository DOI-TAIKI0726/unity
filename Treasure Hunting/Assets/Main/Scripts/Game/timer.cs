using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class timer : MonoBehaviour
{
    [SerializeField]
    private int minute = 3;//分
    [SerializeField]
    private int changeColSecond = 30; //色を変え始める秒数

    private float seconds;　　//秒
    private float oldSeconds; //前のUpdateの時の秒数
    private Text timerText;   //タイマー表示用テキスト
    private Color textColor;  //文字の色変更用

    //オーディオソース
    private AudioSource audioSource;
    //GameManager
    private GameManager gameManagerScript;
    //TutorialManager
    private TutorialManager tutorialManagerScript;

    //タイムアップしたか
    [System.NonSerialized]
    public bool isTimeUp = false;

    //タイムアップSE
    public AudioClip timeUpSE;

    void Start()
    {
        seconds = 0f;
        oldSeconds = 0f;
        timerText = GetComponent<Text>();
        //赤くする
        textColor = new Color(1, 0, 0, 1);

        audioSource = this.GetComponent<AudioSource>();

        if (SceneManager.GetActiveScene().name == "Game")
        {
            gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            tutorialManagerScript = GameObject.Find("TutorialManager").GetComponent<TutorialManager>();
        }

        //タイマーの表示をminuteの値と揃える
        timerText.text = minute.ToString("0") + ":00";
    }

    void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            if (gameManagerScript.isEndCountDown == true)
            {
                //タイムアップ
                if (minute == 0 && seconds <= 0)
                {
                    audioSource.PlayOneShot(timeUpSE);
                    isTimeUp = true;
                }
                else
                {
                    if (gameManagerScript.quitPanel.activeSelf == false
                        || tutorialManagerScript.quitPanel.activeSelf == false)
                    {

                        //0秒以下になったら分を1引く
                        if (seconds <= 0f)
                        {
                            minute--;
                            seconds = 60f + seconds;
                        }

                        //　値が変わった時だけテキストUIを更新
                        if ((int)seconds != (int)oldSeconds)
                        {
                            timerText.text = minute.ToString("0") + ":" + ((int)seconds).ToString("00");
                        }

                        oldSeconds = seconds;

                        //00:30のとき文字を赤くする
                        if (minute == 0 && seconds <= changeColSecond)
                        {
                            timerText.color = textColor;
                        }

                        if (minute <= 0)
                        {
                            minute = 0;
                        }
                        if (seconds <= 0)
                        {
                            seconds = 0;
                        }
                        else
                        {
                            //毎秒引いていく
                            seconds -= Time.deltaTime;
                        }
                    }
                }
            }
        }
    }
}