using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Cell
{
    public event Action<Queen> FigureClick;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnMouseDown()
    {
        //Debug.Log("QueenClicked");
        if (FigureClick != null)
        {
            FigureClick(this);
        }
    }
}
