using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Cell, IFigure<Wizard>
{
    public event Action<Wizard, Action<Vector2, bool>> FigureClick;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseDown()
    {
        if (FigureClick != null)
        {
            FigureClick(this, PossibleTurns);
        }
    }

    private void PossibleTurns(Vector2 pos, bool isWhite)
    {
        for (int i = -1; i <= 1; i += 2)
        {
            for (int j = -1; j <= 1; j += 2)
            {
                float x = i + pos.x;
                float y = j + pos.y;

                if (Math.Abs(x) <= ClassicChessMain.border &&
                    Math.Abs(y) <= ClassicChessMain.border && (
                    ClassicChessMain.IsOpponentOnTheCell(x, y, isWhite) ||
                    !ClassicChessMain.isCellOccupied(x, y)))
                {
                    ClassicChessMain.AddEmptyCell(x, y);
                }

                x += i;

                if (Math.Abs(x) <= ClassicChessMain.border &&
                    Math.Abs(y) <= ClassicChessMain.border && (
                    ClassicChessMain.IsOpponentOnTheCell(x, y, isWhite) ||
                    !ClassicChessMain.isCellOccupied(x, y)))
                {
                    ClassicChessMain.AddEmptyCell(x, y);
                }

                x -= i;
                y += j;

                if (Math.Abs(x) <= ClassicChessMain.border &&
                    Math.Abs(y) <= ClassicChessMain.border && (
                    ClassicChessMain.IsOpponentOnTheCell(x, y, isWhite) ||
                    !ClassicChessMain.isCellOccupied(x, y)))
                {
                    ClassicChessMain.AddEmptyCell(x, y);
                }
            }
        }
    }
}
