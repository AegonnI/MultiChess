using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Champion : Cell
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

    }
}
