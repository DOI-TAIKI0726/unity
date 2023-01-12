//QuitPanelの共通処理
//Author:寺下琉生

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//=======================
//オブジェクトに入れない
//=======================

public class QuitPanel : MonoBehaviour
{
    //QuitPanel
    [System.NonSerialized]
    public GameObject quitPanel;

    //各Managerでの初期化等処理
    protected void StartQuitPanel()
    {
        //各要素にアクセス
        quitPanel = GameObject.Find("QuitPanel");

        //QuitPanelを非アクティブにしておく
        quitPanel.SetActive(false);
    }

    //QuitPanel表示非表示切り替え
    protected void SwitchQuitPanel()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (quitPanel.activeSelf == false)
            {
                //QuitPanelをアクティブにする
                quitPanel.SetActive(true);

                return;
            }

            //QuitPanelがアクティブなら
            if (quitPanel.activeSelf == true)
            {
                //QuitPanelを非アクティブにする
                quitPanel.SetActive(false);

                return;
            }
        }
    }

    //QuitButtonを押したら
    public void SelectQuitButton()
    {
        //SE

        //ゲームの終了
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
        UnityEngine.Application.Quit();
#endif
    }

    //BackButton
    public void SelectBackbutton()
    {
        //SE

        //QuitPanelを閉じる
        quitPanel.SetActive(false);
    }
}
