using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Cell
{
    public event Action<Pawn> FigureClick;

    private void OnMouseDown()
    {
        if (FigureClick != null)
        {
            FigureClick(this);
        }
    }
}
