using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateItem : MonoBehaviour
{
    [SerializeField]
    float rotX;//x軸の回転速度
    [SerializeField]
    float rotY;//ｙ軸の回転速度
    [SerializeField]
    float rotZ;//ｚ軸の回転速度

    //GameManager
    private GameManager gameManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagerScript.quitPanel.activeSelf == false)
        {
            gameObject.transform.Rotate(new Vector3(rotX, rotY, rotZ) * Time.deltaTime);
        }
    }
}
