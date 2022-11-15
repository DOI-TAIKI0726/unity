using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class testTutorial : MonoBehaviour
{
    //進行状況の列挙
    private enum Phase
    {
        
        walk,
        dash,
        passWord,
        key,
        end
    };

    private Phase curPhase = Phase.walk;//現在の進行状況
    private Phase lotPhase;             //前の進行状況

    private GameObject cursor;          //矢印オブジェクト
    private GameObject player;          //プレイヤーオブジェクト
    private GameObject pwPanel;         //パスワードパネルのオブジェクト
    private Text tutorialText;          //説明分テキスト
    private Collider pwSwitchCol;       //㎺switchのコライダー

    private Toggle walkToggle;//歩くToggle
    private Toggle dashToggle;//走るToggle
    private Toggle pwToggle;  //パスワードToggle
    private Toggle keyToggle; //鍵Toggle

    private int timeCnt;       
    private int textCnt;       //Text表示スピード
    private int textCharNumber;//何文字目かを格納する
    private string displayText;//一文字ずつ格納する
    private int limitCnt = 2;  //何秒間歩く、走らせる
    private string[] texts = { "WASDで移動をしてみよう",
                               "上のスタミナゲージに注意しながら\nWASD+LShiftでダッシュしよう",
                               "矢印の先にあるスイッチに触れ\n220となるようにクリックしよう",
                               "3つの鍵を取り指定された扉の前で\nEキーを押そう",
                               "チュートリアルは以上です\nEnterキーでゲームスタート"};

    // Start is called before the first frame update
    void Start()
    {
        //プレイヤー情報取得
        player = GameObject.FindGameObjectWithTag("Player");

        //カーソルオブジェクト
        cursor = GameObject.Find("Player/cursor");
        //非表示にする
        cursor.SetActive(false);
   
        //パスワードswitch情報取得
        GameObject pwSwitch = GameObject.Find("PassObj/PwSwicth");
        //パスワードswitchのコライダー取得
        pwSwitchCol = pwSwitch.GetComponent<Collider>();
        //当たり判定をOff
        pwSwitchCol.isTrigger = false;
        //パスワードパネルの情報を取得
        pwPanel = GameObject.Find("PasswordPanel");

        //tutorialTextのゲームオブジェクト取得
        GameObject tutorialTextObj = GameObject.Find("TutorialText");
        //テキストコンポーネント取得
        tutorialText = tutorialTextObj.GetComponent<Text>();

        //walkToggle取得
        GameObject walkToggleObj = GameObject.Find("walkToggle");
        walkToggle = walkToggleObj.GetComponent<Toggle>();
        //通常の色にする
        SetColorToggle(walkToggle, false);

        //dashToggle取得
        GameObject dashToggleObj = GameObject.Find("dashToggle");
        dashToggle = dashToggleObj.GetComponent<Toggle>();
        //半透明にする
        SetColorToggle(dashToggle,true);

        //pwToggle取得    
        GameObject pwToggleObj = GameObject.Find("pwToggle");
        pwToggle = pwToggleObj.GetComponent<Toggle>();
        //半透明にする
        SetColorToggle(pwToggle, true);

        //keyToggle取得
        GameObject keyToggleObj = GameObject.Find("keyToggle");
        keyToggle = keyToggleObj.GetComponent<Toggle>();
        //半透明にする
        SetColorToggle(keyToggle, true);
        
    }

    // Update is called once per frame
    void Update()
    {
        //phaseの更新
        ChangePhase(curPhase);
        
    }

    void ChangePhase(Phase phase)
    {
        switch (phase)
        {
            case Phase.walk:
                //Text表示
                Textdisplay(texts[(int)phase]);

                //WASDキーが入力されたらDashフェイズに移行
                if (Input.GetKey(KeyCode.W) ||
                    Input.GetKey(KeyCode.A) ||
                    Input.GetKey(KeyCode.S) ||
                    Input.GetKey(KeyCode.D))
                {

                    timeCnt++;
                    //指定カウントを超えたら
                    if (timeCnt / 60 >= limitCnt)
                    {
                        //ダッシュphaseに変更
                        curPhase = Phase.dash;
                        //walkToggleにチェック入れる
                        walkToggle.isOn = true;
                        //半透明にする
                        SetColorToggle(walkToggle, true);
                        timeCnt = 0;
                    }
                }
                break;

            case Phase.dash:
                //通常の色に戻す
                SetColorToggle(dashToggle, false);
                //Text変更
                Textdisplay(texts[(int)phase]);

                //dashしてる時に
                if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) ||
                    Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift) ||
                    Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift) ||
                    Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))
                {
                    timeCnt++;
                    //指定カウントを超えたとき
                    if (timeCnt / 60 >= limitCnt)
                    {
                        //keyフェイズに変更
                        curPhase = Phase.passWord;
                        //dashToggleにチェックを入れる
                        dashToggle.isOn = true;
                        //半透明にする
                        SetColorToggle(dashToggle, true);
                    }
                }
                break;

            case Phase.passWord:
                //通常の色に戻す
                SetColorToggle(pwToggle, false);
                //Text変更
                Textdisplay(texts[(int)phase]);

                //カーソル表示
                cursor.SetActive(true);
                //pwSwitchの当たり判定ON
                pwSwitchCol.isTrigger = true;
                //矢印をターゲットの方向へ
                aim(pwSwitchCol.gameObject.transform.position);

                //パスワードをクリアしたらKeyのフェードに移行
                if (pwPanel.GetComponent<PasswordPanel>().clear)
                {
                    //Keyフェーズに変更
                    curPhase = Phase.key;
                    //pwToggleにチェックを入れる
                    pwToggle.isOn = true;
                    //カーソルを非表示
                    cursor.SetActive(false);
                    //半透明にする
                    SetColorToggle(pwToggle, true);
                }
                break;

            case Phase.key:
                //通常の色に戻す
                SetColorToggle(keyToggle, false);
                Textdisplay(texts[(int)phase]);

                //扉が空いたら
                if (player.GetComponent<Player>().isKeyuse == true)
                {
                    //endフェイズに移行
                    curPhase = Phase.end;
                    //KeyToggleにチェックを入れる
                    keyToggle.isOn = true;
                    //半透明にする
                    SetColorToggle(keyToggle, true);
                }
                break;

            case Phase.end:
                //Text変更
                Textdisplay(texts[(int)phase]);

                if (Input.GetKey(KeyCode.Return))
                {
                    SceneManager.LoadScene("Game");
                }
                break;
        }
    }

    //ターゲット方向を向く
    void aim(Vector3 targetPos)
    {
        // 対象物と自分自身の座標からベクトルを算出
        Vector3 dir = targetPos - cursor.transform.position;
        // もし上下方向の回転はしないようにしたければ以下のようにする。
        dir.y = 0f;

        // Quaternion(回転値)を取得
        Quaternion quaternion = Quaternion.LookRotation(dir);
        
        //度数に変換
        var angleY = quaternion.eulerAngles.y;

        //カーソルの向き変更
        cursor.transform.eulerAngles = new Vector3(90, angleY);
    }

    //Toggleのマテリアル変更
    //trueで半透明,falseで通常
    void SetColorToggle(Toggle toggle,bool alpha)
    {
        //背景チェックボタン取得
        Image backImage = toggle.transform.GetChild(0).GetComponent<Image>();
        //チェックマーク取得
        Image Checkmark = backImage.transform.GetChild(0).GetComponent<Image>();
        //Toogleの文字取得
        Text TextImage = toggle.transform.GetChild(1).GetComponent<Text>();
        
        if (alpha)
        {
            //α値を半透明
            backImage.color = new Color(1, 1, 1, 0.5f);
            Checkmark.color = new Color(1, 1, 1, 0.5f);
            TextImage.color = new Color(0, 0, 0, 0.1f);
        }
        else
        {
            //α値を1にする
            backImage.color = new Color(1, 1, 1, 1f);
            Checkmark.color = new Color(1, 1, 1, 1f);
            TextImage.color = new Color(1, 1, 1, 1f);
        }
    }

    //一文字ずつ表示
    void Textdisplay(string text)
    {
        //フェイズが変わり次のText表示するとき初期化
        if(lotPhase != curPhase)
        {
            displayText = "";
            textCharNumber = 0;
            lotPhase = curPhase;
        }

        textCnt++;
        //１０カウントに一文字ずつ表示
        if (textCnt % 10 == 0)
        {
            //表示する文字が最後まで行ってないとき
            if (textCharNumber != text.Length)
            {
                //次の一文字を格納
                displayText += text[textCharNumber];
                textCharNumber++;
            }
            
            //一文字表示
            tutorialText.text = displayText;
        }
    }
}


