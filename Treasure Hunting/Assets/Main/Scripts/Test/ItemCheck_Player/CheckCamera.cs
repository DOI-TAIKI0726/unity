using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCamera : MonoBehaviour
{
    [SerializeField]
    private float rotate_speed = 3;
    //Axisの位置を指定する変数
    [SerializeField]
    private Vector3 axisPos;
    //カメラをどれくらい離すか
    [SerializeField]
    private float cameraDistance = 0;

    //プレイヤー
    private GameObject player;
    //Main Camera
    private Camera myCamera;
    //X軸の角度を制限するための変数
    private float angleUp = 60f;
    private float angleDown = -60f;

    // Start is called before the first frame update
    void Start()
    {
        //各要素を取得
        player = GameObject.Find("Player");
        myCamera = Camera.main;

        //CameraのAxisに相対的な位置をlocalPositionで指定
        myCamera.transform.localPosition = new Vector3(0, 0, -3);
        //CameraとAxisの向きを最初だけそろえる
        myCamera.transform.localRotation = transform.rotation;

        //Cameraの位置
        myCamera.transform.localPosition
            = new Vector3(myCamera.transform.localPosition.x,
            myCamera.transform.localPosition.y,
            myCamera.transform.localPosition.z + cameraDistance);
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<Checkplayer>().isMove)
        {
            //Axisの位置をPlayer＋axisPosで決める
            transform.position = player.transform.position + axisPos;

            //Cameraの位置、Z軸にスクロール分を加える
            myCamera.transform.localPosition
                = new Vector3(myCamera.transform.localPosition.x,
                myCamera.transform.localPosition.y,
                myCamera.transform.localPosition.z);

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
        }
    }
}
