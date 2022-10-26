using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkplayer : MonoBehaviour
{
    //移動
    //歩く速度
    [SerializeField]
    private float walkSpeed;
    //走る速度
    [SerializeField]
    private float runSpeed;
    //移動方向ベクトル
    private Vector3 moveForward;
    //カメラの正面方向
    private Vector3 cameraForward;

    private Rigidbody rb;                           //リジッドボディ

    //プレイヤースクリプトにコピーしたい変数
    //ここから
    //スピードの倍率
    private float Speedup = 1f;
    //バフの時間
    private float BuffCnt;
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
    //移動可能か
    [System.NonSerialized]
    public bool isMove = true;
    //バフ中か
    private bool buff = false;
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

    void FixedUpdate()
    {
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        //移動の処理
        //Move();

        //プレイヤースクリプトにコピーしたい処理
        //ここから
        //バフ中
        if (buff)
        {
            //バフの経過時間
            BuffCnt -= Time.deltaTime;

            //BuffCnt秒経ったらバフ終了
            if (BuffCnt <= 0)
            {
                BuffCnt = 0f;

                //スピードの倍率を設定
                Speedup = 1f;

                //バフ終了状態にする
                buff = false;

                Debug.Log("end");
            }
        }
        //チェック用
        //スペース押したら宝物を吐き出す
        //吐き出さない場合はいらない処理
        if (Input.GetKeyDown(KeyCode.Space))
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

    void Move()
    {
        //移動キーの入力を取得
        //縦
        float horizonal = Input.GetAxis("Horizontal");
        //横
        float vertical = Input.GetAxis("Vertical");

        //カメラの方向からXZ平面の単位ベクトル取得
        cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        
        //移動可能
        if(isMove)
        {
            //移動方向ベクトルを設定
            moveForward = cameraForward * vertical + Camera.main.transform.right * horizonal;
        }
        //移動不可能
        else
        {
            moveForward = Vector3.zero;
        }

        rb.velocity = moveForward * walkSpeed * Speedup + new Vector3(0, rb.velocity.y, 0);
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

    //スピード系バフの処理
    public void BuffSpeed(float speed,float Bufftime)
    {
        //スピードの倍率を設定
        Speedup = speed;
        //バフの時間を設定
        BuffCnt = Bufftime;

        //バフ中の状態にする
        buff = true;
    }
    //ここまで
}
