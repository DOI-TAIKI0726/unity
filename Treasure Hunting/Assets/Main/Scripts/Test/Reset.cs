using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    //押すオブジェクト
    private GameObject PushObj;
    //初期位置
    private Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        //押すオブジェクトの情報を取得
        PushObj = GameObject.Find("PushCube");
        //押すオブジェクトの初期位置を取得
        pos = PushObj.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag=="Player")
        {
            //初期位置に戻す
            PushObj.transform.position = pos;
        }
    }
}
