using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danmaku : MonoBehaviour
{
    private Transform cenPos; // DanmakuÇ≤Ç∆Ç…àŸÇ»ÇÈcenPosÇï€éù

    public float radius = 2f;
    public float angularSpeed = 2f;

    private float angle = 0f;

    public void SetCenPos(Transform newCenPos)
    {
        cenPos = newCenPos;
    }

    void Update()
    {
        MoveDanmaku();
    }

    void MoveDanmaku()
    {
        if (cenPos != null)
        {
            float x = cenPos.position.x + Mathf.Cos(angle) * radius;
            float y = cenPos.position.y + Mathf.Sin(angle) * radius;

            transform.position = new Vector3(x, y, 0f);

            angle += angularSpeed * Time.deltaTime;
        }
    }
}
