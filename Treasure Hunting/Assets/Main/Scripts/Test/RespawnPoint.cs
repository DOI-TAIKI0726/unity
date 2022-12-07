using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    //リスポーンさせる床
    private GameObject ResFloor;

    // Start is called before the first frame update
    void Start()
    {
        ResFloor = GameObject.Find("RespawnFloor");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag=="Player")
        {
            //リスポーン地点の更新
            ResFloor.GetComponent<RespawnFloor>().ResPoint = transform.position;
        }
    }
}
