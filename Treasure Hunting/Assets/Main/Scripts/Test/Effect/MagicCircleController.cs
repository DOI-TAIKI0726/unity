using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircleController : MonoBehaviour
{
    private GameObject ObjMagicCircle;
    // フェードアウトするまでの時間
    public float fadeTime;
    private float time;
    private SpriteRenderer render;
    private GameObject ObjPlayer;
    bool SwitchStepOn;
   

    // Start is called before the first frame update
    void Start()
    {
        ObjMagicCircle = GameObject.Find("MagicCircle");
        render = ObjMagicCircle.GetComponent<SpriteRenderer>();
        ObjPlayer = GameObject.Find("Player");
        ObjMagicCircle.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (SwitchStepOn == true)
        {
            Debug.Log("踏み続けている");
            time += Time.deltaTime;
            if (time < fadeTime)
            {
                if(ObjMagicCircle.activeSelf == true)
                {
                    float alpha = 0.1f + time / fadeTime;
                    Color color = render.color;
                    color.a = alpha;
                    render.color = color;
                }
            }
        }
        else 
        {
            Debug.Log("踏んでない");
            time += Time.deltaTime;
            if (time < fadeTime)
            {
                //ObjMagicCircle.SetActive(false);

                if(ObjMagicCircle.activeSelf == false)
                {
                    float alpha = 1.0f - time / fadeTime;
                    Color color = render.color;
                    color.a = alpha;
                    render.color = color;
                }
            }
        }
    }

    //踏み続けている間
    void OnCollisionStay(Collision other)
    {
        SwitchStepOn = true;
    }

    //踏んでいない
    void OnCollisionExit(Collision other)
    {
        SwitchStepOn = false;
    }

}

