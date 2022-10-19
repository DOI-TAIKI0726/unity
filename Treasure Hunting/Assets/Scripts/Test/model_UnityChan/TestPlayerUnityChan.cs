//テスト用
//Author:寺下琉生

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestPlayerUnityChan : MonoBehaviour
{
    //歩く速度
    [SerializeField]
    private float walkSpeed;
    //走る速度
    [SerializeField]
    private float runSpeed;
    //走っているときのスタミナ消費
    [SerializeField]
    private float consumptionStamina;

    //スタミナの最大値
    private float MaxStamina;
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
    private bool isMove = false;
    //スタミナが減少中か
    private bool isStamina = false;

    void Start()
    {
        //各要素参照
        animetor = this.GetComponent<Animator>();
        staminagage = GameObject.Find("stamina_gage").GetComponent<RectTransform>(); ;
        rigidBody = GameObject.Find("Player").GetComponent<Rigidbody>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        getItemNumText = GameObject.Find("GetItemNum").GetComponent<Text>();
        MaxStamina = 200;
        nowStamina = MaxStamina;

        //最初は非表示にしておく
        getItemNumText.enabled = false;
    }

    void FixedUpdate()
    {
        //QuitPanelが非アクティブなら
        if (gameManagerScript.quitPanel.activeSelf == false)
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

            //移動方法切り替え
            if (Input.GetKey(KeyCode.LeftShift) == true)
            {
                //スタミナを減少状態にする
                isStamina = true;
                //走る状態にする
                isMove = true;

                //スタミナが切れたら
                if(nowStamina <= 0)
                {
                    //スタミナ減少状態を解除
                    isStamina = false;
                    //歩く状態にする
                    isMove = false;
                }
            }
            if (Input.GetKey(KeyCode.LeftShift) == false)
            {
                //スタミナ減少状態を解除
                isStamina = false;
                //歩く状態にする
                isMove = false;
            }

            //移動切り替え
            if (isMove == false)
            {
                //歩く移動
                rigidBody.velocity = moveForward * walkSpeed + new Vector3(0, rigidBody.velocity.y, 0);
            }

            if (isMove == true)
            {
                //走る移動
                rigidBody.velocity = moveForward * runSpeed + new Vector3(0, rigidBody.velocity.y, 0);
                //スタミナ減少
                Stamina(consumptionStamina);
            }

            //移動中ではないなら
            if (moveForward != Vector3.zero)
            {
                //キャラクターの向きを進行方向に
                transform.rotation = Quaternion.LookRotation(moveForward);
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
        else if (gameManagerScript.quitPanel.activeSelf == true)
        {
            //動きを止める
            rigidBody.velocity = Vector3.zero;
        }
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
            //スタミナを加算していく
            nowStamina++;
            //スタミナの上限を超えないようにする
            if (nowStamina >= MaxStamina)
            {
                nowStamina = MaxStamina;
            }
        }

        //スタミナゲージの更新
        staminagage.sizeDelta = new Vector2(nowStamina, staminagage.sizeDelta.y);
    }



    void Stamina(float stamina)
    {
        //走っているときのスタミナ消費
        if (isMove == true)
        {
            nowStamina -= stamina;
        }

        //ジャンプのスタミナ消費

    }
}