using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmaLauncher : MonoBehaviour
{
    [SerializeField]private GameObject AmaBullet;
    [SerializeField]private int numberOfAma;
    [SerializeField]private float spawnInterval;
    [SerializeField]private float spawnHeightMin;
    [SerializeField]private float spawnHeightMax;

    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= spawnInterval)
        {
            for(int i = 0;i < numberOfAma; i++)
            {
                SpawnAma();
            }

            timer = 0f;
        }
    }

    private void SpawnAma()
    {
        float spawnHeight = Random.Range(spawnHeightMin, spawnHeightMax);
        Vector3 spawnPosition = new Vector3(transform.position.x, spawnHeight,transform.position.z);

        GameObject ama = Instantiate(AmaBullet,spawnPosition,Quaternion.identity);
        ama.transform.parent = transform;
    }
}
