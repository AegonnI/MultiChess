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

    protected static void DirectFilling(float x, float y, int maxDistance, bool isWhite)
    {
        DirectPadding(x + 1, y, 1, 0);
        DirectPadding(x - 1, y, -1, 0);
        DirectPadding(x, y + 1, 0, 1);
        DirectPadding(x, y - 1, 0, -1);

        void DirectPadding(float x, float y, int deltaX, int deltaY)
        {
            bool canAttack = true;
            int c = 0;

            while (Math.Abs(x) <= ClassicChessMain.border && Math.Abs(y) <= ClassicChessMain.border && canAttack && c < maxDistance)
            {
                if (ClassicChessMain.isCellOccupied(x, y))
                {
                    if (ClassicChessMain.GetAnOccupier(x, y).GetComponent<Cell>().isWhite == isWhite)
                    {
                        break;
                    }
                    canAttack = false;
                }

                ClassicChessMain.AddEmptyCell(x, y);
                x += deltaX;
                y += deltaY;
                c++;
            }
        }
    }

     protected static void DiagonalFilling(float posX, float posY,bool isWhite)
    {
        DiagonalPadding(posX + 1, x => x - posX + posY, 1);
        DiagonalPadding(posX + 1, x => -x + posX + posY, 1);
        DiagonalPadding(posX - 1, x => x - posX + posY, -1);
        DiagonalPadding(posX - 1, x => -x + posX + posY, -1);

        void DiagonalPadding(float x, Func<float, float> y, int delta)
        {
            bool canAttack = true;

            while (Math.Abs(x) <= ClassicChessMain.border && Math.Abs(y(x)) <= ClassicChessMain.border && canAttack)
            {
                if (ClassicChessMain.isCellOccupied(x, y(x)))
                {
                    if (ClassicChessMain.GetAnOccupier(x, y(x)).GetComponent<Cell>().isWhite == isWhite)
                    {
                        break;
                    }
                    canAttack = false;
                }

                ClassicChessMain.AddEmptyCell(x, y(x));
                x += delta;
            }
        }
    }
}
