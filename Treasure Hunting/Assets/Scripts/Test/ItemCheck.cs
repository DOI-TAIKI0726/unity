using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCheck : MonoBehaviour
{
    //アイテム数
    private int Item;
    //アイテム数チェックのテキスト
    private Text CheckText = null;

    // Start is called before the first frame update
    void Start()
    {
        CheckText = GameObject.Find("CheckText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //テキスト表示の切り替え
        //ゲームマネージャーで処理してもよい
        //ここから
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            CheckText.enabled = true;
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            CheckText.enabled = false;
        }
        //ここまで

        //テキストの表示
        CheckText.text = "宝物の数:" + Item.ToString();
    }

    //アイテム確認
    //宝物を吐き出さない場合どちらもvoidでよい
    public int Add(int Data)
    {
        //加算
        Item += Data;       //宝物を吐き出す場合
        return Item;
        //Item++;           //宝物を吐き出さない場合
    }
}
