using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class circleLuncher : MonoBehaviour
{
    [SerializeField]
    private GameObject Bullet;
    [SerializeField, Header("èâë¨ìx")]
    private float _Velocity_0;
    [SerializeField, Header("äpìx")]
    private float Degree;
    [SerializeField, Header("Wayêî")]
    private float Angle_Split;
    [SerializeField, Header("ë“Çøéûä‘")]
    private float waitTime;
    float _theta;
    float PI = Mathf.PI;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Luncher(10, 1));
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Vector2 pos = transform.position;
        pos.x += 0.1f * Input.GetAxisRaw("Horizontal");
        transform.position = pos;*/
    }
    public void OnClick()
    {
        
    }
    private IEnumerator Luncher(float degree, float angle_Split)
    {
        float elapsedTime = 0f;
        while (elapsedTime < 0.5f)
        {
            elapsedTime += Time.deltaTime;
            Debug.Log(elapsedTime);
            for (int i = 0; i <= Angle_Split - 1; i++)
            {
                float AngleRange = PI * (Degree / 180);
                //Debug.Log(AngleRange);

                if (Angle_Split > 1) _theta =
                            AngleRange / (Angle_Split - 1) * i + 0.5f * (PI - AngleRange);
                else _theta = 0.5f * PI;

                GameObject Bullet_obj = (GameObject)Instantiate(Bullet, transform.position, transform.rotation);
                BulletSc bullet_cs = Bullet_obj.GetComponent<BulletSc>();
                bullet_cs.theta = _theta;
                bullet_cs.Velocity_0 = _Velocity_0;

            }
            if (Degree != 360)
            {
                float degreesum = degree + Degree;
                Degree = degreesum;
            }
            //Debug.Log(Degree);
            if (Angle_Split != 40)
            {
                float angle_Splitsum = angle_Split + Angle_Split;
                Angle_Split = angle_Splitsum;
            }
            //Debug.Log(Angle_Split);
            yield return new WaitForSeconds(waitTime);
            //Debug.Log("a");
        }
    }
}
