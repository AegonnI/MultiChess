using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Cell, IFigure<Knight>
{
    public event Action<Knight, Action<Vector2, bool>> FigureClick;

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(ClassicChessMain.SkinPath + "\\" + (isWhite ? "WhiteKnight" : "BlackKnight"));
        Rect spriteSize = Resources.Load<Sprite>(ClassicChessMain.SkinPath + "\\" + (isWhite ? "WhiteKnight" : "BlackKnight")).rect;
        gameObject.transform.localScale = new Vector2((spriteSize.width / 100) * ClassicChessMain.scalingFactor, (spriteSize.height / 100) * ClassicChessMain.scalingFactor);
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
                if (Math.Abs(i + pos.x) <= ClassicChessMain.border &&
                    Math.Abs(j * 2 + pos.y) <= ClassicChessMain.border && (
                    ClassicChessMain.IsOpponentOnTheCell(i + pos.x, j * 2 + pos.y, isWhite) ||
                    !ClassicChessMain.isCellOccupied(i + pos.x, j * 2 + pos.y)))
                {
                    ClassicChessMain.AddEmptyCell(i * 1 + pos.x, j * 2 + pos.y);
                }

                if (Math.Abs(i * 2 + pos.x) <= ClassicChessMain.border &&
                    Math.Abs(j + pos.y) <= ClassicChessMain.border && (
                    ClassicChessMain.IsOpponentOnTheCell(i * 2 + pos.x, j + pos.y, isWhite) ||
                    !ClassicChessMain.isCellOccupied(i * 2 + pos.x, j + pos.y)))
                {
                    ClassicChessMain.AddEmptyCell(i * 2 + pos.x, j + pos.y);
                }
            }
        }
    }
}
