using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    //移動距離
    //0にしたらその方向には動かない
    [SerializeField]
    private Vector3 move;
    //スピード
    [SerializeField]
    private float speed;

    private Vector3 StartPos;

    // Start is called before the first frame update
    void Start()
    {
        //始まりの位置を取得
        StartPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //移動
        //始まりの位置からmove分移動する
        transform.position = new Vector3((Mathf.Sin((Time.time) * speed) * move.x + StartPos.x), (Mathf.Sin((Time.time) * speed) * move.y + StartPos.y), (Mathf.Sin((Time.time) * speed) * move.z + StartPos.z));
    }
}
