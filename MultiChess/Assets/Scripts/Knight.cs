using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Cell
{
    public event Action<Knight> FigureClick;

    private void OnMouseDown()
    {
        if (FigureClick != null)
        {
            FigureClick(this);
        }
    }
}
