using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShuraBullet : MonoBehaviour
{
    float speed = 3f;
    float time = 0f;

    [SerializeField]
    private Rigidbody2D rigidbody2D;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private Vector3 pos;

    private SnowFairyBulletCon snowFairyBulletCon;

    private int randomCount = 0;

    [SerializeField]
    private GameObject player;

    private Player playerCon;

    private bool centerFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        if(pos.y > 0)
            rigidbody2D.velocity = speed * -Vector2.up;
        else
            rigidbody2D.velocity = speed * Vector2.up;

        snowFairyBulletCon = FindObjectOfType<SnowFairyBulletCon>();
        playerCon = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //�����Ƃ߂�p
        //if (centerFlag == true)
           //rigidbody2D.velocity = Vector2.zero;

        //�ǔ��ɓ���if��
        //if(snowFairyBulletCon.PPosMoveFlag == true && centerFlag == true)
            //TrackingMove();

        

        if(playerCon.BulletSeverFlag == true)
            Destroy(this.gameObject);
    }

    //�~�܂�����ǔ�����
    /*private void TrackingMove()
    {
        centerFlag = false;
        var p = player.transform.position;
        Debug.Log(p);
        var dir = p - transform.position;

        rigidbody2D.velocity = dir *speed *0.5f;
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerLight"))
            spriteRenderer.color = new Color(255, 255, 255, 255);
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

    //�{�X��Y���Ɠ���Y���̃I�u�W�F�N�g�ɐG�ꂽ��
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("SkillBulletOutLine"))
            centerFlag = true;
    }*/

}
