using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectObjGetSet : MonoBehaviour
{
    private Button lastSelectButton;

    public Button LastSelectButton
    {
        get { return this.lastSelectButton; }
        set { this.lastSelectButton = value;}
    }
}
