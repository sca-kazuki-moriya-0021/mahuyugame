using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StockText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text stockText;
    private TotalGM gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<TotalGM>();
    }

    // Update is called once per frame
    void Update()
    {
        stockText.text = "Stock:" + gm.PlayerHp[0].ToString();
    }
}
