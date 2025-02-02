using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Cell
{
    public event Action<Rook, Action<Vector2, bool>> FigureClick;

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
    }
}
