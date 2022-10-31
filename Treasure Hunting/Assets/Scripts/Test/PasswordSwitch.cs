using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//スイッチになるオブジェクトにアタッチする
public class PasswordSwitch : MonoBehaviour
{
    //パスワードパネルのオブジェクト
    private GameObject pwPanel;

    // Start is called before the first frame update
    void Start()
    {
        //パスワードパネルの情報を取得
        pwPanel = GameObject.Find("PasswordPanel");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        //当たったオブジェクト名がPlayerだった場合
        if(col.gameObject.name=="Player")
        {
            //パスワードがクリアされていなかった場合
            if(!pwPanel.GetComponent<PasswordPanel>().clear)
            {
                //パスワードのCanvasを表示する
                GameObject.Find("Password").GetComponent<Canvas>().enabled = true;
                ////プレイヤー移動不可能状態にする
                //col.gameObject.GetComponent<Checkplayer>().isMove = false;
            }
        }
    }
}
