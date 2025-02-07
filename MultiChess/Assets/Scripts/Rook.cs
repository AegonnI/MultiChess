using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Cell, IFigure<Rook>
{
    public event Action<Rook, Action<Vector2, bool>> FigureClick;

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(ClassicChessMain.SkinPath + "\\" + (isWhite ? "WhiteRook" : "BlackRook"));
        Rect spriteSize = Resources.Load<Sprite>(ClassicChessMain.SkinPath + "\\" + (isWhite ? "WhiteRook" : "BlackRook")).rect;
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
        DirectFilling(pos.x, pos.y, (int)(2 * ClassicChessMain.border), isWhite);
    }
}
