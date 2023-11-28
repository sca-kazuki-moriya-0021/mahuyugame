using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemCon : MonoBehaviour
{
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        var dir = player.transform.position - transform.position;
        dir = dir.normalized;

        transform.Translate(dir * Time.deltaTime * 15.0f);
    }
}
