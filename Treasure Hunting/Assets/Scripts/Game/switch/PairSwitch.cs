//スイッチを変数に入れて、入れたスイッチの色を揃えたら何かしらできるようにする
//Author:寺下琉生

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PairSwitch : MonoBehaviour
{
    //スイッチオブジェクト(ここにギミックに必要なswitchを入れる)
    [SerializeField]
    private GameObject[] switchObj;

    //スイッチのマテリアルが全て同じになっているかどうか
    private bool isMaterial = false;

    //マテリアルの色がcorrectColorと同じswitchObj
    [System.NonSerialized]
    public int correctSwitchNum = 0;
    //クリアフラグ一回だけ立てる用
    [System.NonSerialized]
    public bool isEnd = false;

    //スイッチを起動したときに変えるマテリアルの色
    public Color[] switchColor;
    //スイッチの揃える正解の色(必ずswitchColorにある色のいずれかと同じにする)
    public Color correctColor;

    void Start()
    {

    }

    void Update()
    {
        //念のため0以下にならないようにする
        if (correctSwitchNum <= 0)
        {
            correctSwitchNum = 0;
        }

        //インスペクターで入れたスイッチの数とcorrectSwitchNumが同じなら
        if (switchObj.Length == correctSwitchNum)
        {
            isMaterial = true;
        }

        if (isMaterial == true && isEnd == false)
        {
            Debug.Log("クリア");
            isEnd = true;
        }
    }
}