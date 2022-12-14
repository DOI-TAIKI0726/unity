//DDOL(DontDestroyOnLoadの略)で保持したい変数管理クラス
//Author:寺下琉生

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DDOL : MonoBehaviour
{
    //アイテム取得率
    //[System.NonSerialized]
    public float getTreasurePercent;

    void Start()
    {
        //シーン遷移させてもこれを維持する
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Title" || SceneManager.GetActiveScene().name == "Tutorial")
        {
            Destroy(this.gameObject);
        }
    }
}
