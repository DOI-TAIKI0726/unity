using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RandomItem : MonoBehaviour
{
    private　float rnd = 0.0f;               //乱数
    private int type = 0;                    //バフデバフの種類
    private float data = 0.1f;               //ルーレット中の切り替えの間隔
    private float count = 0.0f;              //アイテムが決まるまでの時間
    private float itemcount = 0.0f;          //アイテムの表示時間
    private bool roulette = false;           //ルーレット中かどうか
    private Image Img;                       //バフデバフのImage

    //バフのスプライト
    [SerializeField]
    private Sprite[] buff = null;
    //デバフのスプライト
    [SerializeField]
    private Sprite[] debuff = null;
    //ルーレットの最大値
    [SerializeField]
    private float max = 0.0f;
    //バフの割合,1.0～0.0の間
    [SerializeField]
    private float buffratio = 0.0f;
    //サーチのオブジェクト
    [SerializeField]
    private GameObject Serch;
    //プレイヤー
    private GameObject player;
    //バフの時間
    private float buffTime = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        //Imageの情報の取得
        Img = GameObject.Find("BuffImage").GetComponent<Image>();

        //プレイヤーの情報を取得
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //確認用
        //右シフトを押したらアイテムルーレット
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            //ルーレット中じゃない場合
            if (!roulette&&!Img.enabled)
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
                if (data <= 0.0f)
                {
                    //バフデバフ表示の乱数を設定
                    //0は固定
                    rnd = Random.Range(0.0f, max);

                    //デバフが表示されていた場合
                    //バフに切り替える
                    if (rnd > 0 && rnd <= max * buffratio)
                    {
                        //表示するバフの種類を決定
                        type = Random.Range(0, buff.Length);
                        //Imageにバフのスプライトを代入
                        Img.sprite = buff[type];
                        //Debug.Log("buff");
                    }
                    //バフが表示されていた場合
                    //デバフに切り替える
                    else
                    {
                        //表示するデバフの種類を決定
                        type = Random.Range(0, debuff.Length);
                        //Imageにデバフのスプライトを代入
                        Img.sprite = debuff[type];
                        //Debug.Log("debuff");
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
                //0は固定
                rnd = Random.Range(0.0f, max);

                //バフ
                //0は固定
                //buffratioの値によって確率が変わる
                if (rnd >= 0.0f && rnd <= max * buffratio)
                {
                    Buff();
                }
                //デバフ
                else
                {
                    Debuff();
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

    //ルーレットの開始の処理
    public void RouletteStart()
    {
        //ルーレット中じゃない場合
        if (!roulette&&!Img.enabled)
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

    //バフ処理
    public void Buff()
    {
        //バフの種類を決定
        //0は固定
        type = Random.Range(0, buff.Length);
        Img.sprite = buff[type];

        //名前が現在表示されている画像と一緒だった場合
        //スピードアップバフ
        if (buff[type].name == "SpeedUp")
        {
            if (SceneManager.GetActiveScene().name == "ItemCheckScene")
            {
                player.GetComponent<Checkplayer>().BuffSpeed(2.0f, buffTime);
            }
            if (SceneManager.GetActiveScene().name == "Game")
            {
                player.GetComponent<Player>().BuffSpeed(2.0f, buffTime);
            }
        }
        //宝の方向を示すバフ
        else if (buff[type].name == "Tresure Direction")
        {
            //バフ時間の設定
            if (SceneManager.GetActiveScene().name == "ItemCheckScene")
            {
                player.GetComponent<Checkplayer>().BuffSerch(buffTime);
            }
            if (SceneManager.GetActiveScene().name == "Game")
            {
                player.GetComponent<Player>().BuffSerch(buffTime);
            }

            Vector3 playerPos = new Vector3(player.transform.position.x, player.transform.position.y + 1f, player.transform.position.z);

            //サーチのオブジェクトを生成
            Instantiate(Serch, playerPos, Serch.transform.rotation);
        }
        //スタミナ無限バフ
        else if (buff[type].name == "Stamina Limitless")
        {
            //スタミナ無限状態、バフ時間の設定
            if (SceneManager.GetActiveScene().name == "ItemCheckScene")
            {
                player.GetComponent<Checkplayer>().BuffStamina(true, buffTime);
            }
            if (SceneManager.GetActiveScene().name == "Game")
            {
                player.GetComponent<Player>().BuffStamina(true, buffTime);
            }
        }

        Debug.Log(buff[type].name);
    }

    //デバフ処理
    public void Debuff()
    {
        //デバフの種類を決定
        //0は固定
        type = Random.Range(0, debuff.Length);
        Img.sprite = debuff[type];

        //名前が現在表示されている画像と一緒だった場合
        //スピードダウンデバフ
        if (debuff[type].name == "SpeedDown")
        {
            //移動速度半減
            if (SceneManager.GetActiveScene().name == "ItemCheckScene")
            {
                player.GetComponent<Checkplayer>().BuffSpeed(0.5f, buffTime);
            }
            if (SceneManager.GetActiveScene().name == "Game")
            {
                player.GetComponent<Player>().BuffSpeed(0.5f, buffTime);
            }
        }
        //移動操作反転デバフ
        else if (debuff[type].name == "Operation Reversal")
        {
            //引数に-1を入れることで操作を反転させる
            if (SceneManager.GetActiveScene().name == "ItemCheckScene")
            {
                player.GetComponent<Checkplayer>().BuffSpeed(-1f, buffTime);
            }
            if (SceneManager.GetActiveScene().name == "Game")
            {
                player.GetComponent<Player>().BuffSpeed(-1f, buffTime);
            }
        }

        Debug.Log(debuff[type].name);
    }
}
