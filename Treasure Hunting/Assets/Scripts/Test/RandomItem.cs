using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomItem : MonoBehaviour
{
    private　float rnd = 0.0f;               //乱数
    private int type = 0;                    //バフデバフの種類
    private float count = 0.0f;              //アイテムが決まるまでの時間
    private float itemcount = 0.0f;          //アイテムの表示時間
    private bool roulette = false;           //ルーレット中かどうか
    private Image Img;                       //バフデバフのImage
    [SerializeField]
    private Sprite[] buff = null;            //バフのスプライト
    [SerializeField]
    private Sprite[] debuff = null;          //デバフのスプライト
    private float data = 0.1f;               //ルーレット中の切り替えの間隔

    // Start is called before the first frame update
    void Start()
    {
        //Imageの情報の取得
        Img = GameObject.Find("BuffDebuff").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //確認用
        //右シフトを押したらアイテムルーレット
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            //ルーレット中じゃない場合
            if (!roulette)
            {
                //ルーレット中に切り替える
                roulette = true;

                //アイテムの表示
                Img.enabled = true;

                //ルーレットの始まりはbuff[0]始まり固定
                Img.sprite = buff[0];

                //アイテムが決まるまでの時間を設定
                count = 2.0f;
                //アイテム決定後の表示時間の設定
                itemcount = 2.0f;

                //確認用
                Debug.Log("ok");
            }
        }

        //ルーレット中
        if (roulette)
        {
            if (count > 0.0f)
            {
                //時間経過
                count -= Time.deltaTime;

                //切り替え
                data -= Time.deltaTime;

                //表示の切り替え処理
                if (data <= 0f)
                {
                    //バフデバフ表示の乱数を設定
                    rnd = Random.Range(0f, 1f);

                    //デバフが表示されていた場合
                    //バフに切り替える
                    if (rnd > 0 && rnd <= 0.5)
                    {
                        //表示するバフの種類を決定
                        type = Random.Range(0, buff.Length);
                        //Imageにバフのスプライトを代入
                        Img.sprite = buff[type];
                        Debug.Log("buff");
                    }
                    //バフが表示されていた場合
                    //デバフに切り替える
                    else
                    {
                        //表示するデバフの種類を決定
                        type = Random.Range(0, debuff.Length);
                        //Imageにデバフのスプライトを代入
                        Img.sprite = debuff[type];
                        Debug.Log("debuff");
                    }

                    //切り替えの間隔を設定
                    data = 0.1f;
                }

            }
            else
            {
                //ルーレット終了
                roulette = false;
                //乱数の設定
                rnd = Random.Range(0.1f, 10.0f);

                //バフ
                if (rnd >= 0.1f && rnd <= 6.0f)
                {
                    //バフの種類を決定
                    type = Random.Range(0, buff.Length);
                    Img.sprite = buff[type];
                }
                //デバフ
                else
                {
                    //デバフの種類を決定
                    type = Random.Range(0, debuff.Length);
                    Img.sprite = debuff[type];
                }

                //確認用
                Debug.Log(rnd);
            }
        }
        //ルーレット終了後
        else
        {
            //2秒後にアイテム非表示
            if (itemcount > 0.0f)
            {
                //表示時間の減算
                itemcount -= Time.deltaTime;
            }
            else
            {
                //アイテムを非表示
                Img.enabled = false;
            }
        }
    }
}
