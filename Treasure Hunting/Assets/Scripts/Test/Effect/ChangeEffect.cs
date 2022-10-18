using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEffect : MonoBehaviour
{
    //エフェクトの数の配列
    private GameObject[] EffectNumber;
    //エフェクトの切り替えカウント
    [SerializeField]
    private int ChangeCount;
    //子の総数
    [SerializeField]
    private int ChildTotalCount;
    

    // Start is called before the first frame update
    void Start()
    {

        //子の総数を取得して格納
        ChildTotalCount = transform.childCount;

        EffectNumber = new GameObject[ChildTotalCount];

        //子の総数分回す
        for (int COUNT = 0;COUNT < ChildTotalCount; COUNT++)
        {
            EffectNumber[COUNT] = this.transform.GetChild(COUNT).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //カウントされた数字で切り替え
        switch (ChangeCount)
        {
            //バフエフェクトON
            case 0:
                EffectNumber[0].SetActive(true);
                EffectNumber[1].SetActive(false);
                EffectNumber[2].SetActive(false);
                EffectNumber[3].SetActive(false);
                break;
            //紙吹雪エフェクトON
            case 1:
                EffectNumber[0].SetActive(false);
                EffectNumber[1].SetActive(true);
                EffectNumber[2].SetActive(false);
                EffectNumber[3].SetActive(false);
                break;
            //キラキラエフェクトON
            case 2:
                EffectNumber[0].SetActive(false);
                EffectNumber[1].SetActive(false);
                EffectNumber[2].SetActive(true);
                EffectNumber[3].SetActive(false);
                break;
            //デバフエフェクトON
            case 3:
                EffectNumber[0].SetActive(false);
                EffectNumber[1].SetActive(false);
                EffectNumber[2].SetActive(false);
                EffectNumber[3].SetActive(true);
                break;

        }
    }
}
