/*==============================
 //ミニマップの範囲外でも表示するアイコン
 /Author:山田祐人
 ==============================*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapIcon : MonoBehaviour
{
    //ミニマップカメラ
    private Camera minimapCamera;
    //アイコンに対応するオブジェクト
    private GameObject iconTarget;
    //表示範囲のオフセット
    [SerializeField]
    private float rangeRadiusOffset;

    //必要なコンポーネント
    private SpriteRenderer spRender;

    private float minimapRangeRaudis;       //ミニマップの表示範囲
    private float defaultPosY;              //アイコンのデフォルトY座標
    private float normalAlpha = 1.0f;       //範囲内のアルファ値
    private float outRangeAlpha = 0.5f;     //範囲外のアルファ値

    // Start is called before the first frame update
    void Start()
    {
        //ミニマップカメラを取得
        minimapCamera = GameObject.Find("MinimapCamera").GetComponent<Camera>();
        //ミニマップの表示範囲を取得
        minimapRangeRaudis = minimapCamera.orthographicSize;

        spRender = gameObject.GetComponent<SpriteRenderer>();
        //アイコンのY座標を取得
        defaultPosY = transform.position.y;

        //親のオブジェクトを取得
        iconTarget = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        DispIcon();
    }

    //オブジェクトがミニマップ範囲内にあるか確認
    private bool CheckInsideMap()
    {
        var CameraPos = minimapCamera.transform.position;
        var targetPos = iconTarget.transform.position;

        //直線距離で判定するため、yを0扱いにする
        CameraPos.y = targetPos.y = 0;

        return Vector3.Distance(CameraPos, targetPos) <= minimapRangeRaudis - rangeRadiusOffset;
    }

    //アイコン表示の更新
    private void DispIcon()
    {
        //アイコンを表示する座標
        var iconPos = new Vector3(iconTarget.transform.position.x, defaultPosY, iconTarget.transform.position.z);

        //ミニマップ範囲内の場合はそのまま表示する
        if(CheckInsideMap())
        {
            spRender.color = new Color(spRender.color.r, spRender.color.g, spRender.color.b, normalAlpha);
            transform.position = iconPos;
            return;
        }

        //マップ範囲外な場合、ミニマップ端までのベクトルを求めて半透明で表示
        spRender.color = new Color(spRender.color.r, spRender.color.g, spRender.color.b, outRangeAlpha);
        var centerPos = new Vector3(minimapCamera.transform.position.x, defaultPosY, minimapCamera.transform.position.z);
        var offset = iconPos - centerPos;
        transform.position = centerPos + Vector3.ClampMagnitude(offset, minimapRangeRaudis - rangeRadiusOffset);
    }
}
