//プレイヤーの操作関連クラス
//Author:寺下琉生

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //歩く速度
    private float walkSpeed = 5.0f;
    //走る速度
    private float runSpeed = 8.0f;
    //スタミナ回復速度
    private float recoveryStamina = 0.2f;
    //走っているときのスタミナ消費量
    private float consumptionStamina = 0.7f;
    //最大スタミナ
    private float maxStamina;
    //スタミナゲージのRectTransform
    private RectTransform staminagage;
    //現在のスタミナ
    private float nowStamina;
    //自身に設定されているアニメーター
    private Animator animetor;
    //アニメーターのパラメーターisIdel
    private const string param_isIdel = "isIdel";
    //アニメーターのパラメーターisRun
    private const string param_isRun = "isRun";
    //アニメーターのパラメーターisJump
    private const string param_isJump = "isJump";
    //カメラの正面方向
    private Vector3 cameraForward;
    //移動方向ベクトル
    private Vector3 moveForward;
    //リジットボディ
    private Rigidbody rigidBody;
    //GameManagerスクリプト
    private GameManager gameManagerScript;
    //Tutorialmanagerスクリプト
    private TutorialManager tutorialManagerScript;
    //スピードの倍率
    private float speedUp = 1f;
    //バフの時間
    private float buffTime;
    //バフ効果中かどうか
    private bool isBuff;
    //スタミナ無限か
    private bool isStaminaLimit = false;
    //移動方法切り替え
    private bool isMoveMode = false;
    //スタミナが減少中か
    private bool isStamina = false;
    //スタミナが減った後、最大まで回復したか
    private bool isMaxStamina = false;
    //Runアニメーション中かどうか
    private bool isRunAnimetion = false;
    //Updateを通っていいか
    private bool isUpdate = false;
    //ドアを開けられるかどうか
    private bool isOpenDoor = false;
    //メインで使っているライト
    private Light MainLight;
    //PlayerHeadのライト
    private Light PlayerHeadLight;
    //移動可能か
    private bool isMove = false;

    //キーが入手されているか
    [System.NonSerialized]
    public bool isKeyuse = false;

    void Start()
    {
        //各要素の参照や初期化
        animetor = this.GetComponent<Animator>();
        staminagage = GameObject.Find("stamina_gage").GetComponent<RectTransform>();
        rigidBody = GameObject.Find("Player").GetComponent<Rigidbody>();
        maxStamina = GameObject.Find("stamina_gage").GetComponent<RectTransform>().sizeDelta.x;
        nowStamina = maxStamina;

        //ゲームシーンのみで通す
        if (SceneManager.GetActiveScene().name == "Game")
        {
            gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
            //ライトの情報を取得
            MainLight = GameObject.Find("Directional Light").GetComponent<Light>();
        }
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            tutorialManagerScript = GameObject.Find("TutorialManager").GetComponent<TutorialManager>();
        }
        PlayerHeadLight = GameObject.Find("hair-back/Point Light").GetComponent<Light>();

    }

    void FixedUpdate()
    {
        Move();
    }

    void Update()
    {
        //各シーンのManagerから参照
        if(SceneManager.GetActiveScene().name == "Game")
        {
            if (gameManagerScript.quitPanel.activeSelf == false && gameManagerScript.isEndCountDown == true)
            {
                isUpdate = true;
            }
        }
        else if(SceneManager.GetActiveScene().name == "Tutorial")
        {
            if(tutorialManagerScript.quitPanel.activeSelf == false)
            {
                isUpdate = true;
            }
        }
        if (isUpdate == true)
        {
            //スタミナが減少状態ではないならorスタミナが無限なら
            if (isStamina == false || isStaminaLimit == true)
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

            //バフ効果時間なら
            if (isBuff == true)
            {
                //バフの経過時間
                buffTime -= Time.deltaTime;

                //BuffTime秒経ったらバフ終了
                if (buffTime <= 0)
                {
                    //バフの時間を0にする
                    buffTime = 0f;

                    //tagがbuffのオブジェクトを削除
                    foreach (GameObject obs in GameObject.FindGameObjectsWithTag("buff"))
                    {
                        //削除
                        Destroy(obs);
                    }

                    if (SceneManager.GetActiveScene().name == "Game")
                    {

                        //ライトを表示する
                        MainLight.gameObject.SetActive(true);
                    }
                    PlayerHeadLight.gameObject.SetActive(true);


                    //スタミナ無限状態じゃなくする
                    isStaminaLimit = false;

                    //スピードの倍率を設定
                    speedUp = 1f;

                    //バフ終了状態にする
                    isBuff = false;

                    Debug.Log("end");
                }
            }
        }
    }

    //移動関連処理
    void Move()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            if (gameManagerScript.quitPanel.activeSelf == false
                && GameObject.Find("Password").GetComponent<Canvas>().enabled == false
                && gameManagerScript.isEndCountDown == true)
            {
                isMove = true;
            }
            else
            {
                isMove = false;
            }
          
        }
        else if(SceneManager.GetActiveScene().name == "Tutorial")
        {
            if (tutorialManagerScript.quitPanel.activeSelf == false
                && GameObject.Find("Password").GetComponent<Canvas>().enabled == false)
            {
                isMove = true;
            }
            else
            {
                isMove = false;
            }
        }
        if(isMove == true)
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
                        //スタミナ回復
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
                rigidBody.velocity = moveForward * speedUp * walkSpeed + new Vector3(0, rigidBody.velocity.y, 0);
            }
            //走る
            if (isMoveMode == true)
            {
                //スタミナを使い切った後回復していないなら
                if (isMaxStamina == false)
                {
                    //走る移動
                    rigidBody.velocity = moveForward * speedUp * runSpeed + new Vector3(0, rigidBody.velocity.y, 0);
                    //走る移動中なら
                    if (moveForward != Vector3.zero)
                    {
                        //スタミナバフ中じゃない
                        if(!isStaminaLimit)
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
                transform.rotation = Quaternion.LookRotation(moveForward * speedUp);
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
        //QuitPanelがアクティブなら
        else
        {
            //動きを止める
            rigidBody.velocity = Vector3.zero;
            //移動アニメーション終了
            this.animetor.SetBool(param_isRun, false);
            //待機アニメーション開始
            this.animetor.SetBool(param_isIdel, true);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //タグがTreasureのオブジェクトに当たったら
        if(collision.gameObject.tag == "Treasure")
        {
            //当たったオブジェクトの削除
            Destroy(collision.gameObject);
            //入手した宝の数に加算する
            GameObject.Find("GameManager").GetComponent<ItemCheck>().AddTreasureNum(1);
        }

        //タグがGatherだった場合
        if (collision.gameObject.tag == "Gather")
        {
            //アイテムの削除
            Destroy(collision.gameObject);

            if(SceneManager.GetActiveScene().name == "Game")
            {
                //収集したアイテム数の加算
                GameObject.Find("GameManager").GetComponent<ItemCheck>().GatherAdd();
            }
            if(SceneManager.GetActiveScene().name == "Tutorial")
            {
                //収集したアイテム数の加算
                GameObject.Find("TutorialManager").GetComponent<ItemCheck>().GatherAdd();
            }
        }
    }

    void OnCollisionStay(Collision collision)
    {
        //タグがDoorのオブジェクトに当たったら
        if (collision.gameObject.tag == "Door")
        {
            if (Input.GetKey(KeyCode.E))
            {
                if(SceneManager.GetActiveScene().name == "Game")
                {
                    if (GameObject.Find("GameManager").GetComponent<ItemCheck>().GatherCount == 3 && isKeyuse == false)
                    {
                        isOpenDoor = true;
                    }
                }
                if(SceneManager.GetActiveScene().name == "Tutorial")
                {
                    isOpenDoor = true;
                }
                //Keyが3つ揃っていたら
                if(isOpenDoor == true)
                {
                    //ドアを開く
                    collision.transform.gameObject.GetComponent<DoorOpen>().DoorMove();
                    isKeyuse = true;
                }
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        //タグがItemのオブジェクトに当たったら
        if (collider.gameObject.tag == "Item")
        {
            //ルーレット開始の処理
            GameObject.Find("GameManager").GetComponent<RandomItem>().RouletteStart();
        }
    }

    //スピード系バフの処理
    public void BuffSpeed(float Speed, float Bufftime)
    {
        //スピードの倍率を設定
        speedUp = Speed;
        //バフの時間を設定
        buffTime = Bufftime;

        //バフ中の状態にする
        isBuff = true;
    }

    //サーチのバフ処理
    public void BuffSerch(float Bufftime)
    {
        //バフの時間を設定
        buffTime = Bufftime;

        //バフ中の状態にする
        isBuff = true;
    }

    //スタミナ無限バフの処理
    public void BuffStamina(bool isLimitless, float Bufftime)
    {
        //バフの時間を設定
        buffTime = Bufftime;

        //スタミナ無限状態にする
        isStaminaLimit = isLimitless;

        //バフ中の状態にする
        isBuff = true;
    }

    //視界系のバフの処理
    public void BuffLight(float Bufftime)
    {
        //バフの時間を設定
        buffTime = Bufftime;

        if (SceneManager.GetActiveScene().name == "Game")
        {
            //ライトの非表示
            MainLight.gameObject.SetActive(false);
        }
        PlayerHeadLight.gameObject.SetActive(false);

        //バフ中の状態にする
        isBuff = true;
    }
}