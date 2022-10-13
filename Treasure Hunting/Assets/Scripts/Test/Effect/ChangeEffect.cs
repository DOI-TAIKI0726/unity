using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEffect : MonoBehaviour
{
    //バフエフェクト
    [SerializeField]
    private GameObject buff;
    //紙吹雪エフェクト
    [SerializeField]
    private GameObject confetti;
    //キラキラエフェクト
    [SerializeField]
    private GameObject Kirakira;
    //カウント用
    [SerializeField]
    private int Count;
    // Start is called before the first frame update
    void Start()
    {
        //バフエフェクトの取得
        buff = GameObject.Find("Buff");
        //紙吹雪エフェクトの取得
        confetti = GameObject.Find("Confetti");
        //キラキラエフェクトの取得
        Kirakira = GameObject.Find("KiraKira");
        //カウント初期化
        Count = 0;
    }


    // Update is called once per frame
    void Update()
    {
        //Tキー押したら
        if (Input.GetKeyDown("t"))
        {
            Count++;
            if (Count >= 4)
            {
                Count = 0;
            }
        }
        //カウントされた数字で切り替え
        switch (Count)
        {
            //全エフェクト非アクティブ
            case 0:       
                buff.SetActive(false);
                confetti.SetActive(false);
                Kirakira.SetActive(false);
                break;
            //バフエフェクトON
            case 1:
                buff.SetActive(true);
                confetti.SetActive(false);
                Kirakira.SetActive(false);
                break;
            //紙吹雪エフェクトON
            case 2:
                buff.SetActive(false);
                confetti.SetActive(true);
                Kirakira.SetActive(false);
                break;
            //キラキラエフェクトON
            case 3:
                buff.SetActive(false);
                confetti.SetActive(false);
                Kirakira.SetActive(true);
                break;
        }
    }
}
