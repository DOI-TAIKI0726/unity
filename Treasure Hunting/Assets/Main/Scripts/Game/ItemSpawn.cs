using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{   
    //生成するItemBox
    [SerializeField]
    private GameObject itemBox;
    //ItemBox生成のCT
    [SerializeField]
    private float createItemBoxCT;

    //子にItemBoxが無いときに進めるカウント
    private float noItemBoxTime;
    //
    private bool isNoChild = false;

    void Start()
    {
        
    }

    void Update()
    {
        if(isNoChild == true)
        {
            GameObject child;

            noItemBoxTime += Time.deltaTime;
            if(noItemBoxTime >= createItemBoxCT)
            {
                child = Instantiate(itemBox, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity, this.gameObject.transform);
                noItemBoxTime = 0;
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
