using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//カメラの方向を向く
public class billboard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 p = Camera.main.transform.position;
       
        p.y = transform.position.y;

        this.transform.LookAt(p);
    }
}
