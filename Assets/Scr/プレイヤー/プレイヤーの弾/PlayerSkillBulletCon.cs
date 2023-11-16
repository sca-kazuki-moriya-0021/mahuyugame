using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillBulletCon : MonoBehaviour
{

    private float time = 0;

    private Player player;
    
    private GameObject boss;

    // Start is called before the first frame update
    void Start()
    {
       boss = GameObject.FindGameObjectWithTag("Boss");
      
    }

    // Update is called once per frame
    void Update()
    {
        if(boss != null)
        {
            Vector3 pos = boss.transform.position - transform.position;
            var dir = pos.normalized;
            //íeÇÃêiÇﬁäÑçáÇTime.deltaTimeÇ≈åàÇﬂÇÈ
            transform.Translate(dir * Time.unscaledDeltaTime * 10.0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
