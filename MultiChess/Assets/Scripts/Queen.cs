using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Cell, IFigure<Queen>
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
        DirectFilling(pos.x, pos.y, (int)(2 * ClassicChessMain.border), isWhite);
        DiagonalFilling(pos.x, pos.y, isWhite);
    }
}
