using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

//Panelのオブジェクトにアタッチする
public class PasswordPanel : MonoBehaviour
{
    //パスワードの入力されている値
    private int[] pwData;
    //答えの値  0～pwSpriteデータ数までの値
    private int[] Answer = { 0, 1, 2 };
    //パスワードの値のスプライト
    [SerializeField]
    private Sprite[] pwSprite;
    //パスワードのボタン
    [SerializeField]
    private Image[] pwButton;
    //ドアのオブジェクト
    private GameObject door;
    //クリアしたか
    [System.NonSerialized]
    public bool clear = false;
    //プレイヤーのオブジェクト
    private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        //入力されるデータ数の取得
        pwData = new int[Answer.Length];
        //ドアの情報を取得
        door = GameObject.Find("PwDoor");
        //プレイヤーの情報を取得
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //パスワードのボタンを押された処理
    public void OnClickPassword(int position)
    {
        //パスワードの値の切り替え
        ChangeNumber(position);
        //パスワードのスプライトを切り替え
        ShowNumber(position);
    }

    //パスワードの値の切り替え処理
    void ChangeNumber(int position)
    {
        //tmpにposition番目のパスワードの値を代入
        int tmp = pwData[position];
        //加算
        tmp++;
        tmp %= pwSprite.Length;
        //position番目のパスワードの値にtmpを代入
        pwData[position] = tmp;
    }

    //パスワードのスプライトを切り替え処理
    void ShowNumber(int position)
    {
        //tmpにposition番目のパスワードの値を代入
        int tmp = pwData[position];
        //ボタンのスプライトをtmpに切り替える
        pwButton[position].sprite = pwSprite[tmp];
    }

    //答え合わせの処理
    public void OnClickEnter()
    {
        //入力されている値が答えと一緒だった場合
        if(pwData.SequenceEqual(Answer))
        {
            //パスワードのCanvasを非表示にする
            GameObject.Find("Password").GetComponent<Canvas>().enabled = false;
            //ドアを開く
            door.GetComponent<DoorOpen>().DoorMove();
            //プレイヤーを移動可能にする
            Player.GetComponent<Checkplayer>().isMove = true;
            //クリアした状態にする
            clear = true;
        }
        //間違えていた場合
        else
        {
            //入力されている値をすべて0にする
            for(int i=0;i<pwData.Length;i++)
            {
                pwData[i] = 0;
                pwButton[i].sprite = pwSprite[0];
            }
        }
    }

    //キャンセル処理
    public void OnClickChancel()
    {
        //パスワードのCanvasを非表示にする
        GameObject.Find("Password").GetComponent<Canvas>().enabled = false;
        ////プレイヤーを移動可能にする
        //Player.GetComponent<Checkplayer>().isMove = true;
    }
}
