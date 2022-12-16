/*-----------------------------------
//キー入力無効
//跳ね返りの処理を使用する場合Playerにアタッチ
/Author:山田祐人
-----------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableKeyInput : MonoBehaviour
{
    //キー入力を無効にする秒数
    [SerializeField]
    private float notSecond;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Obstacle" || col.gameObject.tag == "Obstacle")
        {
            //コルーチンを開始
            StartCoroutine("DistableInputCoroutine");
        }
    }
    
    private IEnumerator DistableInputCoroutine()
    {
        //キー入力のコンポーネントを無効にする
        this.gameObject.GetComponent<Player>().enabled = false;

        //指定した秒数を待つ
        yield return new WaitForSeconds(notSecond);

        //キー入力のコンポーネントを有効にする
        this.gameObject.GetComponent<Player>().enabled = true;
    }
}
