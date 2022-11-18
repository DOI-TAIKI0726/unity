//カメラの描画テスト用
//Author:寺下琉生

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCamera : MonoBehaviour
{
    //レイを出す間隔
    [SerializeField]
    private float rayCT = 0.01f;

    //プレイヤーの頭
    private GameObject playerHeadObj;
    //レイが当たったオブジェクトのコライダー
    private Collider hitCollider;
    //MeshRendererをfalseにしたオブジェクトのコライダー
    private Collider falseMeshCol;
    //時間カウント用
    float rayTime = 0.0f;

    void Start()
    {
        //各要素を取得
        playerHeadObj = GameObject.Find("PlayerHead");
    }

    void Update()
    {
        //レイの当たり判定に使用
        RaycastHit hit;

        rayTime += Time.deltaTime;

        //rayCT毎に通る
        if (rayTime >= rayCT)
        {
            //playerHeadObjと自身の間にレイを出し、レイに何かが当たったなら
            if(Physics.Linecast(playerHeadObj.transform.position, this.transform.position, out hit) == true)
            {
                //当たったオブジェクトのMeshRendererをfalseにする
                hit.collider.gameObject.GetComponent<MeshRenderer>().enabled = false;
                //レイが当たらなくなったときにMeshRendererをtrueにする用に、レイが当たったオブジェクトを保存しておく
                hitCollider = hit.collider;

                //falseMeshColに何か入っているなら
                if (falseMeshCol != null)
                {
                    //falseMeshColとレイが当たったオブジェクトが違うものなら
                    if (falseMeshCol != hit.collider)
                    {
                        //falseMeshColのMeshRendererをtrueに戻す
                        falseMeshCol.gameObject.GetComponent<MeshRenderer>().enabled = true;
                    }
                }

                //MeshRendererをfalseにしたオブジェクトを保存しておく
                falseMeshCol = hit.collider;
            }
            else
            {
                //レイが当たらなくなったオブジェクトのMeshRendererをtrueに戻す
                hitCollider.gameObject.GetComponent<MeshRenderer>().enabled = true;
            }

            //時間リセット
            rayTime = 0.0f;
        }

        Debug.DrawLine(playerHeadObj.transform.position, transform.position, Color.red);
    }
}