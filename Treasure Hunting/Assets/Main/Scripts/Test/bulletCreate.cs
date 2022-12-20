using UnityEngine;
//砲弾等の射出物の生成管理


public class bulletCreate : MonoBehaviour
{
    //発射感覚
    private int count;
    private bool randomStart;
    private float fireTime;//発射時間
    [SerializeField]
    private GameObject bulletprefab;//弾Obj
    [SerializeField]
    private GameObject ItemBoxprefab;//ItemBoxObj
    //GameManagerスクリプト
    private GameManager gameManagerScript;

    void Start()
    {
        // 任意のフォルダから読み込む
        //bulletprefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Main/Prefabs/test/bullet.prefab", typeof(GameObject));
        //ItemBoxprefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Main/Prefabs/test/ReSpoonItemBox.prefab", typeof(GameObject));


        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Update()
    {
        if (gameManagerScript.quitPanel.activeSelf == false)
        {
            //カウント進める
            count += 1;

            // 3秒ごとに砲弾を発射する
            if (count >= 180)
            {
                count = 0;
                //打ち出すアイテムを乱数で決める
                int RandItem = Random.Range(1, 100);

                if (RandItem <= 10)
                {
                    GameObject ItemBox = Instantiate(ItemBoxprefab, transform.position, Quaternion.identity);
                    Rigidbody bullRb = ItemBox.GetComponent<Rigidbody>();
                    //弾速は自由に設定
                    bullRb.AddForce(transform.forward * 500);
                    // 2秒後に砲弾を破壊する
                    Destroy(ItemBox, 2.0f);
                }
                else
                {
                    GameObject bullet = Instantiate(bulletprefab, transform.position, Quaternion.identity);
                    Rigidbody bullRb = bullet.GetComponent<Rigidbody>();
                    // 弾速は自由に設定
                    bullRb.AddForce(transform.forward * 500);
                    // 2秒後に砲弾を破壊する
                    Destroy(bullet, 2.0f);
                }

            }
        }
    }
}
