using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using DG.Tweening;

public class TitleLight2d : MonoBehaviour
{
    [SerializeField]private Light2D pointLight2d;
    // Start is called before the first frame update
    void Start()
    {
        IntensityChg();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void IntensityChg()
    {
        DOTween.To(
            () => pointLight2d.intensity,
            num => pointLight2d.intensity = num,
            0.5f,
            2f
            ).OnComplete(() =>
                DOTween.To(
                () => pointLight2d.intensity,
                num => pointLight2d.intensity = num,
                0.0f,
                2f
                ).OnComplete(() => IntensityChg())
        );
    }
}
