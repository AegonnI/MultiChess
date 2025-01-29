using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Web;
using System;

public class Cell : MonoBehaviour
{
    protected KeyValuePair<int, int> index;
    protected bool isClicked;
    public bool isWhite;

    //public event Action<this, Action<Vector2, bool>> FigureClick;
}
