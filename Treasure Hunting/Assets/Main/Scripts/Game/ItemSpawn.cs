using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{   
    //生成するオブジェクト
    [SerializeField]
    private GameObject item;
    //生成のCT
    [SerializeField]
    private float createItemCT;

    //子にItemBoxが無いときに進めるカウント
    private float noItemTime;
    //
    private bool isNoChild = false;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if(isNoChild == true)
        {
            GameObject child;

            noItemTime += Time.deltaTime;
            if(noItemTime >= createItemCT)
            {
                child = Instantiate(item, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity, this.gameObject.transform);
                noItemTime = 0;
                isNoChild = false;
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            GameObject child = this.transform.GetChild(0).gameObject;
            Destroy(child);
            isNoChild = true;
        }
    }
}
