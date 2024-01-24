using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowNormalBullet : MonoBehaviour
{
    [SerializeField]
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player.BulletSeverFlag == true)
            Destroy(this.gameObject);
    }


    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Destroy(this.gameObject);
    }
}
