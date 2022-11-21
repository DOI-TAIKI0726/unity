//ヒエラルキーでオブジェクトのアクティブ状況を変えられるようにする
//Author:寺下琉生

using UnityEditor;
using UnityEngine;

public static class HierarchyGUI
{
    //ヒエラルキーに表示するトグルのクリック判定の幅
    private const int width = 160;
    //ヒエラルキーに表示するトグルのposXをどれくらい移動させるか
    private const int offset = 10;

    //エディタ起動時に自動で処理を実行させる
    [InitializeOnLoadMethod]
    private static void Intialize()
    {
        //ヒエラルキーを変更する場合に使用
        //OnGUIを通るたびにOnGUIの引数を加算していく
        EditorApplication.hierarchyWindowItemOnGUI += OnGUI;
    }

    private static void OnGUI(int instanceID, Rect selectionRect)
    {
        //instanceIDをGameObject参照に変換
        GameObject go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

        if(go == null)
        {
            return;
        }

        //トグル生成位置指定
        Rect pos = selectionRect;
        //ヒエラルキーの端からoffset分だけトグルの位置を動かす
        pos.x = pos.xMax - offset;
        //トグルの幅設定
        pos.width = width;

        //posで指定した位置にトグルを生成し、オブジェクトのアクティブ状況を切り替えられるようにする
        bool active = GUI.Toggle(pos, go.activeSelf, string.Empty);

        if (active == go.activeSelf)
        {
            return;
        }

        //ヒエラルキーで行われた変更内容にgoを更新する
        Undo.RecordObject(go, $"{(active ? "Activate" : "Deactivate")} GameObject '{go.name}'");
        //ヒエラルキーで行われた変更を反映
        go.SetActive(active);
        //ヒエラルキーで行われた変更を保存
        EditorUtility.SetDirty(go);
    }
}
