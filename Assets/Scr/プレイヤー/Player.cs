using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;
using System.Linq;
using Spine.Unity;
using Spine;

public class Player : MonoBehaviour
{
     private TotalGM gm;

    private void OnEnable()
    {
        gm = FindObjectOfType<TotalGM>();

        var scene = gm.MyGetScene();

        if (scene == gm.BackScene)
        {
           gm.PlayerHp[0] = gm.PlayerHp[1];
        }
        else if (scene != gm.BackScene)
        {
           gm.PlayerHp[1] = gm.PlayerHp[0];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gm.PlayerHp[0] == 0)
        {
            gm.BackScene = gm.MyGetScene();
            SceneManager.LoadScene("GameOver");
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bellet"))
        {
            gm.PlayerHp[0]--;
        }
    }
}
