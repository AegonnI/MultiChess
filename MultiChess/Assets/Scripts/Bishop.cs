using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Cell
{
    public event Action<Bishop> FigureClick;

    private void OnMouseDown()
    {
        if (FigureClick != null)
        {
            FigureClick(this);
        }
    }
}
