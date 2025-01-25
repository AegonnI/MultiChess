using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCell : Cell
{
    public event Action<EmptyCell, GameObject> emptyCellClick;

    private void OnMouseDown()
    {
        if (emptyCellClick != null && ClassicChessMain.ChoosenFigure != null)
        {
            emptyCellClick(this, ClassicChessMain.ChoosenFigure);
        }
    }
}
