//OuterWallprefabを等間隔で設置する
//Author:寺下琉生

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outerWallMaker : MonoBehaviour
{
    //設置するwallprefab
    [SerializeField]
    private GameObject wallPrefad;
    //並べる個数
    [SerializeField]
    private int quantity;
    //X方向に並べる場合trueにする
    [SerializeField]
    private bool intervalX;
    //Z方向に並べる場合trueにする
    [SerializeField]
    private bool intervalZ;

    //設置間隔
    private float interval;
    //生成しきったか
    private bool isEnd = false;

    void Start()
    {
        interval = this.transform.localScale.x;
    }

    void Update()
    {
        if (isEnd == false)
        {
            //生成するouterWallPrefadを自身の子にするための変数
            GameObject obj;
            for (int nCnt = 0; nCnt < quantity; nCnt++)
            {
                if (intervalX == true)
                {
                    //wallPrefadの生成
                    obj = Instantiate(wallPrefad, new Vector3(this.transform.position.x + interval * nCnt, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0, 0, 0));
                    //生成したwallPrefadの親を設定
                    obj.transform.parent = this.transform;
                }
                if (intervalZ == true)
                {
                    //wallPrefadの生成
                    obj = Instantiate(wallPrefad, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + interval * nCnt + 1) , Quaternion.Euler(0, 90, 0));
                    //生成したwallPrefadの親を設定
                    obj.transform.parent = this.transform;
                }

                if (nCnt <= quantity)
                {
                    isEnd = true;
                }
            }
        }
    }
}
