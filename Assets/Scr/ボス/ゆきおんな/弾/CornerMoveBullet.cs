using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerMoveBullet : MonoBehaviour
{
    //l‚ÂŠp‚ÌêŠ
    private GameObject cornerObject;

    private float speed = 10f;

    private GameObject[] cornerPosChild = new GameObject[] { null, null, null, null };

    private bool moveColFlag;

    private int moveCount = 0;

    public GameObject CornerObject
    {
        get { return this.cornerObject; }
        set { this.cornerObject = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < cornerPosChild.Length; i++)
        {
            cornerPosChild[i] = cornerObject.transform.GetChild(i).gameObject;
        }
        transform.position = cornerPosChild[0].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //ˆÚ“®ŠÇ—
        switch (moveCount % 4)
        {
            case 0 when moveColFlag == false:
                StartCoroutine(MovePosition(0, 1));
                break;
            case 1 when moveColFlag == false:
                StartCoroutine(MovePosition(1, 2));
                break;
            case 2 when moveColFlag == false:
                StartCoroutine(MovePosition(2, 3));
                break;
            case 3 when moveColFlag == false:
                StartCoroutine(MovePosition(3, 0));
                break;
        }
    }

    //ˆÚ“®’²®
    private IEnumerator MovePosition(int a, int b)
    {
        float time = 0;
        moveColFlag = true;
        float dir = Mathf.Abs(Vector3.Distance(cornerPosChild[a].transform.position, cornerPosChild[b].transform.position));
     
        //float pos = (Time.time * speed) / dir;
        while (cornerPosChild[b].transform.position != transform.position)
        {
            time += Time.deltaTime;
            float pos = (time * speed ) / dir;
            transform.position = Vector3.Lerp(cornerPosChild[a].transform.position, cornerPosChild[b].transform.position, pos);
            yield return null;
        }
        moveCount++;
        moveColFlag = false;
        StopCoroutine(MovePosition(a, b));
    }
}
