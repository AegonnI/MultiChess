using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class King : Cell, IFigure<King>
{
    public event Action<King, Action<Vector2, bool>> FigureClick;

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(ClassicChessMain.SkinPath + "\\" + (isWhite ? "WhiteKing" : "BlackKing"));
        Rect spriteSize = Resources.Load<Sprite>(ClassicChessMain.SkinPath + "\\" + (isWhite ? "WhiteKing" : "BlackKing")).rect;
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

    private bool Check()
    {
        //Vector2 pos = gameObject.transform.position;

        //for(float i = pos.x; i < ClassicChessMain.border; i++)
        //{
        //    ClassicChessMain.IsOpponentOnTheCell(x, y, isWhite);
        //}
        return false;
    }
}
