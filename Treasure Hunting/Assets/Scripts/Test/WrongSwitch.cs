using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrongSwitch : MonoBehaviour
{
    private float g, b;         //マテリアルの値
    private Renderer render;    //スイッチのレンダラー

    // Start is called before the first frame update
    void Start()
    {
        //レンダラーの情報の取得
        render = GetComponent<Renderer>();

        //マテリアルの値の設定
        g = render.material.color.g;
        b = render.material.color.b;
    }

    // Update is called once per frame
    void Update()
    {
        //レンダラーの更新
        render.material.color = new Color(render.material.color.r, g, b, render.material.color.a);
    }

    //当たり判定
    //オブジェクトに触れている間の処理
    void OnTriggerStay(Collider col)
    {
        //触れているオブジェクトのタグ
        if(col.gameObject.tag=="Player"||col.gameObject.tag=="Switch")
        {
            //マテリアルの値に0を代入
            g = 0;
            b = 0;
        }
    }

    //オブジェクトから離れた時の処理
    void OnTriggerExit(Collider col)
    {
        //触れているオブジェクトのタグ
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Switch")
        {
            //マテリアルの値に1を代入
            g = 1;
            b = 1;
        }
    }
}
