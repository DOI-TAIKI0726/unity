using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherItem : MonoBehaviour
{
    //キーアイテム
    //[SerializeField]
    //GameObject KeyItem;

    private int TotalGather;

    // Start is called before the first frame update
    void Start()
    {
        TotalGather = GameObject.Find("GameManager").GetComponent<ItemCheck>().TotalGather;
    }

    // Update is called once per frame
    void Update()
    {
        //収集するアイテムが指定数集められた場合
        if (GameObject.Find("GameManager").GetComponent<ItemCheck>().GatherCount >= TotalGather)
        {
            //キーを生成
            //Instantiate(KeyItem, KeyItem.transform.position,transform.rotation);

            GameObject.Find("Player").GetComponent<Checkplayer>().keyuse = true;

            //処理が一度がしか通らないよう
            //収集したアイテムの数を0に戻す
            GameObject.Find("GameManager").GetComponent<ItemCheck>().GatherCount = 0;
        }
    }
}
