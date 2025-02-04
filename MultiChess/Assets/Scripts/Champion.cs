using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Champion : Cell, IFigure<Champion>
{
    public event Action<Champion, Action<Vector2, bool>> FigureClick;

    private void OnMouseDown()
    {
        if (FigureClick != null)
        {
            FigureClick(this, PossibleTurns);
        }
    }

    private void PossibleTurns(Vector2 pos, bool isWhite)
    {
        DirectFilling(pos.x, pos.y, 2, isWhite);

        for (int i = -1; i <= 1; i += 2)
        {
            for (int j = -1; j <= 1; j += 2)
            {
                if (Math.Abs(i * 2 + pos.x) <= ClassicChessMain.border &&
                    Math.Abs(j * 2 + pos.y) <= ClassicChessMain.border && (
                    ClassicChessMain.IsOpponentOnTheCell(i * 2 + pos.x, j * 2 + pos.y, isWhite) ||
                    !ClassicChessMain.isCellOccupied(i * 2 + pos.x, j * 2 + pos.y)))
                {
                    ClassicChessMain.AddEmptyCell(i * 2 + pos.x, j * 2 + pos.y);
                }
            }
        }
    }
}
