//踏むと色が変わるスイッチ(スイッチ1つ1つに入れる)
//Author:寺下琉生

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwitch : MonoBehaviour
{
    //PairSwitctスクリプト
    private PairSwitch pairSwitchScript;
    //スイッチのマテリアルの色を変える用の変数
    private int colorNum = 0;
    //スイッチを起動したか
    private bool isStratUp = false;
    //正解の色と同じ色になったら
    private bool isCorrectColor = false;

    void Start()
    {
        //各要素にアクセス
        pairSwitchScript = GameObject.Find("PairSwitch").GetComponent<PairSwitch>();
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
}
