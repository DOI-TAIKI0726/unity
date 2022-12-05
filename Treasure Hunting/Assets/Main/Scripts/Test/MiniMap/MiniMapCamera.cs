using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    //プレイヤー
    private GameObject player;
    //ミニマップカメラのY座標
    [SerializeField]
    private float MiniPosY;

    // Start is called before the first frame update
    void Start()
    {
        //プレイヤーの情報取得
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //プレイたーの位置取得
        Vector3 pos = player.transform.position;
        //ミニカメラのY座標を取得
        pos.y = MiniPosY;

        //ミニカメラの座標にposを代入
        transform.position = pos;
    }
}
