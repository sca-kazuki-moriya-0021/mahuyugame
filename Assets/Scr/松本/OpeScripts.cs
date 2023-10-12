using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpeScripts : MonoBehaviour
{
    [SerializeField]
    GameObject Panel = null;
    // Start is called before the first frame update
    void Start()
    {
        Panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Opetrue()
    {
        if (Panel.activeSelf)
        {
            Panel.SetActive(false);
            //Time.timeScale = 1.0f;
        }
        else
        {
            Panel.SetActive(true);
            //Time.timeScale = 0.0f;
        }
    }
}
