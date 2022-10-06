using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomItem : MonoBehaviour
{
    float rnd = 0.0f;           //乱数
    float count = 0.0f;         //カウント
    float itemcount = 0.0f;     //アイテム表示時間
    bool roulette = false;      //ルーレット中かどうか
    public GameObject photo;    //画像のオブジェクト
    Image Img;                  //Image
    public Sprite buff;         //バフのスプライト
    public Sprite debuff;       //デバフのスプライト
    float data = 0.2f;          //ルーレット中の切り替えの間隔

    // Start is called before the first frame update
    void Start()
    {
        //Imageの情報の取得
        Img = photo.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            if(!roulette)
            {
                //ルーレット中に切り替える
                roulette = true;

                //アイテムの表示
                Img.enabled = true;
                Img.sprite = buff;

                //アイテムが決まるまでの時間を設定
                count = 2.0f;

                itemcount = 2.0f;

                Debug.Log("ok");
            }
        }

        //ルーレット中
        if(roulette)
        {
            if(count>0.0f)
            {
                //時間経過
                count -= Time.deltaTime;

                //切り替え
                data -= Time.deltaTime;

                if (data<=0f)
                {
                    if (Img.sprite == debuff) 
                    {
                        Img.sprite = buff;
                    }
                    else if (Img.sprite == buff)
                    {
                        Img.sprite = debuff;
                    }

                    data = 0.2f;
                }
                
            }
            else
            {
                //ルーレット終了
                roulette = false;
                //乱数の設定
                rnd = Random.Range(0.1f, 10.0f);

                //バフ
                if(rnd>=0.1f&&rnd<=6.0f)
                {
                    Img.sprite = buff;
                }
                //デバフ
                else
                {
                    Img.sprite = debuff;
                }

                Debug.Log(rnd);
            }
        }
        else
        {
            //2秒後にアイテム非表示
            if(itemcount>0.0f)
            {
                itemcount -= Time.deltaTime;
            }
            else
            {
                Img.enabled = false;
            }
        }
    }
}
