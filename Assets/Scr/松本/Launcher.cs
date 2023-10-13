using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField,Header("’e‚ÌƒvƒŒƒnƒu")] private GameObject bulletPrefab;
    [SerializeField,Header("’e‚Ì‘¬“x")] private float bulletSpeed;
    [SerializeField,Header("Å‰‚Ì’e‚Ì”")] private int numberOfBullets;
    [SerializeField,Header("Å‰‚Ì•úËó‚ÌŠp“x")] private float spreadAngle;
    [SerializeField,Header("”­ËŠÔŠu")] private float bulletSpacing;
    [SerializeField,Header("ˆê‰ñ‚Ì’e‚Ì‘‰Á—Ê")] private int bulletAmount;
    [SerializeField,Header("’e‚ğ‘‚â‚·ŠÔ")] private float createBullet;
    [SerializeField,Header("Šp“x‚ğ‘‚â‚·ŠÔ")] private float timeAngle;
    [SerializeField,Header("‘‰ÁŠp“x")] private float yimespreadAngle;
    [SerializeField,Header("Å‘å’e”")]private int MaxBullet;

    private float BulletsTime;//’eŒo‰ßŠÔ
    private float elaTime;//Šp“xŒo‰ßŠÔ
    private int curBullet;//Œ»İ‚Ì’e
    private float curAngle;//Œ»İ‚ÌŠp“x

     void Start()
    {
        curBullet = numberOfBullets;
        curAngle = spreadAngle;
    }

    void Update()
    {
        BulletsTime += Time.deltaTime;
        elaTime += Time.deltaTime;
        if(curBullet < MaxBullet && BulletsTime >= createBullet)
        {
            curBullet += bulletAmount;
            curAngle += yimespreadAngle;
            BulletsTime = 0.0f;
        }
        if(elaTime >= timeAngle)
        {
            Debug.Log("a");
            curAngle += yimespreadAngle;
            elaTime = 0.0f;
        }
        ShootNWayBullets(curBullet,curAngle);
    }

    private void ShootNWayBullets(int curBullet, float curAngle)
    {
        float angleStep = curAngle / (curBullet - 1);
        float initialAngle = transform.eulerAngles.z - (curAngle / 2);
        bulletSpacing += Time.deltaTime;
        if (bulletSpacing > 2.0f)
        {
            for (int i = 0; i < curBullet; i++)
            {
                // ’e‚ğ¶¬‚µ‚ÄA‰ŠúˆÊ’u‚ğİ’è
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

                float bulletAngle = initialAngle + (i * angleStep);

                // ’e‚ÌŒü‚«‚ğƒJƒXƒ^ƒ}ƒCƒY‚·‚é‚½‚ß‚ÉA’e‚ÌŠp“x‚ğ•ÏX
                bullet.transform.rotation = Quaternion.Euler(0, 0, bulletAngle);

                Vector2 bulletDirection = Quaternion.Euler(0, 0, bulletAngle) * Vector2.up;
                rb.velocity = bulletDirection * bulletSpeed;
            }
            bulletSpacing = 0.0f;
        }
            
    }
}
