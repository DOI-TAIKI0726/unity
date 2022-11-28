//カメラの描画関連クラス
//Author:寺下琉生

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myCamera : MonoBehaviour
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
            if (Physics.Linecast(playerHeadObj.transform.position, this.transform.position, out hit) == true)
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
                //hitColliderに何か入っているなら
                if (hitCollider != null)
                {
                    //レイが当たらなくなったオブジェクトのMeshRendererをtrueに戻す
                    hitCollider.gameObject.GetComponent<MeshRenderer>().enabled = true;
                }
            }

            //時間リセット
            rayTime = 0.0f;
        }
        Debug.DrawLine(playerHeadObj.transform.position, transform.position, Color.red);
    }

    ////　キャラクターのTransform
    //[SerializeField]
    //private Transform charaLookAtPosition;
    ////　カメラの移動スピード
    //[SerializeField]
    //private float cameraMoveSpeed = 2f;
    ////　カメラの回転スピード
    //[SerializeField]
    //private float cameraRotateSpeed = 90f;
    ////　カメラのキャラクターからの相対値を指定
    //[SerializeField]
    //private Vector3 basePos = new Vector3(0f, 0f, 2f);
    //// 障害物とするレイヤー
    //[SerializeField]
    //private LayerMask obstacleLayer;

    //void Update()
    //{
    //    //　通常のカメラ位置を計算
    //    var cameraPos = charaLookAtPosition.position + (-charaLookAtPosition.forward * basePos.z) + (Vector3.up * basePos.y);
    //    //　カメラの位置をキャラクターの後ろ側に移動させる
    //    transform.position = Vector3.Lerp(transform.position, cameraPos, cameraMoveSpeed * Time.deltaTime);

    //    RaycastHit hit;
    //    //　キャラクターとカメラの間に障害物があったら障害物の位置にカメラを移動させる
    //    if (Physics.Linecast(charaLookAtPosition.position, transform.position, out hit, obstacleLayer))
    //    {
    //        transform.position = hit.point;
    //    }
    //    //　レイを視覚的に確認
    //    Debug.DrawLine(charaLookAtPosition.position, transform.position, Color.red, 0f, false);

    //    //　スピードを考慮しない場合はLookAtで出来る
    //    //transform.LookAt(charaTra.position);
    //    //　スピードを考慮する場合
    //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(charaLookAtPosition.position - transform.position), cameraRotateSpeed * Time.deltaTime);
    //}
}