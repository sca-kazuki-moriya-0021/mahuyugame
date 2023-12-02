using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpBullet : MonoBehaviour
{

    [SerializeField] float bulletSpeed = 5f;  // ’e‚Ì‘¬‚³
    [SerializeField] float angularSpeed = 2f;  // Šp‘¬“x
    [SerializeField] float radiusIncrement = 0.1f;  // —†ù‚Ì”¼Œa‚Ì‘‰Á—Ê
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float timeBetweenShots = 0.1f;

    private float timer;
    private float currentRadius = 0f;
    private Rigidbody2D rb2d;
    private Player player;

    public int count = 0;
    public int insCount = 0;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        if (rb2d == null)
        {
            rb2d = gameObject.AddComponent<Rigidbody2D>();
        }
        rb2d.isKinematic = false;
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(count % 2 == 0)
        {
            AngleSwitch(1);
        }
        else
        {
            AngleSwitch(-1);
        }
    }

    private void AngleSwitch(int i)
    {
        float angle = Time.time * angularSpeed * i;

        currentRadius += radiusIncrement * Time.deltaTime;

        float x = currentRadius * Mathf.Cos(angle);
        float y = currentRadius * Mathf.Sin(angle);

        Vector3 bulletDirection = new Vector3(x,y,0).normalized;
        Vector3 perpendicularDirection = new Vector3(-bulletDirection.y,bulletDirection.x,0);
        rb2d.velocity = new Vector3(x, y, 0);

        if (insCount > 4)
        {
            if (timer >= timeBetweenShots)
            {
                //’e‚Ì¶¬ŠJn
                GenerateBullet(perpendicularDirection); 
                timer = 0f;
            }
        }
    }

    private void GenerateBullet(Vector3 direction)
    {
        // ’e‚ğ¶¬‚µ‚Ä“KØ‚ÈˆÊ’u‚É”z’u
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }
}
