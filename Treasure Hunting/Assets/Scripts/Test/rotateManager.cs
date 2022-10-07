using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateManager : MonoBehaviour
{
    GameObject obj1;
    GameObject obj2;
    GameObject obj3;


    //回転させるオブジェクト名
    [SerializeField]
    private string objName1 = "rotateCube1";
    [SerializeField]
    private string objName2 = "rotateCube2";
    [SerializeField]
    private string objName3 = "rotateCube3";


    // Start is called before the first frame update
    void Start()
    {
        obj1 = GameObject.Find(objName1);
        obj2 = GameObject.Find(objName2);
        obj3 = GameObject.Find(objName3);
    }

    // Update is called once per frame
    void Update()
    {
        //オブジェクトの向きがそろった
        if(obj1.transform.forward == obj2.transform.forward &&
           obj2.transform.forward == obj3.transform.forward &&
           obj3.transform.forward == obj1.transform.forward)
        {
            Debug.Log("クリア");
        }
    }
}
