using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectPlayer : MonoBehaviour
{
    //移動速度
    [SerializeField]
    private float moveSpeed;

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

    void Start()
    {
        //各要素参照
        animetor = this.GetComponent<Animator>();

        rigidBody = GameObject.Find("EffectPlayer").GetComponent<Rigidbody>();
        gameManagerScript = GameObject.Find("EffectManager").GetComponent<GameManager>();
        getItemNumText = GameObject.Find("GetItemNum").GetComponent<Text>();

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

            ////移動
            rigidBody.velocity = moveForward * moveSpeed + new Vector3(0, rigidBody.velocity.y, 0);

            // キャラクターの向きを進行方向に
            if (moveForward != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(moveForward);
            }

            //移動したらアニメーション遷移
            if (horizonal != 0 || vertical != 0)
            {
                this.animetor.SetBool(param_isRun, true);
            }
            else
            {
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
        ////getItemNumTextの表示
        //getItemNumText.text = "入手した宝の数:" + getItemNum.ToString();

        //tab押したら
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            getItemNumText.enabled = true;
        }
        else
        {
            getItemNumText.enabled = false;
        }
    }
}