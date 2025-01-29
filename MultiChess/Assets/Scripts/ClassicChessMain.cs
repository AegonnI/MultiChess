using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ClassicChessMain : MonoBehaviour
{
    private const byte _fieldSize = 8;

    public GameObject bgCell;
    public GameObject emptyCell;
    private static GameObject MoveCell;

    //White
    public King whiteKing;
    public Queen whiteQueen;

    public Rook whiteRook1;
    public Rook whiteRook2;

    public Knight whiteKnight1;
    public Knight whiteKnight2;

    public Bishop whiteBishop1;
    public Bishop whiteBishop2;

    public Pawn whitePawn1;
    public Pawn whitePawn2;
    public Pawn whitePawn3;
    public Pawn whitePawn4;
    public Pawn whitePawn5;
    public Pawn whitePawn6;
    public Pawn whitePawn7;
    public Pawn whitePawn8;

    //Black
    public King blackKing;
    public Queen blackQueen;

    public Rook blackRook1;
    public Rook blackRook2;

    public Knight blackKnight1;
    public Knight blackKnight2;

    public Bishop blackBishop1;
    public Bishop blackBishop2;

    public Pawn blackPawn1;
    public Pawn blackPawn2;
    public Pawn blackPawn3;
    public Pawn blackPawn4;
    public Pawn blackPawn5;
    public Pawn blackPawn6;
    public Pawn blackPawn7;
    public Pawn blackPawn8;
    //

    private static List<GameObject> emptyCells;
    private static bool isWhiteMove;
    private static GameObject[,] cellOccupied;
    private List<GameObject> figures;

    public static GameObject ChoosenFigure;
    public static bool figureClicked;



    void Start()
    {
        figureClicked = false;
        emptyCells = new List<GameObject>();
        ChoosenFigure = null;
        isWhiteMove = true;
        MoveCell = emptyCell;

        cellOccupied = new GameObject[8,8];
        for (int i = 0; i < cellOccupied.GetLength(0); i++)
        {
            for (int j = 0; j < cellOccupied.GetLength(0); j++)
            {
                cellOccupied[i,j] = null;
            }
        }

        figures = new List<GameObject>();
        figures.Add(whiteKing.GameObject());
        figures.Add(whiteQueen.GameObject());
        figures.Add(whiteRook1.GameObject());
        figures.Add(whiteRook2.GameObject());
        figures.Add(whiteKnight1.GameObject());
        figures.Add(whiteKnight2.GameObject());
        figures.Add(whiteBishop1.GameObject());
        figures.Add(whiteBishop2.GameObject());
        figures.Add(whitePawn1.GameObject());
        figures.Add(whitePawn2.GameObject());
        figures.Add(whitePawn3.GameObject());
        figures.Add(whitePawn4.GameObject());
        figures.Add(whitePawn5.GameObject());
        figures.Add(whitePawn6.GameObject());
        figures.Add(whitePawn7.GameObject());
        figures.Add(whitePawn8.GameObject());
        figures.Add(blackKing.GameObject());
        figures.Add(blackQueen.GameObject());
        figures.Add(blackRook1.GameObject());
        figures.Add(blackRook2.GameObject());
        figures.Add(blackKnight1.GameObject());
        figures.Add(blackKnight2.GameObject());
        figures.Add(blackBishop1.GameObject());
        figures.Add(blackBishop2.GameObject());
        figures.Add(blackPawn1.GameObject());
        figures.Add(blackPawn2.GameObject());
        figures.Add(blackPawn3.GameObject());
        figures.Add(blackPawn4.GameObject());
        figures.Add(blackPawn5.GameObject());
        figures.Add(blackPawn6.GameObject());
        figures.Add(blackPawn7.GameObject());
        figures.Add(blackPawn8.GameObject());

        foreach (GameObject figure in figures)
        {
            FillcellOccupied(figure);
        }

        for (int i = 0; i < _fieldSize; i++)
        {
            for (int j = 0; j < _fieldSize; j++)
            {                
                bgCell.GetComponent<SpriteRenderer>().color = (i + j + 2) % 2 == 0 ? new Color(0.9f, 0.9f, 0.8f, 1f) : new Color(0.27f, 0.3f, 0.37f, 1f);
                bgCell.name = "bgCell [" + i + ';' + j + "]";

                Instantiate(bgCell, new Vector2(j - 3.5f, - i + 3.5f), Quaternion.identity);
            }
        }

        //whiteKing.transform.position = new Vector3(4 - 3.5f, -7 + 3.5f, -1);

        //White
        AppointFigure(whiteKing, () => whiteKing.FigureClick += OnFigureClick);
        AppointFigure(whiteQueen, () => whiteQueen.FigureClick += OnFigureClick);

        AppointFigure(whiteRook1, () => whiteRook1.FigureClick += OnFigureClick);
        AppointFigure(whiteRook2, () => whiteRook2.FigureClick += OnFigureClick);

        AppointFigure(whiteKnight1, () => whiteKnight1.FigureClick += OnFigureClick);
        AppointFigure(whiteKnight2, () => whiteKnight2.FigureClick += OnFigureClick);

        AppointFigure(whiteBishop1, () => whiteBishop1.FigureClick += OnFigureClick);
        AppointFigure(whiteBishop2, () => whiteBishop2.FigureClick += OnFigureClick);

        AppointFigure(whitePawn1, () => whitePawn1.FigureClick += OnFigureClick);
        AppointFigure(whitePawn2, () => whitePawn2.FigureClick += OnFigureClick);
        AppointFigure(whitePawn3, () => whitePawn3.FigureClick += OnFigureClick);
        AppointFigure(whitePawn4, () => whitePawn4.FigureClick += OnFigureClick);
        AppointFigure(whitePawn5, () => whitePawn5.FigureClick += OnFigureClick);
        AppointFigure(whitePawn6, () => whitePawn6.FigureClick += OnFigureClick);
        AppointFigure(whitePawn7, () => whitePawn7.FigureClick += OnFigureClick);
        AppointFigure(whitePawn8, () => whitePawn8.FigureClick += OnFigureClick);

        //black
        AppointFigure(blackKing, () => blackKing.FigureClick += OnFigureClick);
        AppointFigure(blackQueen, () => blackQueen.FigureClick += OnFigureClick);

        AppointFigure(blackRook1, () => blackRook1.FigureClick += OnFigureClick);
        AppointFigure(blackRook2, () => blackRook2.FigureClick += OnFigureClick);

        AppointFigure(blackKnight1, () => blackKnight1.FigureClick += OnFigureClick);
        AppointFigure(blackKnight2, () => blackKnight2.FigureClick += OnFigureClick);

        AppointFigure(blackBishop1, () => blackBishop1.FigureClick += OnFigureClick);
        AppointFigure(blackBishop2, () => blackBishop2.FigureClick += OnFigureClick);

        AppointFigure(blackPawn1, () => blackPawn1.FigureClick += OnFigureClick);
        AppointFigure(blackPawn2, () => blackPawn2.FigureClick += OnFigureClick);
        AppointFigure(blackPawn3, () => blackPawn3.FigureClick += OnFigureClick);
        AppointFigure(blackPawn4, () => blackPawn4.FigureClick += OnFigureClick);
        AppointFigure(blackPawn5, () => blackPawn5.FigureClick += OnFigureClick);
        AppointFigure(blackPawn6, () => blackPawn6.FigureClick += OnFigureClick);
        AppointFigure(blackPawn7, () => blackPawn7.FigureClick += OnFigureClick);
        AppointFigure(blackPawn8, () => blackPawn8.FigureClick += OnFigureClick);

    }

    private void FillcellOccupied(GameObject gameObject)
    {
        cellOccupied[(int)(gameObject.transform.position.x + 3.5f), (int)(gameObject.transform.position.y + 3.5f)] = gameObject;
    }

    public static bool isCellOccupied(float x, float y)
    {
        return cellOccupied[(int)(x + 3.5f), (int)(y + 3.5f)] != null;
    }

    private static GameObject GetAnOccupier(float x, float y)
    {
        return cellOccupied[(int)(x + 3.5f), (int)(y + 3.5f)];
    }

    public static bool IsOpponentOnTheCell(float x, float y, bool isWhite)
    {
        return isCellOccupied(x, y) && GetAnOccupier(x, y).GetComponent<Cell>().isWhite != isWhite;
    }

    //private void FiguresRegister()
    //{
    //    foreach (GameObject figure in figures)
    //    {
    //        if (figure != null)
    //            figure.GetComponent<Cell>().FigureClick += OnFigureClick;
    //        else
    //            Debug.LogError("Необходимо назначить фигуру в ClassicChessMain!");
    //    }
    //}

    private void AppointFigure<T>(T figure, Action action)
    {
        if (figure != null)
            action();
        else
            Debug.LogError("Необходимо назначить фигуру в ClassicChessMain!");
    }


    private void OnDestroy()
    {
        if (whiteKing != null)
            whiteKing.GetComponent<King>().FigureClick -= OnFigureClick;
    }

    private void OnFigureClick(Cell clickedFigure, Action<Vector2, bool> action)
    {
        Debug.Log($"Клик на: {clickedFigure.gameObject.name}");

        if (!figureClicked && isWhiteMove == clickedFigure.isWhite)
        {
            figureClicked = true;
            ChoosenFigure = clickedFigure.GameObject();

            action(clickedFigure.transform.position, clickedFigure.isWhite);
        }
        else
        {
            if (ChoosenFigure != null && ChoosenFigure.GetComponent<Cell>().isWhite != clickedFigure.isWhite)
            {
                KillFigure(clickedFigure.GameObject());
                isWhiteMove = !isWhiteMove;
            }
            DestroyEmptyCells();
        }
    }

    private void KillFigure(GameObject clickedFigure)
    {
        Vector2 figurePos = clickedFigure.transform.position;
        foreach (GameObject emptyCell in emptyCells)
        {
            Vector2 emptyCellPos = emptyCell.transform.position;
            if (emptyCellPos == figurePos)
            {
                Destroy(clickedFigure.gameObject);
                ChoosenFigure.transform.position = emptyCell.transform.position;
            }
        }
    }

    public static void DirectFilling(float x, float y, bool isWhite)
    {
        DirectPadding(x + 1, y, 1, 0);
        DirectPadding(x - 1, y, -1, 0);
        DirectPadding(x, y + 1, 0, 1);
        DirectPadding(x, y - 1, 0, -1);

        void DirectPadding(float x, float y, int deltaX, int deltaY)
        {
            bool canAttack = true;

            while (Math.Abs(x) <= 3.5f && Math.Abs(y) <= 3.5f && canAttack)
            {
                if (isCellOccupied(x, y))
                {
                    if (GetAnOccupier(x, y).GetComponent<Cell>().isWhite == isWhite)
                    {
                        break;
                    }
                    canAttack = false;
                }

                AddEmptyCell(x, y);
                x += deltaX;
                y += deltaY;
            }
        }
    }

    public static void DiagonalFilling(float posX, float posY, bool isWhite)
    {
        DiagonalPadding(posX + 1, x => x - posX + posY, 1);
        DiagonalPadding(posX + 1, x => -x + posX + posY, 1);
        DiagonalPadding(posX - 1, x => x - posX + posY, -1);
        DiagonalPadding(posX - 1, x => -x + posX + posY, -1);

        void DiagonalPadding(float x, Func<float, float> y, int delta)
        {
            bool canAttack = true;

            while (Math.Abs(x) <= 3.5f && Math.Abs(y(x)) <= 3.5f && canAttack)
            {
                if (isCellOccupied(x, y(x)))
                {
                    if (GetAnOccupier(x, y(x)).GetComponent<Cell>().isWhite == isWhite)
                    {
                        break;
                    }
                    canAttack = false;
                }

                AddEmptyCell(x, y(x));
                x += delta;
            }
        }
    }

    public static void AddEmptyCell(float x, float y)
    {
        emptyCells.Add(Instantiate(MoveCell, new Vector3(x, y, -0.9f), Quaternion.identity));
        emptyCells.Last().GetComponent<EmptyCell>().emptyCellClick += OnEmptyCellClicked;
    }

    private static void DestroyEmptyCells()
    {
        figureClicked = false;
        ChoosenFigure = null;

        foreach (var cell in emptyCells)
        {
            Destroy(cell.gameObject);
        }
        emptyCells.Clear();
    }

    private static void OnEmptyCellClicked(EmptyCell clickedCell, GameObject clickedFigure)
    {
        Debug.Log($"Клик на клетку для хода: {clickedCell.gameObject.name}");

        cellOccupied[(int)(clickedFigure.transform.position.x + 3.5f), (int)(clickedFigure.transform.position.y + 3.5f)] = null;

        clickedFigure.transform.position = new Vector3(clickedCell.transform.position.x, clickedCell.transform.position.y, -1);
        isWhiteMove = !isWhiteMove;

        if (clickedFigure.GetComponent<Pawn>())
        {
            clickedFigure.GetComponent<Pawn>().isFirstTurn = false;
        }

        cellOccupied[(int)(clickedFigure.transform.position.x + 3.5f), (int)(clickedFigure.transform.position.y + 3.5f)] = clickedFigure.GameObject();

        DestroyEmptyCells();
    }
}
