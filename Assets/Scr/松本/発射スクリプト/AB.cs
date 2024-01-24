using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AB : MonoBehaviour
{
    public GameObject bulletPrefab; // 弾のプレハブ
    public Transform centerPoint;   // 中心点
    public int numberOfBullet;      // 弾の数
    public float radius;            // 半径
    public float rotationSpeed;     // 回転速度
    public float duration;          // 弾生成にかかる時間
    private bool isMove = false;
    private List<GameObject> bullets = new List<GameObject>();
    private NewHoming newHoming;

    void Start()
    {
        newHoming = FindObjectOfType<NewHoming>();
        CounterAttack(bulletPrefab, centerPoint, numberOfBullet, radius, rotationSpeed, duration);
    }

    public void CounterAttack(GameObject bulletPrefab, Transform centerPoint, int numberOfBullet, float radius, float rotationSpeed, float duration)
    {
        isMove = false;
        StartCoroutine(SpawnBulletsOverTime(bulletPrefab, centerPoint, numberOfBullet, radius, rotationSpeed, duration));
    }

    IEnumerator SpawnBulletsOverTime(GameObject bulletPrefab, Transform centerPoint, int numberOfBullet, float radius, float rotationSpeed, float duration)
    {
        for (int i = 0; i < numberOfBullet; i++)
        {
            float angle = i * (360f / numberOfBullet);
            Vector3 spawnPosition = GetCirclePosition(centerPoint.position, angle, radius);
            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            bullet.transform.parent = centerPoint;
            bullets.Add(bullet);

            yield return new WaitForSeconds(duration / numberOfBullet);
        }
        isMove = true;
        foreach (var bullet in bullets)
        {
            if (bullet != null)
            {
                bullet.GetComponent<NewHoming>().IsMove = isMove;
            }
        }
        //newHoming.IsMove = true;
    }

    Vector3 GetCirclePosition(Vector3 center, float angle, float radius)
    {
        float x = center.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = center.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        return new Vector3(x, y, center.z);
    }
}
