using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Cell, IFigure<Bishop>
{
    public event Action<Bishop, Action<Vector2, bool>> FigureClick;

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(ClassicChessMain.SkinPath + "\\" + (isWhite ? "WhiteBishop" : "BlackBishop"));
        Rect spriteSize = Resources.Load<Sprite>(ClassicChessMain.SkinPath + "\\" + (isWhite ? "WhiteBishop" : "BlackBishop")).rect;
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
        DiagonalFilling(pos.x, pos.y, isWhite);
    }
}
