using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestColorSwitch : MonoBehaviour
{
    //PairSwitctスクリプト
    private TestPairSwitch pairSwitchScript;
    //スイッチのマテリアルの色を変える用の変数
    private int colorNum;
    //スイッチを起動したか
    private bool isStratUp = false;
    //正解の色と同じ色になったら
    private bool isCorrectColor = false;

    void Start()
    {
        //各要素にアクセス
        pairSwitchScript = GameObject.Find("PairSwitch").GetComponent<TestPairSwitch>();
        SetMaterial();
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        //クリアしてないなら
        if (pairSwitchScript.isEnd == false)
        {
            //当たったオブジェクトのタグがPlayerなら
            if (collider.gameObject.tag == "Player")
            {
                //スイッチ起動
                isStratUp = true;

                //colorNumがswitchColorの最大数以外なら
                if (colorNum != pairSwitchScript.switchColor.Length - 1)
                {
                    //スイッチを踏むたびにスイッチのマテリアルの色をswitColorの順番通りに変更させる
                    this.gameObject.GetComponent<Renderer>().material.color = pairSwitchScript.switchColor[colorNum];
                    //次の色に変えるための変数加算
                    colorNum++;

                    StartUpChangeSwitchColor();
                }
                //colorNumがswitchColorの最大数-1なら
                else if (colorNum == pairSwitchScript.switchColor.Length - 1)
                {
                    //スイッチを踏んだので色を変更
                    this.gameObject.GetComponent<Renderer>().material.color = pairSwitchScript.switchColor[colorNum];
                    //colorNumが一周したので0にする
                    colorNum = 0;

                    StartUpChangeSwitchColor();
                }

                isStratUp = false;
            }
        }
    }

    //スイッチ起動時にスイッチの色を変える処理
    void StartUpChangeSwitchColor()
    {
        //スイッチのマテリアルの色が正解の色なら
        if (this.gameObject.GetComponent<Renderer>().material.color == pairSwitchScript.correctColor)
        {
            //正解の色のスイッチの数を加算
            pairSwitchScript.correctSwitchNum = pairSwitchScript.correctSwitchNum + 1;
            //スイッチの色が正解
            isCorrectColor = true;
        }
        //スイッチの色が正解の状態なら
        if (isCorrectColor == true)
        {
            //現在のスイッチの色が正解の色ではないなら
            if (this.gameObject.GetComponent<Renderer>().material.color != pairSwitchScript.correctColor)
            {
                //正解の色のスイッチの数を加算
                pairSwitchScript.correctSwitchNum = pairSwitchScript.correctSwitchNum - 1;
                //スイッチの色が不正解
                isCorrectColor = false;
            }
        }
    }

    //現在のマテリアルの色からColorNumを設定
    void SetMaterial()
    {
        Color red    = new Color(1f, 0, 0, 1f);
        Color blue   = new Color(0, 0, 1f, 0);
        Color green  = new Color(0, 1f, 0, 0);
        Color yellow = new Color(1f, 1f, 0, 0);
        
        if(this.gameObject.GetComponent<Renderer>().material.color == red)
        {//赤
            colorNum = 1;
        }
        else if(this.gameObject.GetComponent<Renderer>().material.color == blue)
        {//青
            colorNum = 2;
        }
        else if(this.gameObject.GetComponent<Renderer>().material.color == green)
        {//緑
            colorNum = 3;
        }
        else if(this.gameObject.GetComponent<Renderer>().material.color == yellow)
        {//黄色
            colorNum = 0;
        }
    }
}
