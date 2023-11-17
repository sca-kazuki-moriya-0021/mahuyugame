using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBullet : MonoBehaviour
{
    [SerializeField]
    float waveFrequency;
    [SerializeField]
    float waveAmplitude;
    [SerializeField]
    float bulletSpeed;
    public Vector2 startPosition;

    private float startTime;

    void Start()
    {
        startTime = Time.deltaTime;
    }

    void Update()
    {
        float timePassed = Time.deltaTime - startTime;
        float verticalMovement = Mathf.Sin(timePassed * waveFrequency) * waveAmplitude;

        float newX = startPosition.x - timePassed * bulletSpeed;

        float newY = startPosition.y + verticalMovement;

        transform.position= new Vector2(newX,newY);
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
