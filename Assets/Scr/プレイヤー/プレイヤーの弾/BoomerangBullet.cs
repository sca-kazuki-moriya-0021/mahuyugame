using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangBullet : MonoBehaviour
{
    //�p�x�v�Z�Ƒ��x
    private Vector3 angle;
    private int number;
    private Vector3 endPosition;

    //���Ԉʒu
    private Vector3 middlePostion;

    //�v���C���[�擾
    private GameObject player;

    //�����ʒu
    private Vector3 stratPos;

    //��ԑJ��
    private STATE state;

    //���Ԍv���p
    private float time;

    private Rigidbody2D rb2d;

    public Vector3 Angle { get => angle; set => angle = value; }
    public int Number {get => number; set => number = value; }
    public Vector3 EndPosition {get => endPosition; set => endPosition = value;}

    enum STATE
    {
        Start,
        End,
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        stratPos = transform.position;

        state = STATE.Start;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime; 
        var p = player.transform.position;
        //�s��
        if(state == STATE.Start)
        {
            //���ˈʒu���璆�S���W�����߂�
            switch (number)
            {
                case 0:
                    middlePostion = new Vector3(angle.x,angle.y+5f,0);
                    break;
                case 1:
                    break;
                case 2:
                    middlePostion = new Vector3(angle.x,angle.y-5f,0);
                 break;
            }
            //�ړ�
            var a = Vector3.Lerp(stratPos,middlePostion,time);
            var b = Vector3.Lerp(middlePostion,endPosition,time);
            this.transform.position = Vector3.Lerp(a,b,time);

            //�I�_�ʒu�܂ōs�����炢������time���Z�b�g��state�ύX
            if(transform.position == endPosition)
            {
                state = STATE.End;
                time = 0;
            }
        }
        //�A��
        if(state == STATE.End)
        {
            //�A���Ă���Ƃ��Ɏg�����Ԉʒu�w��
            switch (number)
            {
                case 0:
                    middlePostion = new Vector3(angle.x, angle.y - 5f, 0);
                    break;
                case 1:
                    break;
                case 2:
                    middlePostion = new Vector3(angle.x, angle.y + 5f, 0);
                    break;
            }

            //�ړ�
            var a = Vector3.Lerp(endPosition, middlePostion, time);
            var b = Vector3.Lerp(middlePostion, p, time);
            this.transform.position = Vector3.Lerp(a, b,time);

            //�v���C���[�̈ʒu�ɂȂ�����e������
            if(player.transform.position == transform.position)
                Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boss") && state == STATE.End)
            Destroy(this.gameObject);
    }
}
