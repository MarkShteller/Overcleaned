using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    public GameObject shirt;
    public FocusArrow focusArrow;
    

    public void setShirtColor(Color color)
    {
        shirt.GetComponent<SkinnedMeshRenderer>().materials[0].color = color;
        focusArrow.SetColor( color);
    }
}
