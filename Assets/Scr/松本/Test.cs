using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Test : MonoBehaviour, ITimeControl
{

    // ���x���Ă΂��
    public void SetTime(double time)
    {
        //transform.localRotation = Quaternion.AngleAxis(((float)time) * speed, axis);
    }

    // �N���b�v�J�n���ɌĂ΂��
    public void OnControlTimeStart()
    {
        Debug.Log("a");
    }

    // �N���b�v���甲���鎞�ɌĂ΂��
    public void OnControlTimeStop()
    {
    }
}