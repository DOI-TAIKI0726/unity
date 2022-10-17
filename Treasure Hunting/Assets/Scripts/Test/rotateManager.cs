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

    [Header("正解の向き設定")]
    [SerializeField]
    private VectorType vecType;

    [Header("開くドアの名前入力")]
    [SerializeField]
    private string doorName = "doorManager";

    private enum VectorType
    {
        foward, back, left, right,
    }

    //正解の向き格納先
    private Vector3 TrueVec;

    //回転OBJ格納用変数  
    private GameObject[] childRotateObj;

    private GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        //スイッチobjの数分の配列を確保
        GameObject[] childSwitchObj = new GameObject[transform.childCount];

        //回転するオブジェクトの数文配列を確保
        childRotateObj = new GameObject[transform.childCount];

        //ドアManagerの情報取得
        door = GameObject.Find(doorName);

        //スイッチOBJと回転OBJの子情報取得
        for (int nCnt = 0; nCnt < transform.childCount; nCnt++)
        {
            //RotateManagerの子オブジェクト(スイッチobj)の情報取得
            childSwitchObj[nCnt] = transform.GetChild(nCnt).gameObject;

            //スイッチオブジェクトの子オブジェクト(回転obj)の情報取得
            childRotateObj[nCnt] = childSwitchObj[nCnt].transform.GetChild(0).gameObject;
        }

        //正解の向きを設定
        SetVector(vecType);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //回転OBJの向きが揃っているかチェック
    public void CheckRotate()
    {
        int nCnt = 0;

        //正解の向きに回転するOBJが向いているとき
        while(childRotateObj[nCnt].transform.forward == TrueVec)
        {
            
            //正解の方向に回転OBJが全て揃っていたら
            if (nCnt >= transform.childCount-1)
            {
                //ドアを開く関数実行
                door.GetComponent<DoorOpen>().DoorMove();
                bEnd = true;
                break;
            }
            else
            {
                nCnt++;
            }
        }
    }

    //正解の向き設定
    void SetVector(VectorType vec)
    {
        if (vec == VectorType.foward)
        {
            //正面
            TrueVec = new Vector3(0, 0, 1);
        }
        else if (vec == VectorType.back)
        {
            //後ろ
            TrueVec = new Vector3(0, 0, -1);
        }
        else if (vec == VectorType.left)
        {
            //左
            TrueVec = new Vector3(-1, 0, 0);
        }
        else if (vec == VectorType.right)
        {
            //右
            TrueVec = new Vector3(1, 0, 0);
        }
    }
}
