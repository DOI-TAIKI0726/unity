using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    //ドアを開けられるかどうか
    [System.NonSerialized]
    public bool isOpenDoor = false;

    void Start()
    {
        //各要素の参照や初期化

    }

    void Update()
    {
        if (transform.childCount <= 0)
        {
            isOpenDoor = true;
        }
    }

}