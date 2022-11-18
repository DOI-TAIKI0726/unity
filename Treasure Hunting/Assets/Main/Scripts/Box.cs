﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(Box))]
public class BoxEditor : Editor
{
    //グリッドの幅
    public const float GRIDSIZE = 1f;

    void OnSceneGUI()
    {
        //このスクリプトがついてるオブジェクト
        Box box = target as Box;
        //グリッドの色
        Color color = Color.cyan * 0.7f;
        //グリッドの中心座標
        Vector3 orig = new Vector3(0.0f,0.0f,0.0f);
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
        Vector3 position = box.transform.position;
        position.x = Mathf.Floor(position.x / GRIDSIZE) * GRIDSIZE;
        position.y = Mathf.Floor(position.y / GRIDSIZE) * GRIDSIZE;
        position.z = Mathf.Floor(position.z / GRIDSIZE) * GRIDSIZE;
        box.transform.position = position;

        //Sceneビュー更新
        EditorUtility.SetDirty(target);
    }

    //フォーカスが外れたときに実行
    void OnDisable()
    {
        //Sceneビュー更新
        EditorUtility.SetDirty(target);
    }
}
#endif //UNITY_EDITOR

public class Box : MonoBehaviour
{

}