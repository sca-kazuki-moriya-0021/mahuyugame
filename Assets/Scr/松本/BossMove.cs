using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    [SerializeField] private float speed = 2.0f; // �ړ����x
    [SerializeField] private float amplitudeX; // X���̐U��
    [SerializeField] private float amplitudeY; // Y���̐U��
    [SerializeField] private float stopTime;
    [SerializeField] private Transform centerObject;
    private float stopCountTime;
    private bool bossAttack1 = false;
    private bool bossAttack2 = false;

    private float angle;
    
    //private Vector3 startPos;
    private bool isMoving = true;

    //�X�N���v�g�擾
    private Player player;
    private BossCollder bossCollder;

    public bool BossAttack1
    {
        get { return this.bossAttack1; }
        set { this.bossAttack1 = value; }
    }
    public bool BossAttack2
    {
        get { return this.bossAttack2; }
        set { this.bossAttack2 = value; }
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
        bossCollder = FindObjectOfType<BossCollder>();
    }

    void Update()
    {
        if(bossAttack1 == false && isMoving == true && bossCollder.BossDeathFlag == false)
        {
            Move();
        }
    }

    private void Move()
    {
        if (bossCollder.BossDebuffFlag == true)
            transform.position = NewPosition(0.1f,0.5f,1f);
        else
            transform.position = NewPosition(1f, 1f, 1f);
    }

    //�ړ��̑����ƍ��W�v�Z
    private Vector3 NewPosition(float angleSpeed,float speed,float moveSpeed)
    {
        angle += Time.deltaTime * speed * angleSpeed;
        float x = Mathf.Sin(angle * 2) * amplitudeX * speed;
        float y = Mathf.Sin(angle) * amplitudeY * speed;
        // Z���̈ʒu�͌Œ�i2D��ԂɌŒ�j
        Vector3 offset = new Vector3(x, y, 0);
        Vector3 newPosition = centerObject.position + offset;
        //���W�X�V
        Vector3 pos = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * moveSpeed);
        return pos;
    }
}
