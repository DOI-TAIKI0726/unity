using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnFloor : MonoBehaviour
{
    //リスポーン地点
    [HideInInspector]
    public Vector3 ResPoint;
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
        if(col.gameObject.tag=="Player")
        {
            //リスポーンに復活
            col.transform.position = ResPoint;
        }
    }
}
