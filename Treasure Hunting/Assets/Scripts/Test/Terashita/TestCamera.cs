using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCamera : MonoBehaviour
{
    //プレイヤーの頭
    private GameObject playerHeadObj;
    //レイが当たったオブジェクトのコライダー
    private Collider hitCollider;
    //メッシュを消したオブジェクトのコライダー
    private Collider deleteMeshCol;

    void Start()
    {
        playerHeadObj = GameObject.Find("PlayerHead");
    }

    void Update()
    {
        //レイの当たり判定に使用
        RaycastHit hit;

        bool isHit = false;

        if (Physics.Linecast(playerHeadObj.transform.position, transform.position, out hit) == true)
        {
            hitCollider = hit.collider;
            isHit = true;
        }

        if (hitCollider != null)
        {

            if (isHit == true)
            {
                hitCollider.gameObject.GetComponent<MeshRenderer>().enabled = false;
                deleteMeshCol = hit.collider;
            }
            else
            {
                hitCollider.gameObject.GetComponent<MeshRenderer>().enabled = true;
            }
        }
        Debug.DrawLine(playerHeadObj.transform.position, transform.position, Color.red);
    }
}