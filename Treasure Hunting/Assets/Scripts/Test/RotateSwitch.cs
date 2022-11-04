//回転するスイッチクラス
//Author : 藤田育昂

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSwitch : MonoBehaviour
{
    //プレイヤーオブジェクト情報格納先
    private GameObject player_obj;
    //スイッチオブジェクトの子(回転するオブジェクト)情報格納
    private GameObject rotate_obj;

    private rotateManager rotManager;//クリア判定取得用
    private bool bUsedButton;//左クリックが押されたかの判定


    void Start()
    {
        
        //クリア判定取得のために親オブジェクト(rotateManager)情報取得
        GameObject objParent = transform.parent.gameObject;
        rotManager = objParent.GetComponent<rotateManager>();

        //プレイヤーの情報をPlayertagから取得
        player_obj = GameObject.FindGameObjectWithTag("Player");
        
        //回転するobjの情報はスイッチobjの子から取得 
        rotate_obj = transform.GetChild(0).gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //徐々に回転させる処理
    IEnumerator Rotate()
    {
        //回転速度
        float speed = 1f;
        //回転カウント
        int Cnt = 0;

        //90回カウントする
        while (Cnt < 90 / speed)
        {
            Cnt++;
            rotate_obj.transform.Rotate(0, speed, 0);
            yield return null;
        }
        
        //向きが揃っているかチェック
        rotManager.CheckRotate();

        bUsedButton = false;
    }

    ////プレイヤーと当たり続けている間の判定
    //void OnCollisionStay(Collision col)
    //{
    //    //正解の向きに揃っていない場合
    //    if (rotManager.bEnd == false)
    //    {
    //        //スイッチオブジェクトがプレイヤータグついたやつと当たった
    //        if (col.gameObject.tag == "Player")
    //        {
    //            //左クリックボタンが入力
    //            if (Input.GetMouseButton(0) && bUsedButton == false)
    //            {
    //                bUsedButton = true;
    //                //オブジェクトを回転させるコルーチンスタート
    //                StartCoroutine(Rotate());
    //            }
    //        }
    //    }
    //}

    //プレイヤーと当たり続けている間の判定
    void OnTriggerStay(Collider col)
    {
        //正解の向きに揃っていない場合
        if (rotManager.bEnd == false)
        {
            //スイッチオブジェクトがプレイヤータグついたやつと当たった
            if (col.gameObject.tag == "Player")
            {
                //左クリックボタンが入力
                if (Input.GetMouseButton(0) && bUsedButton == false)
                {
                    bUsedButton = true;
                    //オブジェクトを回転させるコルーチンスタート
                    StartCoroutine(Rotate());
                }
            }
        }
    }
}