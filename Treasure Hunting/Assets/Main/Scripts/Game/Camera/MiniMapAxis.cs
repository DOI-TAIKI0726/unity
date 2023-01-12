using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapAxis : MonoBehaviour
{
    //メインカメラのaxis
    GameObject axis;
    // Start is called before the first frame update
    void Start()
    {
        //Axisの情報を取得
        axis = GameObject.Find("Axis");
    }

    // Update is called once per frame
    void Update()
    {
        //ミニマップのAxisに
        //メインカメラのAxisの回転Yを代入
        transform.eulerAngles = new Vector3(90f, axis.transform.eulerAngles.y, 0f);
    }
}
