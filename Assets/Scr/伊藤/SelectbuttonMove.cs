using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectbuttonMove : MonoBehaviour
{
    [SerializeField] 
    private ResultScoreDisplay rs;
    private bool selectFlag;

    public bool SelectFlag { get => selectFlag; set => selectFlag = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rs.Stop == true)
        {
            selectFlag=true;
        }

    }
}
