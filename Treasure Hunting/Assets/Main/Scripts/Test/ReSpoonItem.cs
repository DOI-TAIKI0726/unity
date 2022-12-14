using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//アイテムボックスのリスポーン
//Auther　藤田育昂

public class ReSpoonItem : MonoBehaviour
{
    //prefabのItemBox格納
    [SerializeField]
    private GameObject ItemBoxPrefab;

    [SerializeField]
    private float reSpoonTime;//リスポーンするまでの時間を測る

    private GameObject ItemBoxObj;//アイテムボックスObj
    private float      reSpoonCnt;//リスポーン時間の保存

    void Start()
    {
        //ItemBoxの情報取得
        ItemBoxObj = transform.GetChild(0).gameObject;
        
        //リスポーン時間を代入
        reSpoonCnt = reSpoonTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // ItemBoxObjが存在していない場合
        if (ItemBoxObj == null)
        {
            reSpoonTime -= Time.deltaTime;
            //リスポーン時間が0になったとき
            if (reSpoonTime <= 0)
            {
                //リスポーン時間を代入
                reSpoonTime = reSpoonCnt;
                // ItemBoxPrefabから新しくGameObjectを作成
                GameObject newItemBoxObj = Instantiate(ItemBoxPrefab, this.transform);

                // 新しく作成したGameObjectの名前を再設定(今回は"PlayerSphere"となる)
                ItemBoxObj = newItemBoxObj;
            }
        }
    }
}
