using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //移動速度
    [SerializeField]
    private float moveSpeed;

    //カメラの正面方向
    private Vector3 cameraForward;
    //移動方向ベクトル
    private Vector3 moveVector;
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
        rigidBody = GameObject.Find("Player").GetComponent<Rigidbody>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
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
            moveVector = cameraForward * vertical + Camera.main.transform.right * horizonal;

            //移動
            rigidBody.AddForce(new Vector3(moveVector.x, 0, moveVector.z) * moveSpeed, ForceMode.Force);
        }
        //QuitPanelがアクティブなら
        else if(gameManagerScript.quitPanel.activeSelf == true)
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
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            getItemNumText.enabled = true;
        }
        else
        {
            getItemNumText.enabled = false;
        }
    }
}