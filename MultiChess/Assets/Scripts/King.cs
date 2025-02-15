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
            //if (Check())
            //{
            //    if (isWhite)
            //    {
            //        ClassicChessMain.checkOfWhite = true;
            //    }
            //    else
            //    {
            //        ClassicChessMain.checkOfBlack = true;
            //    }
            //    Debug.Log(ClassicChessMain.checkOfWhite);
            //    Debug.Log(ClassicChessMain.checkOfBlack);
            //}
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

    public bool Check()
    {
        Vector2 pos = gameObject.transform.position;

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i != j || (i == j && i != 0 && j != 0))
                {
                    if (CheckTheThreat(pos.x + i, pos.y + j, i, j))
                    {
                        return true;
                    }
                }
            }
        }
        
        
        //return 
        //    CheckTheThreat(pos.x + 1, pos.y, 1, 0) || CheckTheThreat(pos.x - 1, pos.y, -1, 0) || 
        //    CheckTheThreat(pos.x, pos.y + 1, 0, 1) || CheckTheThreat(pos.x, pos.y - 1, 0, -1) ||
        //    CheckTheThreat(pos.x + 1, pos.y + 1, 1, 1) || CheckTheThreat(pos.x + 1, pos.y - 1, 1, -1) ||
        //    CheckTheThreat(pos.x - 1, pos.y - 1, -1, -1) || CheckTheThreat(pos.x - 1, pos.y + 1, -1, 1);

        bool CheckTheThreat(float startX, float startY, float deltaX, float deltaY)
        {
            for (float x = startX, y = startY; Math.Abs(x) <= ClassicChessMain.border && Math.Abs(y) <= ClassicChessMain.border; x += deltaX, y += deltaY)
            {
                if (ClassicChessMain.isCellOccupied(x, y) && ClassicChessMain.GetAnOccupier(x, y).GetComponent<Cell>().isWhite != isWhite)
                {
                    if (ClassicChessMain.GetAnOccupier(x, y).GetComponent<Bishop>() || ClassicChessMain.GetAnOccupier(x, y).GetComponent<Queen>())
                    {
                        return true;
                    }
                    break;
                }
            }
            return false;
        }

        return false;
    }
}
