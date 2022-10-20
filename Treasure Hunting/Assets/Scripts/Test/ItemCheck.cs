using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCheck : MonoBehaviour
{
    //宝の数
    private int Item;
    //収集したアイテムの数
    [System.NonSerialized]
    public int GatherCount;
    //収集するアイテムの指定数
    [System.NonSerialized]
    public int TotalGather = 3;
    //宝の数チェックのテキスト
    private Text CheckText = null;
    //収集したアイテムのアイコン
    private Image GatherIcon = null;
    //収集したアイテム数チェックのテキスト
    private Text GatherText;

    // Start is called before the first frame update
    void Start()
    {
        //宝の数チェック用テキストの情報を取得
        CheckText = GameObject.Find("CheckText").GetComponent<Text>();

        //収集するアイテムのアイコンの情報の取得
        GatherIcon = GameObject.Find("GatherIcon").GetComponent<Image>();
        //収集するアイテム数のテキストの情報を取得
        GatherText = GameObject.Find("GatherText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //テキスト表示の切り替え
        //ゲームマネージャーで処理してもよい
        //ここから
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            //テキスト表示
            //宝の数チェックのテキスト
            CheckText.enabled = true;

            //収集するアイテムを指定数集められた場合
            //収集するアイテムのテキストとアイコンを表示
            if (GatherCount > 0 && GatherCount < TotalGather)
            {
                GatherIcon.enabled = true;
                GatherText.enabled = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            //テキスト非表示
            //宝の数チェックのテキスト
            CheckText.enabled = false;

            //収集するアイテムのテキストとアイコンを非表示
            GatherIcon.enabled = false;
            GatherText.enabled = false;
        }
        //ここまで

        //テキストの表示
        //宝の数チェックのテキスト
        CheckText.text = "宝物の数:" + Item.ToString();
        //収集するアイテムのテキスト
        GatherText.text = "×" + GatherCount.ToString();
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

    //収集するアイテムの加算処理
    public void GatherAdd()
    {
        GatherCount++;
    }
}
