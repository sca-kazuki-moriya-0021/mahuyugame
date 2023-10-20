using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleLauncher : MonoBehaviour
{
    public GameObject bulletPrefab; // ’e‚ÌƒvƒŒƒnƒu
    public int bulletCount; // ’e‚Ì‘”
    public float radius; // ’e‚Ì”­Ë”¼Œa
    public float rotationSpeed; // ‰QŠª‚«‚Ì‘¬‚³

    private void Start()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * (360f / bulletCount); // ’e‚ÌŠp“x‚ğŒvZ
            float x = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            float y = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            Vector3 spawnPosition = transform.position + new Vector3(x, y, 0);

            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            CircleBullet bulletController = bullet.GetComponent<CircleBullet>();
            bulletController.SetDirection(Quaternion.Euler(0, 0, angle) * Vector2.right);
            bulletController.SetRotationSpeed(rotationSpeed);
        }
    }
}
