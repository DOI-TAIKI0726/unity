//オブジェクトを指定した値ずつだけ動かせるようにする
//Author:寺下琉生

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(ObjectMoveLimit))]
public class ObjectMoveLimitEditor : Editor
{
    //グリッドの幅(この値分動かせる)
    public const float GRIDSIZE = 1f;

    void OnSceneGUI()
    {
        //処理コードの形でインスタンス
        ObjectMoveLimit myObject = target as ObjectMoveLimit;
        //グリッドの色
        Color color = Color.cyan * 0.7f;
        //グリッドの中心座標
        Vector3 orig = new Vector3(0.0f, 0.0f, 0.0f);
        //グリッドの分割数/2
        const int num = 50;

        //グリッド描画
        for (int x = -num; x <= num; x++)
        {
            //グリッドの生成
            Vector3 gridPos = orig + Vector3.right * x * GRIDSIZE;
        }
        for (int y = -num; y <= num; y++)
        {
            Vector3 gridPos = orig + Vector3.up * y * GRIDSIZE;
        }

        //オブジェクトを移動させたときにグリッドの位置にそろえるようにする
        Vector3 position = myObject.transform.position;
        position.x = Mathf.Floor(position.x / GRIDSIZE) * GRIDSIZE;
        position.y = Mathf.Floor(position.y / GRIDSIZE) * GRIDSIZE;
        position.z = Mathf.Floor(position.z / GRIDSIZE) * GRIDSIZE;
        myObject.transform.position = position;

        //Sceneビュー更新
        EditorUtility.SetDirty(target);
    }

    //フォーカスが外れたときに実行
    void OnDisable()
    {
        //Sceneビュー更新
        EditorUtility.SetDirty(target);
    }

    //インスペクター拡張
    public override void OnInspectorGUI()
    {
        const int rot = 90;
         
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("X回転"))
        {
            ObjectMoveLimit myObject = target as ObjectMoveLimit;
            myObject.transform.Rotate(new Vector3(rot, 0.0f, 0.0f));

            //Sceneビュー更新
            EditorUtility.SetDirty(target);
        }

        if (GUILayout.Button("Y回転"))
        {
            ObjectMoveLimit myObject = target as ObjectMoveLimit;
            myObject.transform.Rotate(new Vector3(0.0f, rot, 0.0f));

            //Sceneビュー更新
            EditorUtility.SetDirty(target);
        }

        if (GUILayout.Button("Z回転"))
        {
            ObjectMoveLimit myObject = target as ObjectMoveLimit;
            myObject.transform.Rotate(new Vector3(0.0f, 0.0f, rot));

            //Sceneビュー更新
            EditorUtility.SetDirty(target);
        }
        EditorGUILayout.EndHorizontal();
    }
}
#endif //UNITY_EDITOR

public class ObjectMoveLimit : MonoBehaviour
{
}