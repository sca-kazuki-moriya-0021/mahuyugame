using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    [SerializeField]
    GameObject TimeBulletSpawn;
    [SerializeField]
    GameObject BulletSpawn;
    [SerializeField]
    GameObject SnakeSpawn;
    private float angle;
    private Vector3 startPos;
    private float elapsedTime = 0.0f;
    private int phase = 0; // フェーズを管理

    public float speed = 2.0f; // 移動速度
    public float amplitudeX = 3.0f; // X軸の振幅
    public float amplitudeY = 1.0f; // Y軸の振幅
    public float skill1Duration = 5.0f; // スキル1の持続時間
    public float danmakuDuration = 10.0f; // 弾幕の持続時間
    public float skill2Duration = 5.0f; // スキル2の持続時間

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        angle += Time.deltaTime * speed;
        elapsedTime += Time.deltaTime;

        float x = startPos.x + Mathf.Sin(angle * 2) * amplitudeX;
        float y = startPos.y + Mathf.Sin(angle) * amplitudeY;

        // Z軸の位置は固定（2D空間に固定）
        transform.position = new Vector3(x, y, 0);

        // 各フェーズでの処理
        switch (phase)
        {
            case 0:
                // スキル1の処理
                SnakeSpawn.SetActive(false);
                TimeBulletSpawn.SetActive(true);
                break;
            case 1:
                // 弾幕の処理
                TimeBulletSpawn.SetActive(false);
                BulletSpawn.SetActive(true);
                break;
            case 2:
                BulletSpawn.SetActive(false);
                SnakeSpawn.SetActive(true);
                // スキル2の処理
                break;
        }

        // フェーズ管理
        if (phase == 0 && elapsedTime >= skill1Duration)
        {
            // スキル1の処理を終えたら次のフェーズへ
            phase = 1;
            elapsedTime = 0.0f;
        }
        else if (phase == 1 && elapsedTime >= danmakuDuration)
        {
            // 弾幕の処理を終えたら次のフェーズへ
            phase = 2;
            elapsedTime = 0.0f;
        }
        else if (phase == 2 && elapsedTime >= skill2Duration)
        {
            // スキル2の処理を終えたら最初のフェーズへ戻る
            phase = 0;
            elapsedTime = 0.0f;
        }


    }
}
