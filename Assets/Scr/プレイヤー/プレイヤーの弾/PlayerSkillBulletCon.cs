using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillBulletCon : MonoBehaviour
{

    private float time = 0;

    private Vector2 middlePos;

    private GameObject player;
    
    private GameObject boss;

    private bool flag = true;

    // Start is called before the first frame update
    void Start()
    {
       boss = GameObject.FindGameObjectWithTag("Boss");
       player = GameObject.FindGameObjectWithTag("Player");
       middlePos = (boss.transform.position - player.transform.position) / 2;
       middlePos.y += 15f;
    }

    // Update is called once per frame
    void Update()
    {
       if(flag == true)
       {
            flag = false;
            StartCoroutine(RotationSet());
       }

       time += Time.unscaledDeltaTime * 0.5f;
       var a = Vector3.Lerp(player.transform.position, middlePos, time);
       var b = Vector3.Lerp(middlePos,boss.transform.position,time);
       this.transform.position = Vector3.Lerp(a,b,time);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boss"))
            Destroy(this.gameObject);
    }

    private IEnumerator RotationSet()
    {
        while (true)
        {
            var t = transform.position;
            yield return null;
            var t2 = transform.position;

            Vector2 m = t -t2;
            transform.rotation = Quaternion.FromToRotation(Vector2.left,m);
        }
       

    }
}
