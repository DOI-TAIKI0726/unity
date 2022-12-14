using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    //DDOLスクリプト
    private DDOL dDOLScript;
    //アイテム収集率表示テキスト
    private Text treasuerPercentText;

    void Start()
    {
        dDOLScript = GameObject.Find("DDOL").GetComponent<DDOL>();
        treasuerPercentText = GameObject.Find("TreasuerPercent").GetComponent<Text>();
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Result")
        {
            treasuerPercentText.text = dDOLScript.getTreasurePercent.ToString("f1");

            if(Input.anyKeyDown)
            {
                SceneManager.LoadScene("Title");
            }
        }
    }
}
