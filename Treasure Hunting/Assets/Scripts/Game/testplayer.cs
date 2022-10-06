using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testplayer : MonoBehaviour
{
    float speed = 0.1f;

   

    

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKey("a"))
        {
            transform.position += new Vector3(-speed,0,0); 
        }
        else if (Input.GetKey("d"))
        {
            transform.position += new Vector3(speed, 0, 0);
        }
        else if (Input.GetKey("s"))
        {
            transform.position += new Vector3(0, 0, -speed);
        }
        else if (Input.GetKey("w"))
        {
            transform.position += new Vector3(0, 0, speed);
        }
    }

    
}
