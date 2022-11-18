using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //RotateManagerのクリア時に呼びドアを開く
    public void DoorMove()
    {
        StartCoroutine("DoorRotate");
    }

    //ドアを開く
    IEnumerator DoorRotate()
    {
        for (int turn = 0; turn < 90; turn++)
        {
            transform.Rotate(0, 1, 0);
            yield return new WaitForSeconds(0.01f);
        }

    }

    //ドアを閉じる処理を呼び出す
    public void CloseDoor()
    {
        StartCoroutine("CloseRotate");
    }

    //ドアを閉じる
    IEnumerator CloseRotate()
    {
        for (int turn = 0; turn < 90; turn++)
        {
            transform.Rotate(0, -1, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
