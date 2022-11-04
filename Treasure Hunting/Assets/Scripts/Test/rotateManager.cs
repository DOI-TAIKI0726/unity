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

    //スイッチオブジェクト(ここにギミックに必要なswitchを入れる)
    [SerializeField]
    private GameObject[] switchObj;

    [Header("ドアを開く場合チェック入れる")]
    [SerializeField]
    public bool openDoor = false;

    [Header("正解の向き設定")]
    [SerializeField]
    private VectorType vecType;

    //ドアオブジェクト左
    [SerializeField]
    private GameObject doorObjR;
    //ドアオブジェクト右
    [SerializeField]
    private GameObject doorObjL;

    private enum VectorType
    {
        foward, back, left, right,
    }

    //正解の向き格納先
    private Vector3 TrueVec;

    //回転OBJ格納用変数  
    private GameObject[] RotateObj;

    // Start is called before the first frame update
    void Start()
    {
        //回転するオブジェクトの数文配列を確保
        RotateObj = new GameObject[switchObj.Length];

        //回転OBJの情報取得
        for (int nCnt = 0; nCnt < switchObj.Length; nCnt++)
        {
            //回転するObjの情報取得
            RotateObj[nCnt] = switchObj[nCnt].transform.GetChild(0).gameObject;
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
        //ドアを開かない場合終了
        if(!openDoor)
        {
            return;
        }

        int nCnt = 0;

        //正解の向きに回転するOBJが向いているとき
        while(RotateObj[nCnt].transform.forward == TrueVec)
        {
            //正解の方向に回転OBJが全て揃っていたら
            if (nCnt >= RotateObj.Length-1)
            {
                //ドアを開く関数実行
                doorObjR.GetComponent<DoorOpen>().DoorMove();
                doorObjL.GetComponent<DoorOpen>().CloseDoor();
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
