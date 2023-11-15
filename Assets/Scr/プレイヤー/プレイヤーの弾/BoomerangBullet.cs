using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangBullet : MonoBehaviour
{
    //äpìxåvéZÇ∆ë¨ìx
    private float velocity;
    private Vector3 angle;
    private int number;
    private Vector3 endPosition;

    private Vector3 middlePostion;

    private GameObject player;

    //èÛë‘ëJà⁄
    private STATE state;

    private float time;

    private Rigidbody2D rb2d;

    public float Velocity { get => velocity; set => velocity = value; }
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
        player = GameObject.Find("Player").transform.GetChild(1).gameObject;

        state = STATE.Start;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime; 
        var p = player.transform.position;
        if(state == STATE.Start)
        {
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
            var a = Vector3.Lerp(player.transform.position,middlePostion,time);
            var b = Vector3.Lerp(middlePostion,endPosition,time);
            this.transform.position = Vector3.Lerp(a,b,time);

            if(transform.position == endPosition)
            {
                state = STATE.End;
                time = 0;
            }
        }

        if(state == STATE.End)
        {
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

            var a = Vector3.Lerp(endPosition, middlePostion, time);
            var b = Vector3.Lerp(middlePostion, p, time);
            this.transform.position = Vector3.Lerp(a, b,time);

            if(player.transform.position == transform.position)
            {
                Debug.Log("àÍâû");
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == player)
        {
            Debug.Log("è¡Ç¶ÇÈ");
            Destroy(this.gameObject);
        }
    }
}
