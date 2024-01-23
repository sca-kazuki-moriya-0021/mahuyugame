using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectWallDown : MonoBehaviour
{
    [SerializeField]
    string bulletTag1 = "ReflectBullet";
    [SerializeField]
    string bulletTag2 = "ApolloReflector";
    [SerializeField]
    string bulletTag3 = "Proliferation";
    [SerializeField]
    GameObject Bullets;
    private NurarihyonPushBulletCon nurarihyonPushBulletCon;
    // Start is called before the first frame update
    void Start()
    {
        nurarihyonPushBulletCon = FindObjectOfType<NurarihyonPushBulletCon>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(bulletTag1))
        {
            ReflectBullet(collision.gameObject);
        }

        if (collision.CompareTag(bulletTag2))
        {
            Vector3 spawnPosition = collision.transform.position;
            ApolloReflector(collision.gameObject);
        }

        if (collision.CompareTag(bulletTag3))
        {
            Vector3 spawnPosition = collision.transform.position;
            BulletSpawn(spawnPosition);
            Destroy(collision.gameObject);
        }
    }

    private void ReflectBullet(GameObject bullet)
    {
        Debug.Log("a");
        var rb = bullet.GetComponent<Rigidbody2D>();
        if (rb == null) return;

        var inDirection = rb.velocity;
        var inNormal = transform.up;
        var result = Vector2.Reflect(inDirection, inNormal);

        rb.velocity = result * 0.9f;

        BulletCon ballCountScript = bullet.GetComponent<BulletCon>();
        if (ballCountScript != null)
        {
            ballCountScript.IncrementCount();


            if (ballCountScript.GetCount() >= 5)
            {
                Destroy(bullet);
            }
        }
    }

    private void ApolloReflector(GameObject bullet)
    {
        BulletCon ballCountScript = bullet.GetComponent<BulletCon>();
        if (ballCountScript != null)
        {
            ballCountScript.IncrementCount();
            if (ballCountScript.GetCount() >= 5)
            {
                Destroy(bullet);
            }
        }
        var rb = bullet.GetComponent<Rigidbody2D>();
        if (rb == null) return;

        var inDirection = rb.velocity;
        var inNormal = transform.up;
        var result = Vector2.Reflect(inDirection, inNormal);
        if ((ballCountScript.GetCount() > 2))
        {
            rb.velocity = result * 1.5f;
        }
        else
        {
            rb.velocity = result;
        }
    }

    private void BulletSpawn(Vector3 position)
    {
        float startAngle = -180f; // îºâ~Ç…ïœçX

        for (int i = 0; i < 13; i++)
        {
            float angle = startAngle + i * (180f / (13 - 1)); // îºâ~Ç…ïœçX
            Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.left;
            Rigidbody2D bulletRigidbody = Instantiate(Bullets, position, Quaternion.identity).GetComponent<Rigidbody2D>();
            bulletRigidbody.velocity = direction * 2f;
        }
    }
}
