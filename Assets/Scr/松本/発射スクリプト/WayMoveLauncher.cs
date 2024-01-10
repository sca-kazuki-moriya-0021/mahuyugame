using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayMoveLauncher : MonoBehaviour
{
    [SerializeField]GameObject BigWay;
    [SerializeField]GameObject BulletAll;
    [SerializeField]float bulletSpeed;
    [SerializeField]float subBulletSpeed;
    [SerializeField]int numberOfBullets;
    [SerializeField]float spreadAngle;
    [SerializeField]float timeShots;
    [SerializeField]float subTimeShots;
    [SerializeField]float randomMoveTime;

    private Transform play;
    private Player player;
    private List<Rigidbody2D> bulletList = new List<Rigidbody2D>();
    // Start is called before the first frame update
    void Start()
    {
        play = GameObject.FindGameObjectWithTag("Player").transform;
        player = FindObjectOfType<Player>();
        StartCoroutine(ShootBullets());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ShootBullets()
    {
        while (true)
        {
            if(player.BulletSeverFlag == false)
            {
                //bulletList.Clear();
                ShootNWayBullets();
                
                //yield return new WaitForSeconds(randomMoveTime);  
            }
            yield return new WaitForSeconds(timeShots);
        }
    }

    private void ShootNWayBullets()
    {
        float angleStep = spreadAngle / (numberOfBullets - 1);
        float initialAngle = transform.eulerAngles.z - (spreadAngle / 2);
        Vector2 playerPosition = play.position;

        for(int i = 0;i < numberOfBullets; i++)
        {
            GameObject bullet = Instantiate(BigWay, transform.position,Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            float bulletAngle = initialAngle + (i * angleStep);
            Vector2 directionPlayer = (playerPosition - (Vector2)transform.position).normalized;
            float anglePlayer = Mathf.Atan2(directionPlayer.y,directionPlayer.x) * Mathf.Rad2Deg;

            bullet.transform.rotation = Quaternion.Euler(0,0,bulletAngle + anglePlayer);
            rb.velocity = bullet.transform.up * bulletSpeed;

            StartCoroutine(ShootSubBullets(bullet.transform));
        }
    }

    private IEnumerator ShootSubBullets(Transform parentBullet)
    {
        yield return new WaitForSeconds(subTimeShots);

        while(parentBullet != null)
        {
            GameObject subBullet = Instantiate(BulletAll, parentBullet.position,Quaternion.identity);
            Rigidbody2D bulletRigidbody = subBullet.GetComponent<Rigidbody2D>();
            yield return new WaitForSeconds(subTimeShots);
        }
    }
}
