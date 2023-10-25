using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBullet : MonoBehaviour
{

    //それぞれの位置を保存する変数
    //スタート地点
    private Vector3 bulletPostion;

    private float time = 0;

    private void Awake()
    {
        bulletPostion  = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //弾の進む割合をTime.deltaTimeで決める
        time += Time.deltaTime;
        transform.Translate(Vector3.right * time);
    }

    void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
    }
}
