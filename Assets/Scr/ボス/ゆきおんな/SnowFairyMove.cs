using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowFairyMove : MonoBehaviour
{
    private float time = 0f;
    [SerializeField]
    private GameObject movePos;
    private GameObject[] movePosChild = new GameObject[]{null,null,null,null};
    private int count = 0;

    private Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        for(int i = 0; i < movePosChild.Length; i++)
        movePosChild[i] = movePos.transform.GetChild(i).gameObject;

        for (int i = 0; i < movePosChild.Length; i++)
          Debug.Log( movePosChild[i].transform.position);

        transform.position = movePosChild[0].transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        time += Time.deltaTime * 0.0001f;
        if (time < 0.5f)
        {
            var a = Vector3.Lerp(movePosChild[0].transform.position,movePosChild[1].transform.position,time);
            var b = Vector3.Lerp(movePosChild[1].transform.position,movePosChild[2].transform.position,time);
            rigidbody.velocity = Vector3.Lerp(a,b,time);
        }
        else if(time >0.5f && time <1.0f)
        {
            var a = Vector3.Lerp(movePosChild[2].transform.position, movePosChild[3].transform.position, time);
            var b = Vector3.Lerp(movePosChild[3].transform.position, movePosChild[0].transform.position, time);
            rigidbody.velocity = Vector3.Lerp(a,b,time);
        }
        else if(time >= 1.0f)
        {
            time = 0;
        }
    }
}
