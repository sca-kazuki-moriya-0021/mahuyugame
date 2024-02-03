using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleOperationAnimaCon : MonoBehaviour
{
    [SerializeField]
    private TitleStageCon stageCon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAnimationCompleted() => stageCon.AnimEndFlag = true;
}
