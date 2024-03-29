﻿//タイトルシーン管理クラス
//Autor:寺下琉生

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : Manager
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
    //タイトルからチュートリアルに遷移する時の音
    [SerializeField]
    private AudioClip SE_0;
    
    //背景
    private GameObject background;
    //タイトルロゴ
    private GameObject titlelogo;
    //PressAnyKey
    private GameObject pak;
    //FadePanel
    private GameObject fadePanel;
    //AudioSource
    private AudioSource audioSource;
    //タイトルロゴの初期位置
    private float defaultPos = 700.0f;
    //FadepanelのImage
    private Image panelImage;
    //パネルの色管理(赤、緑、青、透明度)
    private float red, green, blue, alpha;
    //タイトルロゴが止まったかどうか
    private bool isStopTitleLogo = false;
    //press any keyがtrueになった状態でキーを押したら
    private bool isInputKey = false;
    //タイトルロゴ止まってから一瞬待ち時間を入れる
    private float stopLogoTime = 0.0f;

    void Start()
    {
        StartQuitPanel();

        //各要素にアクセス
        background = GameObject.Find("Background");
        titlelogo = GameObject.Find("Titlelogo");
        pak = GameObject.Find("press any key");
        fadePanel = GameObject.Find("FadePanel");
        audioSource = this.GetComponent<AudioSource>();
        //press any keyを非アクティブにしておく
        pak.SetActive(false);
        //QuitPanelを非アクティブにしておく
        quitPanel.SetActive(false);
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
        SwitchQuitPanel();

        //QuitPanelが非アクティブなら
        if(quitPanel.activeSelf == false)
        {
            //タイトルロゴが止まっていないなら
            if (isStopTitleLogo == false)
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
                    //押したキーがescapeじゃないなら
                    if (Input.GetKey(KeyCode.Escape) == false)
                    {
                        //マウスの左右クリックのどちらも押されていないなら
                        if (Input.GetMouseButton(0) == false && Input.GetMouseButton(1) == false)
                        {
                            //タイトルロゴのPosYをstopTitlelogoPosYと同じにする
                            titlelogo.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.0f, stopTitlelogoPosY);
                        }
                    }
                }

                //タイトルロゴが停止位置に来たら
                if (titlelogo.GetComponent<RectTransform>().anchoredPosition.y <= stopTitlelogoPosY)
                {
                    stopLogoTime += Time.deltaTime;
                    //待ち時間を入れないと何かキーを押したときにすぐ遷移してしまうので一瞬だけ待つ
                    if (stopLogoTime >= 0.5f)
                    {
                        isStopTitleLogo = true;
                    }
                }
            }
        }

        //タイトルロゴが止まったなら
        if (isStopTitleLogo == true)
        {
            //アクティブにする
            pak.SetActive(true);
        }

        //press any keyがアクティブなら
        if (pak.activeSelf == true)
        {
            //なにかキーを押したら
            if (Input.anyKey)
            {
                //マウスの左右クリックのどちらも押されていないなら
                if (Input.GetMouseButton(0) == false && Input.GetMouseButton(1) == false)
                {
                    if (isInputKey == false)
                    {
                        audioSource.Stop();
                        audioSource.PlayOneShot(SE_0);
                        isInputKey = true;
                    }
                }
            }
        }

        if (isInputKey == true)
        {
            //アルファ値を加算していく
            alpha += fadeSpeed;
            SetCollar();

            //アルファ値が1を超えたら(完全に透過されなくなったら)
            if (panelImage.color.a >= 1)
            {
                SceneManager.LoadScene("Tutorial");
            }
        }
    }


    //Collarの変更を反映する
    public void SetCollar()
    {
        //変えた色の反映
        panelImage.color = new Color(red, green, blue, alpha);
    }
}