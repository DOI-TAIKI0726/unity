using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    //フェードのスピード
    private float fadeSpeed = 0.02f;
    //RGBAの値
    private Color fadeColor;
    //フェードの状態
    [System.NonSerialized]
    public bool fadeIn = false, fadeOut = false;
    //フェードのパネル
    private Image fadePanel;

    // Start is called before the first frame update
    void Start()
    {
        //Imageの情報を取得
        fadePanel = GetComponent<Image>();
        //RGBAの情報を取得
        fadeColor = fadePanel.color;

        fadeIn = true;
    }

    // Update is called once per frame
    void Update()
    {
        //フェードイン
        if(fadeIn)
        {
            FadeInStart();
        }
        //フェードアウト
        else if(fadeOut)
        {
            FadeOutStart();
        }
    }

    //フェードインの処理
   void FadeInStart()
    {
        //アルファ値の減算
        fadeColor.a -= fadeSpeed;

        //色の更新
        SetColor();
        //アルファ値が0以下になったら
        if(fadeColor.a<=0)
        {
            fadeIn = false;
            //パネルのImageを非表示
            fadePanel.enabled = false;
        }
    }
    
    //フェードアウトの処理
    void FadeOutStart()
    {
        //パネルのImageを表示
        fadePanel.enabled = true;
        //アルファ値の加算
        fadeColor.a += fadeSpeed;

        //色の更新
        SetColor();
        //アルファ値が1以上になったら
        if(fadeColor.a>=1)
        {
            fadeOut = false;
        }
    }

    //色の更新処理
    void SetColor()
    {
        fadePanel.color = fadeColor;
    }
}
