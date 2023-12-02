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

    private Rigidbody2D rigidbody;
    private Coroutine startCol;

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
        Debug.Log(transform.position);
        //ˆÚ“®ŠÇ—
        switch (count%4)
        {
            case 0 when startCol == null:
                startCol = StartCoroutine(MovePosition(1));
            break;
            case 1 when startCol == null:
                startCol = StartCoroutine(MovePosition(2));
            break;
            case 2 when startCol == null:
                startCol = StartCoroutine(MovePosition(3));
            break;
            case 3 when startCol == null:
                startCol = StartCoroutine(MovePosition(0));
            break;
        }
    }

    //ˆÚ“®’²®
    private IEnumerator MovePosition(int i)
    {
        Debug.Log("haita");
        float dir = Mathf.Abs(Vector3.Distance(transform.position, movePosChild[i].transform.position));
        //float pos = (Time.time * speed) / dir;
        while (movePosChild[i].transform.position != transform.position)
        {
            float pos = (Time.time * speed) / dir;
            rigidbody.velocity = Vector3.Lerp(transform.position, movePosChild[i].transform.position, pos);
            yield return null;
        }
        count++;
        startCol = null;
        StopCoroutine(MovePosition(i));
    }
}
