using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowFairyMove : MonoBehaviour
{
    private float speed = 1f;
    [SerializeField]
    private GameObject movePos;
    private GameObject[] movePosChild = new GameObject[]{null,null,null,null};
    private int count = 0;
    private float time =0;

    private Rigidbody2D rigidbody;
    private bool moveColFlag;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        for(int i = 0; i < movePosChild.Length; i++)
        {
            movePosChild[i] = movePos.transform.GetChild(i).gameObject;
        }

        transform.position = movePosChild[0].transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {

        //ˆÚ“®ŠÇ—
        switch (count%4)
        {
            case 0 when moveColFlag == false:
                StartCoroutine(MovePosition(0,1));
            break;
            case 1 when moveColFlag == false:
                StartCoroutine(MovePosition(1,2));
            break;
            case 2 when moveColFlag == false:
                StartCoroutine(MovePosition(2,3));
            break;
            case 3 when moveColFlag == false:
                StartCoroutine(MovePosition(3,0));
            break;
        }
    }

    //ˆÚ“®’²®
    private IEnumerator MovePosition(int a ,int b)
    {
        moveColFlag = true;
        float dir = Mathf.Abs(Vector3.Distance(movePosChild[a].transform.position,movePosChild[b].transform.position));
        Debug.Log(dir);
        //float pos = (Time.time * speed) / dir;
        while (movePosChild[b].transform.position != transform.position)
        {
            time += Time.deltaTime;
            float pos = (time * speed) / dir;
            transform.position = Vector3.Lerp(movePosChild[a].transform.position, movePosChild[b].transform.position, pos);
            yield return null;
        }
        count++;
        time =0;
        moveColFlag = false;
        StopCoroutine(MovePosition(a,b));
    }
}
