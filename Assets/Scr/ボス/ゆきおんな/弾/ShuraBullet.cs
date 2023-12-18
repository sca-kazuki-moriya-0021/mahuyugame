using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuraBullet : MonoBehaviour
{
    float speed = 1f;

    [SerializeField]
    private Rigidbody2D rigidbody2D;

    private Vector3 pos;
    private Vector3 bossPos;

    [SerializeField]
    private GameObject boss;

    private bool bossLineFlag;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        if(pos.y > 0)
            rigidbody2D.velocity = speed * Vector2.up;
        else
            rigidbody2D.velocity = speed * -Vector2.up;

        bossPos = boss.transform.position;
        
       
    }

    // Update is called once per frame
    void Update()
    {
        var i = Mathf.Abs(bossPos.y - transform.position.y);
        if (i < transform.position.y + 1f && bossLineFlag)
        {
            StartCoroutine(Move());
        }
    }


    private IEnumerator Move()
    {
        rigidbody2D.velocity = Vector2.zero;
        bossLineFlag = true;
        while (true)
        {
            var i = Random.Range(-2.0f, 2.1f);
            var a = new Vector2(transform.position.x + i,transform.position.y + i);
            transform.position = Vector2.MoveTowards(transform.position,a,speed * Time.deltaTime);
        }
        
    }
}
