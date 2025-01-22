using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Cell
{
    public event Action<Rook> FigureClick;

    private void OnMouseDown()
    {
        if (FigureClick != null)
        {
            FigureClick(this);
        }
    }
}
