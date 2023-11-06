using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemyPrefabs;

    private float minX,maxX,minY,maxY;
    // Start is called before the first frame update
    void Start()
    {
        GameObject LeftPos = GameObject.Find("LeftPos");
        GameObject RightPos = GameObject.Find("RightPos");
        minX = LeftPos.transform.position.x;
        maxX = RightPos.transform.position.x;
        minY = LeftPos.transform.position.y;
        maxY = RightPos.transform.position.y;

        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnEnemy()
    {
        for(int i = 0; i < 2; i++)
        {
            Vector2 position = new Vector2(Random.Range(minX,maxX),Random.Range(minY,maxY));
            GameObject enemy = enemyPrefabs[Random.Range(0,enemyPrefabs.Length)];
            Instantiate(enemy,position,Quaternion.identity,transform);

            yield return new WaitForSeconds(3.0f);
        }
    }
}
