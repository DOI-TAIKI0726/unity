using UnityEngine;
using System.Collections;

using UnityEditor;

public class MapEditor : EditorWindow
{
    //床となるオブジェクト
    private GameObject plane;
    //生成するprefab
    private GameObject prefab;
    //生成するpos
    private Vector3 pos;

    [MenuItem("Terashita / MapEditor")]
    private static void CreateEditorWidow()
    {
        //エディタウィンドウの生成
        MapEditor myWindow = GetWindow<MapEditor>("MapEditor");
        //生成した際の最小サイズの設定
        myWindow.minSize = new Vector2(240, 120);
    }

}