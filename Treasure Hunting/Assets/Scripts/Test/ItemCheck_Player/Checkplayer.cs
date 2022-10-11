using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkplayer : MonoBehaviour
{
    //移動
    float x;                                //x方向の移動
    float z;                                //y方向の移動
    public float WalkSpeed = 3.0f;          //移動の速さ
    public float gravity = 9.8f;            //重力
    Rigidbody rb;                           //リジッドボディ

    //確認用
    private int ItemCount = 0;              //アイテム数
    //アイテム
    [SerializeField]
    private GameObject Item = null;

    // Start is called before the first frame update
    void Start()
    {
        //リジッドボディの情報の格納
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //移動の処理
        x = Input.GetAxis("Horizontal") * WalkSpeed;
        z = Input.GetAxis("Vertical") * WalkSpeed;
        rb.velocity = new Vector3(x, rb.velocity.y, z);
        
        //チェック用
        //スペース押したら宝物を吐き出す
        //吐き出さない場合はいらない処理
        //ここから
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //アイテム数が0より大きい場合
            if(ItemCount>0)
            {
                //減算
                ItemCount = GameObject.Find("GameManager").GetComponent<ItemCheck>().Add(-1);

                //アイテムの生成
                Vector3 position = new Vector3(transform.position.x + 2f, transform.position.y, transform.position.z);
                GameObject NewItem = Instantiate(Item, position, transform.rotation);
            }
        }
        //ここまで
    }

    //プレイヤースクリプトにコピーしたい処理
    //当たり判定
    void OnCollisionEnter(Collision col)
    {
        //当たったオブジェクトのタグがItemだった場合
        if(col.gameObject.tag=="Item")
        {
            //アイテムの削除
            Destroy(col.gameObject);

            //加算
            ItemCount = GameObject.Find("GameManager").GetComponent<ItemCheck>().Add(1);
        }
    }
}
