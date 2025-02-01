using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Cell
{
    public event Action<Bishop, Action<Vector2, bool>> FigureClick;

    private void OnMouseDown()
    {
        if (FigureClick != null)
        {
            FigureClick(this, PossibleTurns);
        }
    }

    private void PossibleTurns(Vector2 pos, bool isWhite)
    {
        DiagonalFilling(pos.x, pos.y, isWhite);
    }
}
