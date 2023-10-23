using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Test : MonoBehaviour, ITimeControl
{

    // 何度も呼ばれる
    public void SetTime(double time)
    {
        //transform.localRotation = Quaternion.AngleAxis(((float)time) * speed, axis);
    }

    // クリップ開始時に呼ばれる
    public void OnControlTimeStart()
    {
        Debug.Log("a");
    }

    // クリップから抜ける時に呼ばれる
    public void OnControlTimeStop()
    {
    }
}