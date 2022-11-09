using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    private GameObject PushObj;
    private Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        PushObj = GameObject.Find("PushCube");
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
            PushObj.transform.position = pos;
        }
    }
}
