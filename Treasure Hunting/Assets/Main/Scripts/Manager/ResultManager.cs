//リザルトシーン管理クラス
//Author:寺下琉生

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultManager : Manager
{
    //DDOLスクリプト
    private DDOL dDOLScript;
    //アイテム収集率表示テキスト
    private Text treasuerPercentText;
    //resultシーンに遷移した後にすぐ遷移させないで待機時間を設ける
    private float stayTime = 0.5f;
    //fadeスクリプト
    private Fade FadeScript;
    //なにか入力されたか
    private bool isInput = false;

    void Start()
    {
        dDOLScript = GameObject.Find("DDOL").GetComponent<DDOL>();
        treasuerPercentText = GameObject.Find("TreasuerPercent").GetComponent<Text>();
        FadeScript = GameObject.Find("FadePanel").GetComponent<Fade>();
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Result")
        {
            stayTime -= Time.deltaTime;
            //gameシーンから持ってきた変数の内容をテキストに反映する
            treasuerPercentText.text = dDOLScript.getTreasurePercent.ToString("f1");

            //待機時間終わったら
            if (stayTime <= 0.0f)
            {
                if (isInput == false)
                {
                    if (Input.anyKeyDown == true)
                    {
                        isInput = true;
                        FadeScript.fadeOut = true;
                    }
                }
                else
                {
                    if(FadeScript.fadeOut == false)
                    {

                        SceneManager.LoadScene("Title");
                    }
                }
            }
        }
    }
}
