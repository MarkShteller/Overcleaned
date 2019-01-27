using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusArrow : MonoBehaviour
{
    public MeshRenderer r1;
    public MeshRenderer r2;

    public void Show()
    {
        r1.enabled = true;
        r2.enabled = true;
    }

    public void Hide()
    {
        r1.enabled = false;
        r2.enabled = false;
    }

    public void SetColor(Color c)
    {
        r1.material.color = c;
        r2.material.color = c;
    }
}
