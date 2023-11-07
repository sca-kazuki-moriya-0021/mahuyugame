using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBullet : MonoBehaviour
{
    private Vector3 velocity;
    private Vector3 position;
    private Vector3 accleration;
    //ターゲットが存在しない時の目標位置
    private Vector3 randomPos;
    private Transform target;
    private GameObject searchNearObj;
    //着弾までの時間
    [SerializeField]
    private float period;

    private Player player;


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();

        period = period + Random.Range(-0.1f,0.1f);
        searchNearObj = FindEnemy();
        position = transform.position;
        if(searchNearObj != null)
        {
              
          target = searchNearObj.transform;
            
        }

        randomPos = new Vector3(4.0f,Random.Range(-1.5f,1.5f),0);
        velocity = new Vector3(Random.Range(-1.0f,-1.5f),Random.Range(-2.0f,2.0f),0);

    }

    // Update is called once per frame
    void Update()
    {
        accleration = Vector3.zero;
        if(searchNearObj != null)
        {
            Vector3 diff = target.position - position;
            accleration += (diff - velocity * period) / (period * period);
        }
        else
        {
            Vector3 diff = randomPos - position;
            accleration += (diff - velocity * period) * 2f /(period * period); 
        }

        period -= Time.deltaTime;
        if(period < 0f)
        {
            return;
        }

        velocity +=  accleration * Time.deltaTime;
        if (player.PBaffSkillFlag == true)
            position += velocity * Time.deltaTime * 2;
        else
            position += velocity * Time.deltaTime;
        transform.position = position;
    }

    public GameObject FindEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        foreach(GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            if(curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

}
