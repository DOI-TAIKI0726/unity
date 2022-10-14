//回転するオブジェクトの向き管理クラス
//
//Auther：藤田育昂
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateManager : MonoBehaviour
{
    //終了判定
    [System.NonSerialized]
    public bool bEnd = false;

    //正解の向き格納先
    private Vector3 TrueVec;
     private enum VectorType
    {
        foward,back,left,right,
    }

    [Header("正解の向き設定")]
    [SerializeField]
    private VectorType vecType;

    //回転OBJ格納用変数  
    private GameObject[] childRotateObj;

    // Start is called before the first frame update
    void Start()
    {
        //スイッチOBJの数分の配列を確保
        GameObject[] childSwitchObj = new GameObject[transform.childCount];

        //回転するオブジェクトの数文配列を確保
        childRotateObj = new GameObject[transform.childCount];

        //スイッチOBJと回転OBJの子情報取得
        for (int nCnt = 0; nCnt < transform.childCount; nCnt++)
        {
            //RotateManagerの子オブジェクト(スイッチOBJ)の情報取得
            childSwitchObj[nCnt] = transform.GetChild(nCnt).gameObject;

            //スイッチオブジェクトの子オブジェクト(回転OBJ)の情報取得
            childRotateObj[nCnt] = childSwitchObj[nCnt].transform.GetChild(0).gameObject;
        }

        //正解の向き設定
        if(vecType ==VectorType.foward)
        {
            TrueVec = new Vector3(0,0,1);
        }
        else if(vecType == VectorType.back)
        {
            TrueVec = new Vector3(0, 0, -1);
        }
        else if(vecType == VectorType.left)
        {
            TrueVec = new Vector3(-1, 0, 0);
        }
        else if(vecType == VectorType.right)
        {
            TrueVec = new Vector3(1, 0, 0);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //クリアしてないとき
        if (bEnd == false)
        {
            CheckRotate();
        }
        else
        {
            Debug.Log("クリア");
        }
        
    }

    //回転OBJの向きが揃っているかチェック
    void CheckRotate()
    {
        int nCnt = 0;

        //正解の向きに回転するOBJが向いているとき
        while(childRotateObj[nCnt].transform.forward == TrueVec)
        {
            nCnt++;
            //正解の方向に回転OBJが全て揃っていたら
            if(nCnt >= transform.childCount-1)
            {
                bEnd = true;
                break;
            }
        }
    }
}
