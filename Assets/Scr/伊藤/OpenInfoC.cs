using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class OpenInfoC : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Image k;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenInfo()
    {
        panel.SetActive(true);
        k.enabled = false;
    }
}
