using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSwitch : MonoBehaviour
{
    //回転するモデル
    GameObject obj1;
    GameObject obj2;
    GameObject obj3;

    //スイッチ名
    [SerializeField]
    private string switchName1 = "switch1";
    [SerializeField]
    private string switchName2 = "switch2";
    [SerializeField]
    private string switchName3 = "switch3";

    //回転するオブジェクトの名前
    [SerializeField]
    private string objName1 = "rotateCube1";
    [SerializeField]
    private string objName2 = "rotateCube2";
    [SerializeField]
    private string objName3 = "rotateCube3";

    void Start()
    {
        //モデル名取得
        obj1 = GameObject.Find(objName1);
        obj2 = GameObject.Find(objName2);
        obj3 = GameObject.Find(objName3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    ////当たり続けている間
    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //回転させるスイッチと接触してるとき
                if (gameObject.name == switchName1)
                {
                    obj1.transform.Rotate(0, 90, 0);
                }
                if (gameObject.name == switchName2)
                {
                    obj2.transform.Rotate(0, 90, 0);
                }
                else if (gameObject.name == switchName3)
                {
                    obj3.transform.Rotate(0, 90, 0);
                }
            }
        }
    }


    //void OnCollisionStay(Collision collision)
    //{
    //    if(collision.gameObject.tag == "Player")
    //    {
    //        if (Input.GetKeyDown(KeyCode.Space))
    //        {
    //            //回転させるスイッチと接触してるとき
    //            if (gameObject.name == switchName1)
    //            {
    //                obj1.transform.Rotate(0, 90, 0);
    //            }
    //            else if (gameObject.name == switchName2)
    //            {
    //                obj2.transform.Rotate(0, 90, 0);
    //            }
    //            else if (gameObject.name == switchName3)
    //            {
    //                obj3.transform.Rotate(0, 90, 0);
    //            }
    //        }
    //    }
    //
    //    
    //}
}
