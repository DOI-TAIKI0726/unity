using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    //跳ね返すときの速さ
    [SerializeField]
    private float boundSpeed;
    //跳ね返す単位ベクトルにかける倍数
    [SerializeField]
    private float bounceVector;
    
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Player")
        {
            //跳ね返りの処理
            //衝突した面の、接触した点における法線ベクトルを取得
            Vector3 normal = col.contacts[0].normal;
            //衝突した速度ベクトルを単位ベクトルにする
            Vector3 velocity = col.rigidbody.velocity.normalized;
            //x,z方向に対して逆向きの法線ベクトルを取得
            velocity += new Vector3(-normal.x * bounceVector, 0f, -normal.z * bounceVector);
  
            //取得した法線ベクトルに跳ね返す速さをかけて、跳ね返す
            col.rigidbody.AddForce(velocity * boundSpeed, ForceMode.VelocityChange);
        }
    }
}
