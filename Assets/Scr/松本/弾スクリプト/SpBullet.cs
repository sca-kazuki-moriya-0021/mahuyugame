using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpBullet : MonoBehaviour
{

    public float bulletSpeed = 5f;  // ’e‚Ì‘¬‚³
    public float angularSpeed = 2f;  // Šp‘¬“x
    public float radiusIncrement = 0.1f;  // —†ù‚Ì”¼Œa‚Ì‘‰Á—Ê

    private float currentRadius = 0f;
    private Rigidbody2D rb2d;
    public int count = 0;
    public int insCount = 0;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        if (rb2d == null)
        {
            rb2d = gameObject.AddComponent<Rigidbody2D>();
        }
        rb2d.isKinematic = false;
    }

    void Update()
    {
        if(count % 2 == 0)
        {
            AngleSwitch(1);
        }
        else
        {
            AngleSwitch(-1);
        }

        if(insCount > 4)
        {
            //’e‚Ì¶¬ŠJn
        }
    }

    private void AngleSwitch(int i)
    {
        float angle = Time.time * angularSpeed * i;

        currentRadius += radiusIncrement * Time.deltaTime;

        float x = currentRadius * Mathf.Cos(angle);
        float y = currentRadius * Mathf.Sin(angle);

        rb2d.velocity = new Vector3(x, y, 0);
    }
}
