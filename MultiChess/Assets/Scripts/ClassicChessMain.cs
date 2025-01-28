using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ClassicChessMain : MonoBehaviour
{
    private const byte _fieldSize = 8;

    public GameObject bgCell;
    public GameObject emptyCell;

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

    private List<GameObject> emptyCells;
    private bool isWhiteMove;
    private GameObject[,] cellOccupied;
    private List<GameObject> figures;

    public static GameObject ChoosenFigure;
    public static bool figureClicked;



    void Start()
    {
        figureClicked = false;
        emptyCells = new List<GameObject>();
        ChoosenFigure = null;
        isWhiteMove = true;

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
        AppointFigure(whiteKing, () => whiteKing.FigureClick += OnKingClicked);
        AppointFigure(whiteQueen, () => whiteQueen.FigureClick += OnQueenClicked);

        AppointFigure(whiteRook1, () => whiteRook1.FigureClick += OnRookClicked);
        AppointFigure(whiteRook2, () => whiteRook2.FigureClick += OnRookClicked);

        AppointFigure(whiteKnight1, () => whiteKnight1.FigureClick += OnKnightClicked);
        AppointFigure(whiteKnight2, () => whiteKnight2.FigureClick += OnKnightClicked);

        AppointFigure(whiteBishop1, () => whiteBishop1.FigureClick += OnBishopClicked);
        AppointFigure(whiteBishop2, () => whiteBishop2.FigureClick += OnBishopClicked);

        AppointFigure(whitePawn1, () => whitePawn1.FigureClick += OnPawnClicked);
        AppointFigure(whitePawn2, () => whitePawn2.FigureClick += OnPawnClicked);
        AppointFigure(whitePawn3, () => whitePawn3.FigureClick += OnPawnClicked);
        AppointFigure(whitePawn4, () => whitePawn4.FigureClick += OnPawnClicked);
        AppointFigure(whitePawn5, () => whitePawn5.FigureClick += OnPawnClicked);
        AppointFigure(whitePawn6, () => whitePawn6.FigureClick += OnPawnClicked);
        AppointFigure(whitePawn7, () => whitePawn7.FigureClick += OnPawnClicked);
        AppointFigure(whitePawn8, () => whitePawn8.FigureClick += OnPawnClicked);

        //black
        AppointFigure(blackKing, () => blackKing.FigureClick += OnKingClicked);
        AppointFigure(blackQueen, () => blackQueen.FigureClick += OnQueenClicked);

        AppointFigure(blackRook1, () => blackRook1.FigureClick += OnRookClicked);
        AppointFigure(blackRook2, () => blackRook2.FigureClick += OnRookClicked);

        AppointFigure(blackKnight1, () => blackKnight1.FigureClick += OnKnightClicked);
        AppointFigure(blackKnight2, () => blackKnight2.FigureClick += OnKnightClicked);

        AppointFigure(blackBishop1, () => blackBishop1.FigureClick += OnBishopClicked);
        AppointFigure(blackBishop2, () => blackBishop2.FigureClick += OnBishopClicked);

        AppointFigure(blackPawn1, () => blackPawn1.FigureClick += OnPawnClicked);
        AppointFigure(blackPawn2, () => blackPawn2.FigureClick += OnPawnClicked);
        AppointFigure(blackPawn3, () => blackPawn3.FigureClick += OnPawnClicked);
        AppointFigure(blackPawn4, () => blackPawn4.FigureClick += OnPawnClicked);
        AppointFigure(blackPawn5, () => blackPawn5.FigureClick += OnPawnClicked);
        AppointFigure(blackPawn6, () => blackPawn6.FigureClick += OnPawnClicked);
        AppointFigure(blackPawn7, () => blackPawn7.FigureClick += OnPawnClicked);
        AppointFigure(blackPawn8, () => blackPawn8.FigureClick += OnPawnClicked);

    }

    private void FillcellOccupied(GameObject gameObject)
    {
        cellOccupied[(int)(gameObject.transform.position.x + 3.5f), (int)(gameObject.transform.position.y + 3.5f)] = gameObject;
    }

    private bool isCellOccupied(float x, float y)
    {
        return cellOccupied[(int)(x + 3.5f), (int)(y + 3.5f)] != null;
    }

    private GameObject GetAnOccupier(float x, float y)
    {
        return cellOccupied[(int)(x + 3.5f), (int)(y + 3.5f)];
    }

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
            whiteKing.GetComponent<King>().FigureClick -= OnKingClicked;
    }

    private void OnFigureClick(Cell clickedFigure, Action<Vector2> action)
    {
        Debug.Log($"Клик на короля: {clickedFigure.gameObject.name}");

        if (!figureClicked)
        {
            figureClicked = true;
            ChoosenFigure = clickedFigure.GameObject();

            action(clickedFigure.transform.position);
        }
        else
        {
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

    private void OnKingClicked(King clickedFigure)
    {
        Debug.Log($"Клик на короля: {clickedFigure.gameObject.name}");

        if (!figureClicked && isWhiteMove == clickedFigure.isWhite)
        {
            figureClicked = true;
            ChoosenFigure = clickedFigure.GameObject();

            KingPossibleTurns(clickedFigure.transform.position);
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

    private void KingPossibleTurns(Vector2 pos)
    {
        float x = pos.x + 1;
        float y = pos.y + 1;
        float dx = 0;
        float dy = -1;

        for (int i = 0; i < 8; i++)
        {
            if (Math.Abs(x) <= 3.5 && Math.Abs(y) <= 3.5)
            {
                AddEmptyCell(x, y);
            }

            if (x + dx > pos.x + 1 || x + dx < pos.x - 1 || y + dy > pos.y + 1 || y + dy < pos.y - 1)
            {
                (dx, dy) = (dy, -dx);
            }
            x += dx;
            y += dy;
        }
    }

    private void OnQueenClicked(Queen clickedFigure)
    {
        Debug.Log($"Клик на ферзя: {clickedFigure.gameObject.name}");

        if (!figureClicked && isWhiteMove == clickedFigure.isWhite)
        {
            figureClicked = true;
            ChoosenFigure = clickedFigure.GameObject();

            Vector2 pos = clickedFigure.transform.position;

            for (float i = -3.5f; i <= 3.5f; i += 1)
            {
                if (i != pos.x)
                {
                    AddEmptyCell(i, pos.y);
                }
                if (i != pos.y)
                {
                    AddEmptyCell(pos.x, i);
                }

            }

            for (float x = -3.5f; x <= 3.5f; x += 1)
            {
                float y = x - pos.x + pos.y;
                float uy = -x + pos.x + pos.y;

                if (Math.Abs(x) <= 3.5 && x != pos.x)
                {
                    if (y != pos.y && Math.Abs(y) <= 3.5)
                    {
                        AddEmptyCell(x, y);
                    }

                    if (uy != pos.y && Math.Abs(uy) <= 3.5)
                    {
                        AddEmptyCell(x, uy);
                    }
                }
            }
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

    private void OnRookClicked(Rook clickedFigure)
    {
        Debug.Log($"Клик на ладью: {clickedFigure.gameObject.name}");

        if (!figureClicked && isWhiteMove == clickedFigure.isWhite)
        {
            figureClicked = true;
            ChoosenFigure = clickedFigure.GameObject();

            Vector2 pos = clickedFigure.transform.position;

            for (float i = -3.5f; i <= 3.5f; i += 1)
            {
                if (i != pos.x)
                {
                    AddEmptyCell(i, pos.y);
                }
                if (i != pos.y)
                {
                    AddEmptyCell(pos.x, i);
                }
            }
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

    private void OnKnightClicked(Knight clickedFigure)
    {
        Debug.Log($"Клик на коня: {clickedFigure.gameObject.name}");

        if (!figureClicked && isWhiteMove == clickedFigure.isWhite)
        {
            figureClicked = true;
            ChoosenFigure = clickedFigure.GameObject();

            Vector2 pos = clickedFigure.transform.position;          

            for (int i = -1 ; i <= 1; i += 2)
            {
                for (int j = -1; j <= 1; j += 2)
                {
                    if (Math.Abs(i * 1 + pos.x) <= 3.5 && Math.Abs(j * 2 + pos.y) <= 3.5)
                    {
                        AddEmptyCell(i * 1 + pos.x, j * 2 + pos.y);
                    }

                    if (Math.Abs(i * 2 + pos.x) <= 3.5 && Math.Abs(j * 1 + pos.y) <= 3.5)
                    {
                        AddEmptyCell(i * 2 + pos.x, j * 1 + pos.y);
                    }
                }
            }
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

    private void OnBishopClicked(Bishop clickedFigure)
    {
        Debug.Log($"Клик на ферзя: {clickedFigure.gameObject.name}");

        if (!figureClicked && isWhiteMove == clickedFigure.isWhite)
        {
            figureClicked = true;
            ChoosenFigure = clickedFigure.GameObject();

            Vector2 pos = clickedFigure.transform.position;

            DiagonalPadding(pos.x + 1, x => x - pos.x + pos.y, 1, clickedFigure.isWhite);
            DiagonalPadding(pos.x + 1, x => -x + pos.x + pos.y, 1, clickedFigure.isWhite);

            DiagonalPadding(pos.x - 1, x => x - pos.x + pos.y, -1, clickedFigure.isWhite);
            DiagonalPadding(pos.x - 1, x => -x + pos.x + pos.y, -1, clickedFigure.isWhite);

            //float x = pos.x + 1;
            //float y = x - pos.x + pos.y;

            //while(!isCellOccupied(x, y) || x <= 3.5f || y <= 3.5f)
            //{
            //    AddEmptyCell(x, y);
            //    x += 1;
            //    y += 1;
            //}

            //for (float x = -3.5f; x <= 3.5f; x += 1)
            //{
            //    float y = x - pos.x + pos.y;
            //    float uy = -x + pos.x + pos.y;

            //    if (Math.Abs(x) <= 3.5 && x != pos.x)
            //    {
            //        if (y != pos.y && Math.Abs(y) <= 3.5)
            //        {
            //            AddEmptyCell(x, y);
            //        }

            //        if (uy != pos.y && Math.Abs(uy) <= 3.5)
            //        {
            //            AddEmptyCell(x, uy);
            //        }
            //    }
            //}
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

    private void DiagonalPadding(float x, Func<float, float> y, int delta, bool isWhite)
    {
        bool canAttack = true;

        while (Math.Abs(x) <= 3.5f && Math.Abs(y(x)) <= 3.5f && canAttack) 
        {
            if(isCellOccupied(x, y(x)))
            {
                if(GetAnOccupier(x, y(x)).GetComponent<Cell>().isWhite == isWhite)
                {
                    break;
                }
                canAttack = false;
            }

            AddEmptyCell(x, y(x));
            x += delta;
        }
    }

    private void OnPawnClicked(Pawn clickedFigure)
    {
        Debug.Log($"Клик на ферзя: {clickedFigure.gameObject.name}");

        if (!figureClicked && isWhiteMove == clickedFigure.isWhite)
        {
            figureClicked = true;
            ChoosenFigure = clickedFigure.GameObject();

            Vector2 pos = clickedFigure.transform.position;
            int factor = clickedFigure.isWhite ? 1 : -1;

            if (Math.Abs(pos.y + factor) <= 3.5)
            {
                AddEmptyCell(pos.x, pos.y + factor);

                if (clickedFigure.isFirstTurn)
                {
                    AddEmptyCell(pos.x, pos.y + factor * 2);
                }
            }

            if (isCellOccupied(pos.x + 1, pos.y + factor) &&
               cellOccupied[(int)(pos.x + 1 + 3.5f), (int)(pos.y + factor + 3.5f)].GetComponent<Cell>().isWhite != clickedFigure.isWhite)
            {
                AddEmptyCell(pos.x + 1, pos.y + factor);
            }
            if (isCellOccupied(pos.x - 1, pos.y + factor) &&
                cellOccupied[(int)(pos.x - 1 + 3.5f), (int)(pos.y + factor + 3.5f)].GetComponent<Cell>().isWhite != clickedFigure.isWhite)
            {
                AddEmptyCell(pos.x - 1, pos.y + factor);
            }
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

    public void AddEmptyCell(float x, float y)
    {
        emptyCells.Add(Instantiate(emptyCell, new Vector3(x, y, -0.9f), Quaternion.identity));
        emptyCells.Last().GetComponent<EmptyCell>().emptyCellClick += OnEmptyCellClicked;
    }

    private void DestroyEmptyCells()
    {
        figureClicked = false;
        ChoosenFigure = null;

        foreach (var cell in emptyCells)
        {
            Destroy(cell.gameObject);
        }
        emptyCells.Clear();
    }

    private void OnEmptyCellClicked(EmptyCell clickedCell, GameObject clickedFigure)
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
