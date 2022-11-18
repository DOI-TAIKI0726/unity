//テスト用
//Author:寺下琉生

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPlayer : MonoBehaviour
{
    //歩く速度
    [SerializeField]
    private float walkSpeed = 5.0f;
    //走る速度
    [SerializeField]
    private float runSpeed = 10.0f;
    //ジャンプ力
    [SerializeField]
    private float jumpPower = 300.0f;
    //スタミナ回復速度
    [SerializeField]
    private float recoveryStamina = 0.1f;
    //走っているときのスタミナ消費
    [SerializeField]
    private float consumptionStamina = 0.7f;

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
    //アニメーターのパラメーターisJump
    private const string param_isJump = "isJump";
    //カメラの正面方向
    private Vector3 cameraForward;
    //移動方向ベクトル
    private Vector3 moveForward;
    //リジットボディ参照
    private Rigidbody rigidBody;
    //GameManagerスクリプト
    private GameManager gameManagerScript;
    //入手したアイテム数
    private int getItemNum;
    //入手したアイテム数テキスト
    private Text getItemNumText;
    //移動方法切り替え
    private bool isMoveMode = false;
    //スタミナが減少中か
    private bool isStamina = false;
    //スタミナが減った後、最大まで回復したか
    private bool isMaxStamina = false;
    //地面についているか
    private bool isGround = true;

    void Start()
    {
        //各要素の参照や初期化
        animetor = this.GetComponent<Animator>();
        staminagage = GameObject.Find("stamina_gage").GetComponent<RectTransform>(); ;
        rigidBody = GameObject.Find("Player").GetComponent<Rigidbody>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        getItemNumText = GameObject.Find("GetItemNum").GetComponent<Text>();
        maxStamina = GameObject.Find("stamina_gage").GetComponent<RectTransform>().sizeDelta.x;
        nowStamina = maxStamina;

        //最初は非表示にしておく
        getItemNumText.enabled = false;
    }

    void FixedUpdate()
    {
        Move();
    }

    void Update()
    {
        //getItemNumTextの表示
        getItemNumText.text = "入手した宝の数:" + getItemNum.ToString();

        //tab押したら
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //テキスト表示
            getItemNumText.enabled = true;
        }
        else
        {
            //テキスト非表示
            getItemNumText.enabled = false;
        }

        //スタミナが減少状態ではないなら
        if (isStamina == false)
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
    }

    //移動関連処理
    void Move()
    {
        //QuitPanelが非アクティブでパスワードパネルのキャンバスが非アクティブなら
        if (gameManagerScript.quitPanel.activeSelf == false && GameObject.Find("Password").GetComponent<Canvas>().enabled == false)
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
                rigidBody.velocity = moveForward * walkSpeed + new Vector3(0, rigidBody.velocity.y, 0);
            }
            //走る
            if (isMoveMode == true)
            {
                //スタミナを使い切った後回復していないなら
                if (isMaxStamina == false)
                {
                    //走る移動
                    rigidBody.velocity = moveForward * runSpeed + new Vector3(0, rigidBody.velocity.y, 0);
                    if (moveForward != Vector3.zero)
                    {
                        //スタミナ減少
                        nowStamina -= consumptionStamina;
                    }
                }
            }

            //移動中なら
            if (moveForward != Vector3.zero)
            {
                //キャラクターの向きを進行方向に
                transform.rotation = Quaternion.LookRotation(moveForward);
            }

            //地面についてるなら
            if (isGround == true)
            {
                //SPACEキーを押したら
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rigidBody.AddForce(0.0f, jumpPower, 0.0f);
                    isGround = false;
                }
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
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //タグがKeyのオブジェクトに当たったら
        if (collision.gameObject.tag == "Key")
        {
            //当たった鍵の削除
            Destroy(collision.gameObject);
        }
        //タグがGroundのオブジェクトに当たったら
        if (collision.gameObject.tag == "Ground")
        {
            isGround = true;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        //タグがDoorのオブジェクトに当たったら
        if (collision.gameObject.tag == "Door")
        {
            if (Input.GetKey(KeyCode.E))
            {
                //当たったドアのスクリプト中のisOpenDoorがtrueなら
                if (collision.gameObject.GetComponent<KeyDoor>().isOpenDoor == true)
                {
                    //ドアを動かす

                    Debug.Log("ドア開く");
                }
            }
        }
    }
}