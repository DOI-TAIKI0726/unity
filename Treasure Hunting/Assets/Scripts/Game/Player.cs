using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        //各要素参照
        rigidBody = GameObject.Find("Player").GetComponent<Rigidbody>();
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
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
}