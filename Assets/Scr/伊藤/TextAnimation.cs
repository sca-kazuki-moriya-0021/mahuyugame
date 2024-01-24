using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TextAnimation : MonoBehaviour
{
    //[SerializeField] private TMP_Text tmptext;
    [SerializeField] private Text _text;
    // Start is called before the first frame update
    void Start()
    {
        _text.transform.DOLocalMove(new Vector3(-150f,0f,0f),2f);
        _text.DOText("1234567",2).SetEase(Ease.Linear);
    }
}
