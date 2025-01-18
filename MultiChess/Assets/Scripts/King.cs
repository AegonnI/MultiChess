using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Cell
{
    public event Action<King> FigureClick;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnMouseDown()
    //{
    //    isClicked = !isClicked;
    //    Debug.Log(isClicked);

    //    //ClassicChessMain.figureClicked = isClicked;
    //}

    private void OnMouseDown()
    {
        // Проверяем, есть ли подписчики на событие
        if (FigureClick != null)
        {
            // Вызываем событие, передавая ссылку на этого короля
            FigureClick(this);
        }
    }
}
