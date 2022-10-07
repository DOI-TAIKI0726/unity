//タイトル画面の管理
//Autor:寺下琉生

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    //タイトルロゴの移動速度
    [SerializeField]
    private float moveTitlelogoSpeed = 0.2f;
    //タイトルロゴの停止座標Y
    [SerializeField]
    private float stopTitlelogoPosY;
    //フェードする速度
    [SerializeField]
    private float fadeSpeed = 0.01f;

    //背景
    private GameObject background;
    //タイトルロゴ
    private GameObject titlelogo;
    //PressAnyKey
    private GameObject pak;
    //FadePanel
    private GameObject fadePanel;
    //タイトルロゴの初期位置
    private float defaultPos = 700.0f;
    //FadepanelのImage
    private Image panelImage;
    //パネルの色管理(赤、緑、青、透明度)
    public float red, green, blue, alpha;
    //タイトルロゴが止まったかどうか
    private bool isStop = false;
    //press any keyがtrueになった状態でキーを押したら
    private bool isInputKey = false;

    void Start()
    {
        //各要素にアクセス
        background = GameObject.Find("Background");
        titlelogo = GameObject.Find("Titlelogo");
        pak = GameObject.Find("press any key");
        fadePanel = GameObject.Find("FadePanel");

        //最初はpress any keyを見えなくする
        pak.SetActive(false);
        //タイトルロゴを初期位置に設定
        titlelogo.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.0f, defaultPos);
        //FadePanelのCollarを取得
        panelImage = fadePanel.GetComponent<Image>();
        //パネルの各色の設定
        red = panelImage.color.r;
        green = panelImage.color.g;
        blue = panelImage.color.b;
        alpha = panelImage.color.a;

    }

    void Update()
    {
        if (isStop == false)
        {
            //停止座標Yにタイトルロゴがないなら
            if (titlelogo.GetComponent<RectTransform>().anchoredPosition.y < stopTitlelogoPosY)
            {
                titlelogo.transform.Translate(0.0f, moveTitlelogoSpeed, 0.0f);
            }
            if (titlelogo.GetComponent<RectTransform>().anchoredPosition.y > stopTitlelogoPosY)
            {
                titlelogo.transform.Translate(0.0f, -moveTitlelogoSpeed, 0.0f);
            }

            //移動中に何かキーを押したら
            if (Input.anyKeyDown)
            {
                titlelogo.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.0f, stopTitlelogoPosY);
            }

            //ロゴが停止位置に来たら
            if (titlelogo.GetComponent<RectTransform>().anchoredPosition.y <= stopTitlelogoPosY)
            {
                isStop = true;
            }
        }

        //ロゴが止まったなら
        if (isStop == true)
        {
            //アクティブにする
            pak.SetActive(true);
        }

        //press any keyがアクティブなら
        if(pak.activeSelf == true)
        {
            //なにかキーを押したら
            if(Input.anyKeyDown)
            {
                isInputKey = true;
            }
        }

        if (isInputKey == true)
        {
            //アルファ値を加算していく
            alpha += fadeSpeed;
            SetCollar();
        }

        if(panelImage.color.a >= 1)
        {
            SceneManager.LoadScene("Game");
        }
    }

    //Collarの変更を反映する
    public void SetCollar()
    {
        panelImage.color = new Color(red, green, blue, alpha);
    }
}