using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Cell
{
    public event Action<Queen> FigureClick;

    private void OnMouseDown()
    {
        if (FigureClick != null)
        {
            FigureClick(this);
        }
    }
}
