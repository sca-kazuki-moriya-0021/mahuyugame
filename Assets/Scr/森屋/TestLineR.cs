using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLineR : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    private Vector3[] linePos;

    private void OnEnable()
    {

        lineRenderer.useWorldSpace = true;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.startColor = new Color(0, 0, 0, 0);
        lineRenderer.endColor = new Color(0, 0, 0, 0);

        lineRenderer.positionCount = linePos.Length;
        lineRenderer.SetPositions(linePos);
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
