using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Cell
{
    public event Action<Pawn> FigureClick;
    public bool isFirstTurn { set; get; }

    private void Start()
    {
        isFirstTurn = true;
    }

    private void OnMouseDown()
    {
        if (FigureClick != null)
        {
            FigureClick(this);
        }
    }
}
