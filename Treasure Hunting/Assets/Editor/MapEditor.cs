using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

public class MapEditor : EditorWindow
{
    //親にするオブジェクト
    private GameObject parent;
    //生成するオブジェクト
    private GameObject createObj;
    //X方向に生成する数
    private int createNumX = 1;
    //Y方向に生成する数
    private int createNumY = 1;
    //Z方向に生成する数
    private int createNumZ = 1;
    //生成する位置X
    private float createPosX = 0.0f;
    //生成する位置Y
    private float createPosY = 0.0f;
    //生成する位置Z
    private float createPosZ = 0.0f;
    //オブジェクトを生成した際に間隔をどれくらい開けるかX
    private float objIntervalX = 0.0f;
    //オブジェクトを生成した際に間隔をどれくらい開けるかY
    private float objIntervalY = 0.0f;
    //オブジェクトを生成した際に間隔をどれくらい開けるかZ
    private float objIntervalZ = 0.0f;
    //生成したオブジェクトの回転X
    private float objRotetionX = 0.0f;
    //生成したオブジェクトの回転Y
    private float objRotetionY = 0.0f;
    //生成したオブジェクトの回転Z
    private float objRotetionZ = 0.0f;

    //ウィンドウのスクロール位置
    private Vector2 scrollpos = Vector2.zero;
    //項目折り畳み
    private bool isOpenX, isOpenY, isOpenZ = false;
    //
    private bool isAddX, isAddY, isAddZ = false;

    //メニューからウィンドウ表示できるようにする
    [MenuItem("Terashita / MapEditor")]
    private static void CreateEditorWidow()
    {
        //エディタウィンドウの生成
        MapEditor myWindow = GetWindow<MapEditor>(false, "MapEditor");
        //生成した際の最小サイズの設定
        myWindow.minSize = new Vector2(120, 200);
    }

    void OnGUI()
    {
        try
        {
            GUILayout.Label("UV展開したモデルの方はScaleがデカすぎて上手く使えないです", EditorStyles.boldLabel);


            //描画範囲が足りなければスクロールできるようにする
            scrollpos = EditorGUILayout.BeginScrollView(scrollpos);

            GUILayout.Label("オブジェクト設定", EditorStyles.boldLabel);
            //親オブジェクト指定
            GUILayout.Label("親にするオブジェクト", EditorStyles.boldLabel);
            parent = EditorGUILayout.ObjectField("", parent, typeof(GameObject), true) as GameObject;
            //ウィンドウ上でprefadObjにオブジェクトを登録する
            GUILayout.Label("生成するオブジェクト", EditorStyles.boldLabel);
            createObj = EditorGUILayout.ObjectField("", createObj, typeof(GameObject), true) as GameObject;

            GUILayout.Label("", EditorStyles.boldLabel);

            GUILayout.Label("※各生成数は必ず1以上にする(初期で1入れてる)", EditorStyles.boldLabel);

            isOpenX = EditorGUILayout.Foldout(isOpenX, "X方向");
            if (isOpenX == true)
            {
                //生成数
                GUILayout.Label("生成数", EditorStyles.boldLabel);
                createNumX = int.Parse(EditorGUILayout.TextField("", createNumX.ToString()));
                //生成位置
                GUILayout.Label("最初に生成するオブジェクトのpos.x", EditorStyles.boldLabel);
                createPosX = int.Parse(EditorGUILayout.TextField("", createPosX.ToString()));

                //posをcreateObj分増やしたり減らしたりする
                GUILayout.Label("生成するオブジェクト分posを増減", EditorStyles.boldLabel);

                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("+") == true)
                {
                    createPosX += createObj.GetComponent<Transform>().localScale.x;
                }
                if (GUILayout.Button("-") == true)
                {
                    createPosX -= createObj.GetComponent<Transform>().localScale.x;
                }
                if (GUILayout.Button("0") == true)
                {
                    createPosX = 0.0f;
                }
                EditorGUILayout.EndHorizontal();

                //間隔
                GUILayout.Label("生成オブジェクトどうしの間隔", EditorStyles.boldLabel);
                objIntervalX = int.Parse(EditorGUILayout.TextField("", objIntervalX.ToString()));
                //回転
                GUILayout.Label("回転", EditorStyles.boldLabel);
                objRotetionX = int.Parse(EditorGUILayout.TextField("", objRotetionX.ToString()));
            }

            GUILayout.Label("", EditorStyles.boldLabel);

            isOpenY = EditorGUILayout.Foldout(isOpenY, "Y方向");
            if (isOpenY == true)
            {
                //生成数
                GUILayout.Label("生成数", EditorStyles.boldLabel);
                createNumY = int.Parse(EditorGUILayout.TextField("", createNumY.ToString()));
                //生成位置
                GUILayout.Label("最初に生成するオブジェクトのpos.y", EditorStyles.boldLabel);
                createPosY = int.Parse(EditorGUILayout.TextField("", createPosY.ToString()));

                //posをcreateObj分増やしたり減らしたりする
                GUILayout.Label("生成するオブジェクト分posを増減", EditorStyles.boldLabel);

                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("+") == true)
                {
                    createPosY += createObj.GetComponent<Transform>().localScale.y;
                }
                if (GUILayout.Button("-") == true)
                {
                    createPosY -= createObj.GetComponent<Transform>().localScale.y;
                }
                if (GUILayout.Button("0") == true)
                {
                    createPosX = 0.0f;
                }
                EditorGUILayout.EndHorizontal();

                //間隔
                GUILayout.Label("生成オブジェクトどうしの間隔", EditorStyles.boldLabel);
                objIntervalY = int.Parse(EditorGUILayout.TextField("", objIntervalY.ToString()));
                //回転
                GUILayout.Label("回転", EditorStyles.boldLabel);
                objRotetionY = int.Parse(EditorGUILayout.TextField("", objRotetionY.ToString()));
            }

            GUILayout.Label("", EditorStyles.boldLabel);

            isOpenZ = EditorGUILayout.Foldout(isOpenZ, "Z方向");
            if (isOpenZ == true)
            {
                //生成数
                GUILayout.Label("生成数", EditorStyles.boldLabel);
                createNumZ = int.Parse(EditorGUILayout.TextField("", createNumZ.ToString()));
                //生成位置
                GUILayout.Label("最初に生成するオブジェクトのpos.z", EditorStyles.boldLabel);
                createPosZ = int.Parse(EditorGUILayout.TextField("", createPosZ.ToString()));

                //posをcreateObj分増やしたり減らしたりする
                GUILayout.Label("生成するオブジェクト分posを増減", EditorStyles.boldLabel);

                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("+") == true)
                {
                    createPosZ += createObj.GetComponent<Transform>().localScale.x;
                }
                if (GUILayout.Button("-") == true)
                {
                    createPosZ -= createObj.GetComponent<Transform>().localScale.x;
                }
                if (GUILayout.Button("0") == true)
                {
                    createPosZ = 0.0f;
                }
                EditorGUILayout.EndHorizontal();

                //間隔
                GUILayout.Label("生成オブジェクトどうしの間隔", EditorStyles.boldLabel);
                objIntervalZ = int.Parse(EditorGUILayout.TextField("", objIntervalZ.ToString()));
                //回転
                GUILayout.Label("回転", EditorStyles.boldLabel);
                objRotetionZ = int.Parse(EditorGUILayout.TextField("", objRotetionZ.ToString()));
            }

            GUILayout.Label("", EditorStyles.boldLabel);

            //スクロール終了箇所
            EditorGUILayout.EndScrollView();
            if (GUILayout.Button("生成する"))
            {
                Create();
            }
            GUILayout.Label("", EditorStyles.boldLabel);
        }
        catch (System.FormatException) { }
    }

    //OnGUIで設定したようにオブジェクトを生成
    void Create()
    {
        if (createObj == null)
        {
            return;
        }

        //オブジェクト生成用
        Vector3 pos;

        for (int x = 0; x < createNumX; x++)
        {
            pos.x = createPosX + (objIntervalX * x);

            for (int y = 0; y < createNumY; y++)
            {
                //Box.csの関係で1ずつしか動かせないようにしてるので小数点を切り捨てる
                pos.y = createPosY + (objIntervalY * y);

                for (int z = 0; z < createNumZ; z++)
                {
                    pos.z = createPosZ + (objIntervalZ * z);

                    GameObject obj = Instantiate(createObj, pos, Quaternion.Euler(objRotetionX,objRotetionY,objRotetionZ)) as GameObject;
                    obj.name = createObj.name;
                    if (parent)
                    {
                        obj.transform.parent = parent.transform;
                        Undo.RegisterCreatedObjectUndo(obj, "MapEditor");
                    }
                    pos.z += objIntervalZ;
                }
                pos.y += objIntervalY;
            }
            pos.x += objIntervalX;
        }
    }
}