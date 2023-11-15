using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangBullet : MonoBehaviour
{
    //äpìxåvéZÇ∆ë¨ìx
    private float velocity;
    private Vector3 angle;

   


    private GameObject player;

    //èÛë‘ëJà⁄
    private STATE state;

    private float time;

    private Rigidbody2D rb2d;

    public float Velocity { get => velocity; set => velocity = value; }
    public Vector3 Angle { get => angle; set => angle = value; }

    enum STATE
    {
        Start,
        Middle,
        Final,
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
        if(state == STATE.Start)
        {
            transform.Translate(transform.position.x * angle.x * Time.deltaTime,transform.position.y * angle.y * Time.deltaTime,0);
           
        }

        if(state == STATE.Middle)
        {
            Vector2 now = new Vector2(transform.position.x,transform.position.y);


        }

        if(state == STATE.Final)
        {
            Vector2 a = rb2d.velocity;
            Vector2 b = transform.position - player.transform.position;
            a = b * velocity *1.5f;
            rb2d.velocity = a;
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
