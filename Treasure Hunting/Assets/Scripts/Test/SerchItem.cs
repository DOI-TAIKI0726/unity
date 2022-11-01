using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerchItem : MonoBehaviour
{
    //ターゲット
    private GameObject target;
    //プレイヤー
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //プレイヤーの情報を取得
        player = GameObject.Find("Player");
        //一番近くのアイテムを取得
        target = SerchObj(player, "Item");
    }

    // Update is called once per frame
    void Update()
    {
        //一番近くのアイテムを取得
        target = SerchObj(player, "Item");

        //プレイヤーに追従
        transform.position = player.transform.position;

        //targetがnullだった場合
        if (target == null)
        {
            //targetにゴールの情報
            target = GameObject.Find("goal");
        }

        //targetの方向を向かせる
        transform.LookAt(target.transform);
    }

    //一番近くのアイテムを取得する処理
    GameObject SerchObj(GameObject playerObj, string tagName)
    {
        //アイテムとの距離
        float tmpDis = 0;
        //一番近くのアイテムとの距離
        float serchDis = 0;
        //ターゲットのオブジェクト
        GameObject tarObj = null;
        
        foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
        {
            //アイテムとプレイヤーの距離
            tmpDis = Vector3.Distance(obs.transform.position, playerObj.transform.position);

            //一番近くのアイテムの更新
            if (serchDis == 0 || serchDis > tmpDis)
            {
                //距離の代入
                serchDis = tmpDis;
                //ターゲットのオブジェクトの更新
                tarObj = obs;
            }
        }
        
        return tarObj;
    }
}
