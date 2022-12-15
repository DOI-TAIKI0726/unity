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
    //跳ね返すときの速さ
    [SerializeField]
    private float boundSpeed;
    //跳ね返す単位ベクトルにかける倍数
    [SerializeField]
    private float bounceVector;

    //始まりの位置
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

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Player")
        {
            //跳ね返りの処理
            //衝突した面の、接触した点における法線ベクトルを取得
            Vector3 normal = col.contacts[0].normal;
            //衝突した速度ベクトルを単位ベクトルにする
            Vector3 velocity = col.rigidbody.velocity.normalized;
            //x,z方向に対して逆向きの法線ベクトルを取得
            velocity += new Vector3(-normal.x * bounceVector, 0f, -normal.z * bounceVector);
            //取得した法線ベクトルに跳ね返す速さをかけて、跳ね返す
            col.rigidbody.AddForce(velocity * boundSpeed, ForceMode.Impulse);
        }
    }
}
