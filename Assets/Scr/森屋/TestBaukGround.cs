using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBaukGround : MonoBehaviour
{

    [SerializeField] private float ResetPosition;
    private float MoveSpeed = -0.05f;
    private Vector3 StartPosition;
    void Awake()
    {
        StartPosition = transform.position;
    }
    private void FixedUpdate()
    {
        transform.Translate(MoveSpeed, 0, 0, Space.World);
        if (transform.position.x < ResetPosition)
            transform.position = StartPosition;
    }
}
