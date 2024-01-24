using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class DreamBullet : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(player.BulletSeverFlag == true)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerLight"))
            spriteRenderer.color = new Color(255,255,255,255);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerLight"))
            spriteRenderer.color = new Color(255, 255, 255, 0);
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
