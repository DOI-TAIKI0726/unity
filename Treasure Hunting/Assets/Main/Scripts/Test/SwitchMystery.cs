using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//正解のスイッチにアタッチする
public class SwitchMystery : MonoBehaviour
{
    private GameObject door;    //ドアのオブジェクト
    private float g, b;         //マテリアルの値
    private Renderer render;    //レンダラー
    private bool open = false;  //ドアが開いているか

    // Start is called before the first frame update
    void Start()
    {
        //レンダラーの情報の取得
        render = GetComponent<Renderer>();
        //ドアのオブジェクトの情報取得
        door = GameObject.Find("CheckObj/door");

        //g,bの値をレンダラーから取得
        g = render.material.color.g;
        b = render.material.color.b;
    }

    // Update is called once per frame
    void Update()
    {
        //レンダラーの更新
       // render.material.color = new Color(render.material.color.r, g, b, render.material.color.a);
    }

    //接地判定
    void OnTriggerStay(Collider col)
    {
        //当たったオブジェクトのタグ
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Switch") 
        {
            //g,bに0を代入
            g = 0;
            b = 0;

            //開いていない場合
            if (!open)
            {
                //ドアを開ける
                door.GetComponent<DoorOpen>().DoorMove();
            }

            //ドアを開いている状態
            open = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        //当たったオブジェクトのタグ
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Switch")
        {
            //g,bに1を代入
            g = 1;
            b = 1;

            //ドアが開いている場合
            if (open)
            {
                //ドアを閉める
                door.GetComponent<DoorOpen>().CloseDoor();
            }

            //ドアが閉まった状態
            open = false;
        }
    }
}
