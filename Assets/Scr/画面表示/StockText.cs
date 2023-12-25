using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StockText : MonoBehaviour
{
    [SerializeField]
    private Text stockText;
    private TotalGM gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<TotalGM>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.PlayerHp[0] >= 0)
        {
            stockText.text = "Hp" + gm.PlayerHp[0].ToString();
        }
        else if (gm.PlayerHp[0] < 0)
        {
            stockText.text = "Hp" + 0.ToString();
        }
      
    }
}
