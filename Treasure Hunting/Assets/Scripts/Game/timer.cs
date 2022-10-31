using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    [SerializeField]
    private int minute = 3;//分
    [SerializeField]
    private int changeColSecond = 30; //色を変え始める秒数

    private float seconds;　　//秒
    private float oldSeconds;//前のUpdateの時の秒数
    private Text timerText;　//タイマー表示用テキスト
    private Color textColor; //文字の色変更用

    //GameManager
    private GameManager gameManagerScript;

    void Start()
    {
        seconds = 0f;
        oldSeconds = 0f;
        timerText = GetComponent<Text>();
        //赤くする
        textColor = new Color(1, 0, 0, 1);

        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    //aaaaa
    void Update()
    {
        if (gameManagerScript.quitPanel.activeSelf == false)
        {
            //毎秒引いていく
            seconds -= Time.deltaTime;

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
        }
    }
}