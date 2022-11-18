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
    //ジャンプ力
    [SerializeField]
    private float jumpPower = 300.0f;
    //スタミナ回復速度
    [SerializeField]
    private float recoveryStamina;
    //走っているときのスタミナ消費
    [SerializeField]
    private float consumptionStamina;

    //移動方向ベクトル
    private Vector3 moveForward;
    //カメラの正面方向
    private Vector3 cameraForward;

    //最大スタミナ
    private float maxStamina;
    //スタミナゲージのRectTransform
    private RectTransform staminagage;
    //現在のスタミナ
    private float nowStamina;
    //自身に設定されているアニメーター
    private Animator animetor;
    //アニメーターのパラメーターisRun
    private const string param_isRun = "isRun";
    //アニメーターのパラメーターisIdel
    private const string param_isIdel = "isIdel";
    //アニメーターのパラメーターisJump
    private const string param_isJump = "isJump";
    //移動方法切り替え
    private bool isMoveMode = false;
    //スタミナが減少中か
    private bool isStamina = false;
    //スタミナが減った後、最大まで回復したか
    private bool isMaxStamina = false;
    //GameManagerスクリプト
    private GameManager gameManagerScript;
    //地面についているか
    private bool isGround = true;

    private Rigidbody rb;                           //リジッドボディ

    //プレイヤースクリプトにコピーしたい変数
    //ここから
    //スタミナ無限か
    private bool isLimit = false;
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
    //メインで使っているライト
    private Light MainLight;
    //PlayerHeadのライト
    private Light PlayerHeadLight;
    //ここまで

    // Start is called before the first frame update
    void Start()
    {
        animetor = this.GetComponent<Animator>();

        //リジッドボディの情報の格納
        rb = GetComponent<Rigidbody>();
        staminagage = GameObject.Find("stamina_gage").GetComponent<RectTransform>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();

        //ライトの情報を取得
        MainLight = GameObject.Find("Directional Light").GetComponent<Light>();
        PlayerHeadLight = GameObject.Find("PlayerHead/Point Light").GetComponent<Light>();

        //プレイヤースクリプトにコピー
        //oldtypeのデータ数の更新
        oldtype = new int[DataCnt];

        maxStamina = GameObject.Find("stamina_gage").GetComponent<RectTransform>().sizeDelta.x;
        nowStamina = maxStamina;
    }

    void FixedUpdate()
    {
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManagerScript.quitPanel.activeSelf == false)
        {
            //スタミナが減少状態ではないなら
            if (isStamina == false || isLimit == true) 
            {
                //スタミナを回復
                nowStamina += recoveryStamina;
                //スタミナの上限を超えないようにする
                if (nowStamina >= maxStamina)
                {
                    nowStamina = maxStamina;
                }
            }

            //スタミナを使い切ったら
            if (nowStamina <= 0)
            {
                isMaxStamina = true;
            }
            //スタミナが回復しきったら
            if (nowStamina >= maxStamina)
            {
                isMaxStamina = false;
            }

            //スタミナゲージの更新
            staminagage.sizeDelta = new Vector2(nowStamina, staminagage.sizeDelta.y);

            //プレイヤースクリプトにコピーしたい処理
            //ここから
            //チェック用
            //スペース押したら宝物を吐き出す
            //吐き出さない場合はいらない処理
            if(GameObject.Find("Password").GetComponent<Canvas>().enabled == false)
            {
                //バフ中
                if (buff)
                {
                    //バフの経過時間
                    BuffCnt -= Time.deltaTime;

                    //BuffCnt秒経ったらバフ終了
                    if (BuffCnt <= 0)
                    {
                        //バフの時間を0にする
                        BuffCnt = 0f;

                        //tagがbuffのオブジェクトを削除
                        foreach (GameObject obs in GameObject.FindGameObjectsWithTag("buff"))
                        {
                            //削除
                            Destroy(obs);
                        }

                        //ライトを表示する
                        MainLight.gameObject.SetActive(true);
                        PlayerHeadLight.gameObject.SetActive(true);

                        //スタミナ無限状態じゃなくする
                        isLimit = false;

                        //スピードの倍率を設定
                        Speedup = 1f;

                        //バフ終了状態にする
                        buff = false;

                        Debug.Log("end");
                    }
                }
                if (Input.GetMouseButtonDown(0))
                {
                    //アイテム数が0より大きい場合
                    if (ItemCount > 0)
                    {
                        //減算
                        ItemCount = GameObject.Find("GameManager").GetComponent<ItemCheck>().AddTreasureNum(-1);

                        //アイテムの生成
                        //生成する位置を取得
                        Vector3 position = new Vector3(transform.position.x + 2f, transform.position.y+1f, transform.position.z);
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
            }
            
            //ここまで
        }
    }

    void Move()
    {
        //移動可能
        if(gameManagerScript.quitPanel.activeSelf == false && GameObject.Find("Password").GetComponent<Canvas>().enabled == false)
        {
            //移動キーの入力を取得
            //縦
            float horizonal = Input.GetAxis("Horizontal");
            //横
            float vertical = Input.GetAxis("Vertical");

            //カメラの方向からXZ平面の単位ベクトル取得
            cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            //移動方向ベクトルを設定
            moveForward = cameraForward * vertical + Camera.main.transform.right * horizonal;

            //移動状態切り替え
            //歩き状態に切り替え
            if (Input.GetKey(KeyCode.LeftShift) == false)
            {
                //スタミナ減少状態を解除
                isStamina = false;
                //歩く状態にする
                isMoveMode = false;
            }
            //走り状態に切り替え
            if (Input.GetKey(KeyCode.LeftShift) == true)
            {
                if (isMaxStamina == false)
                {
                    if (moveForward != Vector3.zero)
                    {
                        //スタミナを減少状態にする
                        isStamina = true;
                    }
                    else
                    {
                        //スタミナ減少状態を解除
                        isStamina = false;
                    }
                    //走る状態にする
                    isMoveMode = true;
                }
            }

            //スタミナが切れたら
            if (nowStamina <= 0)
            {
                //スタミナ減少状態を解除
                isStamina = false;
                //歩く状態にする
                isMoveMode = false;
            }

            //移動切り替え
            //歩く
            if (isMoveMode == false)
            {
                //歩く移動
                rb.velocity = moveForward * walkSpeed * Speedup + new Vector3(0, rb.velocity.y, 0);
            }
            //走る
            if (isMoveMode == true)
            {
                //スタミナを使い切った後回復していないなら
                if (isMaxStamina == false)
                {
                    //走る移動
                    rb.velocity = moveForward * runSpeed * Speedup + new Vector3(0, rb.velocity.y, 0);
                    if (moveForward != Vector3.zero)
                    {
                        //スタミナ無限状態じゃなかった場合
                        if(!isLimit)
                        {
                            //スタミナ減少
                            nowStamina -= consumptionStamina;
                        }
                    }
                }
            }

            //移動中なら
            if (moveForward != Vector3.zero)
            {
                //キャラクターの向きを進行方向に
                transform.rotation = Quaternion.LookRotation(moveForward*Speedup);
            }

            //地面についてるなら
            if (isGround == true)
            {
                //SPACEキーを押したら
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rb.AddForce(0.0f, jumpPower, 0.0f);
                    isGround = false;
                }
            }
            //動いていないならアニメーション遷移
            if (horizonal == 0 && vertical == 0)
            {
                //待機アニメーション開始
                this.animetor.SetBool(param_isIdel, true);
            }
            else
            {
                //待機アニメーション開始
                this.animetor.SetBool(param_isIdel, false);
            }
            //移動したらアニメーション遷移
            if (horizonal != 0 || vertical != 0)
            {
                //移動アニメーション開始
                this.animetor.SetBool(param_isRun, true);
            }
            else
            {
                //移動アニメーション終了
                this.animetor.SetBool(param_isRun, false);
            }
        }
        //移動不可能
        else
        {
            rb.velocity = Vector3.zero;
            //移動アニメーション終了
            this.animetor.SetBool(param_isRun, false);
            //待機アニメーション開始
            this.animetor.SetBool(param_isIdel, true);
        }

        //rb.velocity = moveForward * walkSpeed * Speedup + new Vector3(0, rb.velocity.y, 0);
    }

    //プレイヤースクリプトにコピーしたい処理
    //当たり判定
    void OnCollisionEnter(Collision col)
    {
        //タグがGroundのオブジェクトに当たったら
        if (col.gameObject.tag == "Ground")
        {
            isGround = true;
        }
        //当たったオブジェクトのタグがItemだった場合
        if (col.gameObject.tag=="Treasure")
        {
            //アイテムの削除
            Destroy(col.gameObject);

            //加算
            ItemCount = GameObject.Find("GameManager").GetComponent<ItemCheck>().AddTreasureNum(1);

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

        //当たったオブジェクトのタグがItemの場合
        if(col.gameObject.tag=="Item")
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

            Debug.Log("Gather");
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
                    //col.gameObject.GetComponent<DoorOpen>().DoorMove();
                    GameObject.Find("KeyDoorManager").GetComponent<DoorOpen>().DoorMove();

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

    //サーチのバフ処理
    public void BuffSerch(float Bufftime)
    {
        //バフの時間を設定
        BuffCnt = Bufftime;

        //バフ中の状態にする
        buff = true;
    }

    //スタミナ無限バフの処理
    public void BuffStamina(bool isLimitless, float Bufftime)
    {
        //バフの時間を設定
        BuffCnt = Bufftime;

        //スタミナ無限状態にする
        isLimit = isLimitless;

        //バフ中の状態にする
        buff = true;
    }

    //視界系のバフの処理
    public void BuffLight(float Bufftime)
    {
        //バフの時間を設定
        BuffCnt = Bufftime;

        //ライトの非表示
        MainLight.gameObject.SetActive(false);
        PlayerHeadLight.gameObject.SetActive(false);

        //バフ中の状態にする
        buff = true;
    }
    //ここまで
}
