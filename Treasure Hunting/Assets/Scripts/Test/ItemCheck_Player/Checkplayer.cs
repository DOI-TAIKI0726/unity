using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkplayer : MonoBehaviour
{
    //移動
    private float x;                                //x方向の移動
    private float z;                                //z方向の移動
    public float WalkSpeed = 3.0f;                  //移動の速さ
    public float gravity = 9.8f;                    //重力
    private Rigidbody rb;                           //リジッドボディ
    
    //プレイヤースクリプトにコピーしたい変数
    //ここから
    //アイテム
    [SerializeField]
    private GameObject[] Item = null;
    //oldtypeのデータの数
    [SerializeField]
    private int DataCnt = 0;
    private int Itemtype;                           //取得したアイテムの種類
    private int[] oldtype;                          //上記の前に取得したアイテムの種類
    //確認用
    private int ItemCount = 0;              //アイテム数
    //キーが入手されているか
    [System.NonSerialized]
    public bool keyuse = false;
    //ここまで

    // Start is called before the first frame update
    void Start()
    {
        //リジッドボディの情報の格納
        rb = GetComponent<Rigidbody>();

        //プレイヤースクリプトにコピー
        //oldtypeのデータ数の更新
        oldtype = new int[DataCnt];
    }

    // Update is called once per frame
    void Update()
    {
        //移動の処理
        x = Input.GetAxis("Horizontal") * WalkSpeed;
        z = Input.GetAxis("Vertical") * WalkSpeed;
        rb.velocity = new Vector3(x, rb.velocity.y, z);
        

        //プレイヤースクリプトにコピーしたい処理
        //チェック用
        //スペース押したら宝物を吐き出す
        //吐き出さない場合はいらない処理
        //ここから
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //アイテム数が0より大きい場合
            if (ItemCount > 0)
            {
                //減算
                ItemCount = GameObject.Find("GameManager").GetComponent<ItemCheck>().Add(-1);

                //アイテムの生成
                //生成する位置を取得
                Vector3 position = new Vector3(transform.position.x + 2f, transform.position.y, transform.position.z);
                //生成
                GameObject NewItem = Instantiate(Item[Itemtype], position, transform.rotation);
                
                //次生成するアイテムの更新
                for (int cnt = 0; cnt < oldtype.Length; cnt++)
                {
                    //次に生成するアイテムの種類を取得
                    if (cnt == 0)
                    {
                        Itemtype = oldtype[cnt];
                    }
                    //生成するアイテムの順番の更新
                    else
                    {
                        oldtype[cnt - 1] = oldtype[cnt];
                    }
                }
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

            //アイテムを吐き出す場合必要な処理
            //ここから
            //取得したアイテムの種類を取得
            for (int cnt = 0; cnt < Item.Length; cnt++) 
            {
                if (col.gameObject.name == Item[cnt].name || col.gameObject.name == Item[cnt].name + "(Clone)")
                {
                    if(ItemCount>0)
                    {
                        for (int i = oldtype.Length - 1; i >= 0; i--) 
                        {
                            //oldtypeの更新
                            if (i == 0)
                            {
                                //oldtype[0]に現在のItemtypeを代入
                                oldtype[i] = Itemtype;
                                Debug.Log(oldtype[i]);
                            }
                            else
                            {
                                //1つ前のデータを現在のデータに代入
                                oldtype[i] = oldtype[i - 1];
                            }
                        }
                    }
                    
                    //Itemtypeの更新
                    Itemtype = cnt;
                    break;
                }
            }
        }

        //当たったオブジェクトのタグがRouletteの場合
        if(col.gameObject.tag=="Roulette")
        {
            //アイテムの削除
            Destroy(col.gameObject);

            //ルーレット開始の処理
            GameObject.Find("GameManager").GetComponent<RandomItem>().RouletteStart();
        }

        //タグがGatherだった場合
        if (col.gameObject.tag=="Gather")
        {
            //アイテムの削除
            Destroy(col.gameObject);

            //収集したアイテム数の加算
            GameObject.Find("GameManager").GetComponent<ItemCheck>().GatherAdd();
        }

        //タグがKeyだった場合
        if (col.gameObject.tag == "Key")
        {
            //アイテムの削除
            Destroy(col.gameObject);

            //キーを入手
            keyuse = true;
        }
        
    }

    //オブジェクトに触れ続けている場合
    void OnCollisionStay(Collision col)
    {
        //タグがKeyDoorだった場合
        if (col.gameObject.name == "KeyDoor")
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                //キーが入手していた場合
                if (keyuse)
                {
                    //ドアを動かす処理
                    col.gameObject.GetComponent<DoorOpen>().DoorMove();

                    //キーを使えない状態にする
                    keyuse = false;
                }
            }
            //チェック用
            Debug.Log("stay");
        }
    }
    //ここまで
}
