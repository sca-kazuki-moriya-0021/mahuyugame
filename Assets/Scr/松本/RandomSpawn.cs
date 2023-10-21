using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemyPrefabs;
    Transform leftPos;
    Transform rightPos;
    [SerializeField]
    private int enemyCount;

    private float minX,maxX,minY,maxY;
    // Start is called before the first frame update
    void Start()
    {
        minX = leftPos.position.x;
        maxX = rightPos.position.x;
        minY = leftPos.position.y;
        maxY = rightPos.position.x;

        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnEnemy()
    {
        for(int i = 0; i < 5; i++)
        {
            Vector2 position = new Vector2(Random.Range(minX,maxX),Random.Range(minY,maxY));
            GameObject enemy = enemyPrefabs[Random.Range(0,enemyPrefabs.Length)];
            Instantiate(enemy,position,Quaternion.identity,transform);

            yield return new WaitForSeconds(3.0f);
        }
    }
}
