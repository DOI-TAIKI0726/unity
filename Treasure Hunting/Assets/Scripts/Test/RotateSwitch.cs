//回転するスイッチクラス
//Author : 藤田育昂

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSwitch : MonoBehaviour
{
    //回転するモデル名格納先
    private GameObject rotateObj;

    //回転するオブジェクトの名前
    [SerializeField]
    private string rotateObjName = "rotateCube";

    void Start()
    {
        //回転させるモデル名取得
        rotateObj = GameObject.Find(rotateObjName);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //プレイヤーと当たり続けている間の判定
    //void OnTriggerStay(Collider collider)
    //{
    //    //プレイヤータグついたやつと当たっているとき
    //    if (collider.gameObject.tag == "Player")
    //    {
    //        //Spaceボタンが入力
    //        if (Input.GetKeyDown(KeyCode.Space))
    //        {
    //            //指定されてるObjctを90°回転する
    //            rotateObj.transform.Rotate(0, 90, 0);
    //        }
    //    }
    //}

    void OnCollisionStay(Collision col)
    {
        //プレイヤータグついたやつと当たっているとき
        if (col.gameObject.tag == "Player")
        {
            //Spaceボタンが入力
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //指定されてるObjctを90°回転する
                rotateObj.transform.Rotate(0, 90, 0);
            }
        }
    }
}