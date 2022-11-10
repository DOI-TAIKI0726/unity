using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCheck : MonoBehaviour
{
    //入手した宝の数
    private int GetTreasureNum;
    //宝の数チェックのテキスト
    private Text GetTreasureNumText = null;
    //収集したアイテムのアイコン
    private Image GatherIcon = null;
    //収集したアイテム数チェックのテキスト
    private Text GatherText;

    //収集したアイテムの数
    [System.NonSerialized]
    public int GatherCount;
    //収集するアイテムの指定数
    [System.NonSerialized]
    public int TotalGather = 3;

    void Start()
    {
        //宝の数チェック用テキストの情報を取得
        GetTreasureNumText = GameObject.Find("GetTreasureNum").GetComponent<Text>();
        //収集するアイテムのアイコンの情報の取得
        GatherIcon = GameObject.Find("GatherIcon").GetComponent<Image>();
        //収集するアイテム数のテキストの情報を取得
        GatherText = GameObject.Find("GatherText").GetComponent<Text>();

        //Gatherのオブジェクト数を取得
        TotalCount("Gather");
    }

    void Update()
    {
        //テキスト表示の切り替え
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            //テキスト表示
            //宝の数チェックのテキスト
            GetTreasureNumText.enabled = true;
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            //テキスト非表示
            //宝の数チェックのテキスト
            GetTreasureNumText.enabled = false;
        }

        //収集するアイテムを指定数集められた場合
        //収集するアイテムのテキストとアイコンを表示
        if (GatherCount > 0 && GatherCount < TotalGather)
        {
            GatherIcon.enabled = true;
            GatherText.enabled = true;
        }
        else
        {
            //収集するアイテムのテキストとアイコンを非表示
            GatherIcon.enabled = false;
            GatherText.enabled = false;
        }

        //テキストの表示
        //入手した宝の数チェックのテキスト
        GetTreasureNumText.text = "入手した宝物の数:" + GetTreasureNum.ToString();
        //収集するアイテムのテキスト
        GatherText.text = ":" + GatherCount.ToString() + "/" + TotalGather;
    }

    //入手した宝の数を加算
    //宝物を吐き出さない場合どちらもvoidでよい
    public int AddTreasureNum(int Data)
    {
        //加算
        GetTreasureNum += Data;       //宝物を吐き出す場合
        return GetTreasureNum;

        //Item++;           //宝物を吐き出さない場合
    }

    //収集するアイテムの加算処理
    public void GatherAdd()
    {
        GatherCount++;
    }

    //tagNameのオブジェクト数の合計を取得する処理
    void TotalCount(string tagName)
    {
        GameObject[] tagObj = GameObject.FindGameObjectsWithTag(tagName);

        TotalGather = tagObj.Length;
    }
}
