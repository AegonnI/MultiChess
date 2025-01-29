using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Cell
{
    public event Action<Queen, Action<Vector2, bool>> FigureClick;

    private void OnMouseDown()
    {
        if (FigureClick != null)
        {
            FigureClick(this, PossibleTurns);
        }
    }

    private void PossibleTurns(Vector2 pos, bool isWhite)
    {
        ClassicChessMain.DirectFilling(pos.x, pos.y, isWhite);
        ClassicChessMain.DiagonalFilling(pos.x, pos.y, isWhite);
    }
}
