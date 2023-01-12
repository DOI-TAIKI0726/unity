using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircleController : MonoBehaviour
{
    //魔法陣
    private GameObject ObjMagicCircle;
    // フェードアウトするまでの時間
    public float fadeTime;
    //踏んでる間の時間計測
    private float StepOnTime;
    //踏んでない間の時間計測
    private float ExitTime;
    //スプライトレンダラー
    private SpriteRenderer render;
    //スイッチを踏んでいるかどうか
    bool SwitchStepOn;
   

    // Start is called before the first frame update
    void Start()
    {
        //魔法陣を見つけてくる
        ObjMagicCircle = transform.GetChild(0).gameObject;
        //renderにぶち込む
        render = ObjMagicCircle.GetComponent<SpriteRenderer>();
        //魔法陣は最初アクティブに
        ObjMagicCircle.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //スイッチを踏んでいる時
        if (SwitchStepOn == true)
        {
            StepOnTime += Time.deltaTime;
            if (StepOnTime < fadeTime)
            {
                //時間経過でアルファ値上げる
                float alpha = 0.1f + StepOnTime / fadeTime;
                Color color = render.color;
                color.a = alpha;
                render.color = color;
            }
        }
        //スイッチを踏んでいない時
        else
        { 
            ExitTime += Time.deltaTime;
            if (ExitTime > fadeTime)
            {
                //時間経過でアルファ値下げる
                float alpha = 0.1f - ExitTime / fadeTime;
                Color color = render.color;
                color.a -= 0.005f;
                render.color = color;
                
            }
        }
    }

    //踏み続けている間
    void OnTriggerStay(Collider other)
    {
        //踏んでいる
        SwitchStepOn = true;
        //踏み続けている時間をリセット
        ExitTime = 0.0f;
    }

    //踏んでいない間
    void OnTriggerExit(Collider other)
    {
        //踏んでいない
        SwitchStepOn = false;
        StepOnTime = 0f;
    }

}

