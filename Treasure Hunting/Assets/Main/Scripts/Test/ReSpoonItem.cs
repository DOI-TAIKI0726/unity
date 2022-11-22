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

    GameObject ItemBoxObj;//アイテムボックスObj

    [SerializeField]
    private float reSpoonTime = 30;//リスポーンするまでの時間

    void Start()
    {
        //子オブジェクトがないとき
        //設定したItemBoxPrefabを生成
        if (transform.childCount <= 0)
        {
            // ItemBoxPrefabから新しくGameObjectを作成
            GameObject newItemBoxObj = Instantiate(ItemBoxPrefab, this.transform);
            // 新しく作成したGameObjectの名前を再設定
            ItemBoxObj = newItemBoxObj;
        }
        else
        {//子オブジェクトがいるとき
            ItemBoxObj = transform.GetChild(0).gameObject;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // ItemBoxObjが存在していない場合
        if (ItemBoxObj == null)
        {
            reSpoonTime -= Time.deltaTime;
            //リスポーン時間が0になったとき
            if (reSpoonTime <= 0)
            {
                reSpoonTime = 30;
                // ItemBoxPrefabから新しくGameObjectを作成
                GameObject newItemBoxObj = Instantiate(ItemBoxPrefab, this.transform);

                // 新しく作成したGameObjectの名前を再設定(今回は"PlayerSphere"となる)
                ItemBoxObj = newItemBoxObj;
            }
        }
    }
}
