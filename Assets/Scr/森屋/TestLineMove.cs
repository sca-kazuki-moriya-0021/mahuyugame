using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TestLineMove : MonoBehaviour
{

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float speed = 1f;

    private int currentIndex = 0;
    private int lineIndex = 0;
    private bool moveColFlag = false;

    private void Start()
    {
        transform.position = this.lineRenderer.GetPosition(0);
        lineIndex = lineRenderer.positionCount;
        Debug.Log(lineIndex);
    }


    private void Update()
    {
        //ƒ‰ƒCƒ“ã‚ÌˆÚ“®
       if(lineIndex- 1 > currentIndex && moveColFlag == false)
       {
            Debug.Log("‚Í‚¢‚Á‚½");
            StartCoroutine(MovePos(currentIndex , currentIndex + 1));
       }

       if(currentIndex == 3)
       {
            Destroy(this.gameObject);
       }
       
    }


    private IEnumerator MovePos(int i ,int x)
    {

        float time = 0;
        moveColFlag = true;
        float dir = Mathf.Abs(Vector3.Distance(lineRenderer.GetPosition(x),lineRenderer.GetPosition(i)));


        while(lineRenderer.GetPosition(x) != transform.position)
        {
            time += Time.deltaTime;
            float pos = (time * speed * 10) / dir;
            transform.position = Vector3.Lerp(lineRenderer.GetPosition(i), lineRenderer.GetPosition(x), pos);
            yield return null;
        }
        currentIndex++;
        moveColFlag = false;
        
        StopCoroutine(MovePos(i,x));
    }
}
