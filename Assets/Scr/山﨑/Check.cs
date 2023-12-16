using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Check : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField]
    private EventSystem ev = EventSystem.current;
    private GameObject selectedObj;
    [SerializeField] GameObject skillCanvas;
    [SerializeField] GameObject BarrageCanvas;
    private SelectObjGetSet selectObjGetSet;

    // Start is called before the first frame update
    void Start()
    {
        selectObjGetSet = FindObjectOfType<SelectObjGetSet>();
        button.Select();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(skillSelection.LastSelectButton);
        if (selectedObj == null)
        {
            button.Select();
            selectedObj = ev.currentSelectedGameObject;
        }
        else
        {
            selectedObj = ev.currentSelectedGameObject;
            //outLine.transform.position = selectedObj.transform.position;
            //OutLineSize();
        }
    }

    public void Yes()
    {
        BarrageCanvas.SetActive(true);
        this.gameObject.SetActive(false);
    }
    public void No()
    {

        skillCanvas.SetActive(true);
        button = selectObjGetSet.LastSelectButton;
        button.Select();
        this.gameObject.SetActive(false);
    }
}
