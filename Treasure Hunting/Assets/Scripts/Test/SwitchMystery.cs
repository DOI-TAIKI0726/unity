using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMystery : MonoBehaviour
{
    private GameObject door;     //ドアのオブジェクト
    private float g, b;          //マテリアルの値
    private Renderer render;     //レンダラー

    // Start is called before the first frame update
    void Start()
    {
        //レンダラーの情報の取得
        render = GetComponent<Renderer>();
        //ドアのオブジェクトの情報取得
        door = GameObject.Find("door");

        //g,bの値をレンダラーから取得
        g = render.material.color.g;
        b = render.material.color.b;
    }

    // Update is called once per frame
    void Update()
    {
        //レンダラーの更新
        render.material.color = new Color(render.material.color.r, g, b, render.material.color.a);
    }

    //接地判定
    void OnTriggerStay(Collider col)
    {
        //当たったオブジェクトのタグ
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Switch") 
        {
            //アクティブの切り替え
            door.SetActive(false);
            //g,bに0を代入
            g = 0;
            b = 0;
        }
    }

    void OnTriggerExit(Collider col)
    {
        //当たったオブジェクトのタグ
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Switch")
        {
            //アクティブの切り替え
            door.SetActive(true);
            //g,bに1を代入
            g = 1;
            b = 1;
        }
    }
}
