using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSpawn : MonoBehaviour
{
    [SerializeField]
    GameObject waveBuuletPrefab;
    [SerializeField]
    float fireRate = 0.2f;
    [SerializeField]
    Transform spawnPoint;
    private float nextFireTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartWaveBulletPattern());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator StartWaveBulletPattern()
    {
        while (true)
        {
            GameObject waveBullet = Instantiate(waveBuuletPrefab,spawnPoint.position,Quaternion.identity);
            WaveBullet waveScript = waveBullet.GetComponent<WaveBullet>();
            waveScript.startPosition = spawnPoint.position;

            yield return new WaitForSeconds(1.0f / fireRate);
        }
    }
}
