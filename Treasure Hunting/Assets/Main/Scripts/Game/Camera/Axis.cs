//カメラの移動操作クラス
//Author:寺下琉生

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axis : MonoBehaviour
{
    //Cameraの回転スピード
    [SerializeField]
    private float rotate_speed = 3;
    //Axisの位置を指定する変数
    [SerializeField]
    private Vector3 axisPos;
    //カメラをどれくらい離すか
    [SerializeField]
    private float cameraDistance;

    //プレイヤーの頭
    private GameObject playerHeadObj;
    //プレイヤー
    private GameObject player;
    //Main Camera
    private Camera myCamera;
    //GameManage
    private GameManager gameManagerScript;
    //カメラの初期位置
    private Vector3 cameraPos;
    //X軸の角度を制限するための変数
    private float angleUp = 60f;
    private float angleDown = -60f;

    private bool isHit = false;

    void Start()
    {
        //各要素を取得
        playerHeadObj = GameObject.Find("hair-back");
        player = GameObject.Find("Player");
        myCamera = Camera.main;
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();

        //CameraとAxisの向きを最初だけそろえる
        myCamera.transform.localRotation = transform.rotation;

        //Cameraの位置
        myCamera.transform.localPosition
            = new Vector3(myCamera.transform.localPosition.x,
            myCamera.transform.localPosition.y,
            myCamera.transform.localPosition.z - cameraDistance);
        //カメラの初期位置設定
        cameraPos = myCamera.transform.localPosition;
    }

    void Update()
    {
        //レイの当たり判定に使用
        RaycastHit hit;

        //QuitPanelが非アクティブでパスワードパネルのキャンバスが非アクティブなら
        if (gameManagerScript.quitPanel.activeSelf == false && GameObject.Find("Password").GetComponent<Canvas>().enabled == false)
        {
            //Axisの位置をPlayer＋axisPosで決める
            transform.position = player.transform.position + axisPos;

            //Cameraの角度にマウスからとった値を入れる
            transform.eulerAngles += new Vector3(0, Input.GetAxis("Mouse X") * rotate_speed, 0);

            //X軸の角度
            float angleX = transform.eulerAngles.x;
            //X軸の値を180度超えたら360引くことで制限しやすくする
            if (angleX >= 180)
            {
                angleX = angleX - 360;
            }
            //Mathf.Clamp(値、最小値、最大値）でX軸の値を制限する
            transform.eulerAngles = new Vector3(
                Mathf.Clamp(angleX, angleDown, angleUp),
                transform.eulerAngles.y,
                transform.eulerAngles.z
            );

            //playerHeadObjと自身の間にレイを出し、レイに何かが当たったなら
            if (Physics.Linecast(playerHeadObj.transform.position, myCamera.transform.position, out hit) == true)
            {
                myCamera.transform.position = hit.point;
            }
            if(myCamera.transform.localPosition.z >= cameraPos.z)
            {
                myCamera.transform.localPosition -= new Vector3(0.0f, 0.0f, 0.01f);
            }
        }

        Debug.DrawLine(playerHeadObj.transform.position, myCamera.transform.position, Color.red);
    }
}