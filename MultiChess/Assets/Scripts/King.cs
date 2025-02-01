using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class King : Cell
{
    public event Action<King, Action<Vector2, bool>> FigureClick;

    private void OnMouseDown()
    {
        if (FigureClick != null)
        {
            FigureClick(this, PossibleTurns);
        }
    }

    private void PossibleTurns(Vector2 pos, bool isWhite)
    {
        float x = pos.x + 1; float y = pos.y + 1;
        float dx = 0; float dy = -1;

        for (int i = 0; i < 8; i++)
        {
            if (Math.Abs(x) <= ClassicChessMain.border &&
                Math.Abs(y) <= ClassicChessMain.border && (
                ClassicChessMain.IsOpponentOnTheCell(x, y, isWhite) ||
                !ClassicChessMain.isCellOccupied(x, y)))
            {
                ClassicChessMain.AddEmptyCell(x, y);
            }

            if (x + dx > pos.x + 1 || x + dx < pos.x - 1 || y + dy > pos.y + 1 || y + dy < pos.y - 1)
            {
                (dx, dy) = (dy, -dx);
            }
            x += dx; y += dy;
        }
    }
}
