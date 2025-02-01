using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Cell
{
    public event Action<Pawn, Action<Vector2, bool>> FigureClick;
    public bool isFirstTurn { set; get; }

    private void Start()
    {
        isFirstTurn = true;
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
        int factor = isWhite ? 1 : -1;

        if (!ClassicChessMain.isCellOccupied(pos.x, pos.y + factor) && Math.Abs(pos.y + factor) <= ClassicChessMain.border)
        {
            ClassicChessMain.AddEmptyCell(pos.x, pos.y + factor);

            if (!ClassicChessMain.isCellOccupied(pos.x, pos.y + factor) && isFirstTurn)
            {
                ClassicChessMain.AddEmptyCell(pos.x, pos.y + factor * 2);
            }
        }

        if (Math.Abs(pos.y + factor) <= ClassicChessMain.border)
        {
            if (Math.Abs(pos.x + 1) <= ClassicChessMain.border &&
                ClassicChessMain.isCellOccupied(pos.x + 1, pos.y + factor) &&
                ClassicChessMain.IsOpponentOnTheCell(pos.x + 1, pos.y + factor, isWhite))
            {
                ClassicChessMain.AddEmptyCell(pos.x + 1, pos.y + factor);
            }
            if (Math.Abs(pos.x - 1) <= ClassicChessMain.border &&
                ClassicChessMain.isCellOccupied(pos.x - 1, pos.y + factor) &&
                ClassicChessMain.IsOpponentOnTheCell(pos.x - 1, pos.y + factor, isWhite))
            {
                ClassicChessMain.AddEmptyCell(pos.x - 1, pos.y + factor);
            }
        }
    }
}
