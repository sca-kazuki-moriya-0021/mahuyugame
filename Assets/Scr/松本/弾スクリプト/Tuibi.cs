using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuibi : MonoBehaviour
{
    [SerializeField] float limit; // 最大加速度
    Vector3 velocity; // 速度
    Vector3 position; // 位置
    Transform target; // ターゲット
    [SerializeField] float maxSpeed; // 最大速度
    [SerializeField] float slowingDistance; // 到達減速を始める距離
    [SerializeField] float timeToTarget; // 目標に到達する時間

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        // ミサイルの初期位置を設定
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 desiredVelocity = (target.position - position);
        float distance = desiredVelocity.magnitude;

        if (distance < slowingDistance)
        {
            // 到達減速を開始
            float rampedSpeed = maxSpeed * (distance / slowingDistance);
            float clippedSpeed = Mathf.Min(rampedSpeed, maxSpeed);
            desiredVelocity = desiredVelocity.normalized * clippedSpeed;
        }
        else
        {
            desiredVelocity = desiredVelocity.normalized * maxSpeed;
        }

        Vector3 acceleration = (desiredVelocity - velocity) / timeToTarget;

        // 速度制限
        if (velocity.magnitude > limit)
        {
            velocity = velocity.normalized * limit;
        }

        // 速度と位置の更新
        velocity += acceleration * Time.deltaTime;
        position += velocity * Time.deltaTime;
        transform.position = position;
    }
}
