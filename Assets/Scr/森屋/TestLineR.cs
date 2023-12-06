using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLineR : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    private Vector3[] linePos;

    private List<Vector2> linePoints = new List<Vector2>();

    private void OnEnable()
    {
        foreach (Vector3 i in linePos)
        {
            linePoints.Add(i);
        }
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.SetPositions(linePos);
        lineRenderer.startColor = Color.green;
        var lineObj = lineRenderer.gameObject.GetComponent<EdgeCollider2D>();
        lineObj.transform.position = transform.TransformPoint(lineObj.transform.position);
        lineObj.SetPoints(linePoints);
        lineObj.isTrigger = true;

        lineRenderer.gameObject.layer = 9;
       
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
