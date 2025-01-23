using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

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

    public static GameObject ChoosenFigure;
    public static bool figureClicked;


    void Start()
    {
        figureClicked = false;
        emptyCells = new List<GameObject>();
        ChoosenFigure = null;

        for (int i = 0; i < _fieldSize; i++)
        {
            for (int j = 0; j < _fieldSize; j++)
            {                
                bgCell.GetComponent<SpriteRenderer>().color = (i + j + 2) % 2 == 0 ? Color.white : Color.black;
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

        //black
        AppointFigure(blackKing, () => blackKing.FigureClick += OnKingClicked);
        AppointFigure(blackQueen, () => blackQueen.FigureClick += OnQueenClicked);

        AppointFigure(blackRook1, () => blackRook1.FigureClick += OnRookClicked);
        AppointFigure(blackRook2, () => blackRook2.FigureClick += OnRookClicked);

        AppointFigure(blackKnight1, () => blackKnight1.FigureClick += OnKnightClicked);
        AppointFigure(blackKnight2, () => blackKnight2.FigureClick += OnKnightClicked);

        AppointFigure(blackBishop1, () => blackBishop1.FigureClick += OnBishopClicked);
        AppointFigure(blackBishop2, () => blackBishop2.FigureClick += OnBishopClicked);
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

    private void OnKingClicked(King clickedKing)
    {
        Debug.Log($"Клик на короля: {clickedKing.gameObject.name}");

        if (!figureClicked)
        {
            figureClicked = true;
            ChoosenFigure = clickedKing.GameObject();

            Vector2 pos = clickedKing.transform.position;

            float x = pos.x + 1;
            float y = pos.y + 1;
            float dx = 0;
            float dy = -1;

            for (int i = 0; i < 8; i++)
            {
                if (Math.Abs(x) <= 3.5 && Math.Abs(y) <= 3.5)
                {
                    emptyCells.Add(Instantiate(emptyCell, new Vector3(x, y, -1), Quaternion.identity));
                    emptyCells.Last().GetComponent<EmptyCell>().emptyCellClick += OnEmptyCellClicked;
                }

                if (x + dx > pos.x + 1 || x + dx < pos.x - 1 || y + dy > pos.y + 1 || y + dy < pos.y - 1)
                {
                    (dx, dy) = (dy, -dx);
                }
                x += dx;
                y += dy;
            }
        }
        else
        {
            DestroyEmptyCells();
        }
    }

    private void OnQueenClicked(Queen clickedQueen)
    {
        Debug.Log($"Клик на ферзя: {clickedQueen.gameObject.name}");

        if (!figureClicked)
        {
            figureClicked = true;
            ChoosenFigure = clickedQueen.GameObject();

            Vector2 pos = clickedQueen.transform.position;

            for (float i = -3.5f; i <= 3.5f; i += 1)
            {
                if (i != pos.x)
                {
                    emptyCells.Add(Instantiate(emptyCell, new Vector3(i, pos.y, -1), Quaternion.identity));
                    emptyCells.Last().GetComponent<EmptyCell>().emptyCellClick += OnEmptyCellClicked;
                }
                if (i != pos.y)
                {
                    emptyCells.Add(Instantiate(emptyCell, new Vector3(pos.x, i, -1), Quaternion.identity));
                    emptyCells.Last().GetComponent<EmptyCell>().emptyCellClick += OnEmptyCellClicked;
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
                        emptyCells.Add(Instantiate(emptyCell, new Vector3(x, y, -1), Quaternion.identity));
                        emptyCells.Last().GetComponent<EmptyCell>().emptyCellClick += OnEmptyCellClicked;
                    }

                    if (uy != pos.y && Math.Abs(uy) <= 3.5)
                    {
                        emptyCells.Add(Instantiate(emptyCell, new Vector3(x, uy, -1), Quaternion.identity));
                        emptyCells.Last().GetComponent<EmptyCell>().emptyCellClick += OnEmptyCellClicked;
                    }
                }
            }
        }
        else
        {
            DestroyEmptyCells();
        }
    }

    private void OnRookClicked(Rook clickedFigure)
    {
        Debug.Log($"Клик на ладью: {clickedFigure.gameObject.name}");

        if (!figureClicked)
        {
            figureClicked = true;
            ChoosenFigure = clickedFigure.GameObject();

            Vector2 pos = clickedFigure.transform.position;

            for (float i = -3.5f; i <= 3.5f; i += 1)
            {
                if (i != pos.x)
                {
                    emptyCells.Add(Instantiate(emptyCell, new Vector3(i, pos.y, -1), Quaternion.identity));
                    emptyCells.Last().GetComponent<EmptyCell>().emptyCellClick += OnEmptyCellClicked;
                }
                if (i != pos.y)
                {
                    emptyCells.Add(Instantiate(emptyCell, new Vector3(pos.x, i, -1), Quaternion.identity));
                    emptyCells.Last().GetComponent<EmptyCell>().emptyCellClick += OnEmptyCellClicked;
                }
            }
        }
        else
        {
            DestroyEmptyCells();
        }
    }

    private void OnKnightClicked(Knight clickedFigure)
    {
        Debug.Log($"Клик на коня: {clickedFigure.gameObject.name}");

        if (!figureClicked)
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
                        emptyCells.Add(Instantiate(emptyCell, new Vector3(i * 1 + pos.x, j * 2 + pos.y, -1), Quaternion.identity));
                        emptyCells.Last().GetComponent<EmptyCell>().emptyCellClick += OnEmptyCellClicked;
                    }

                    if (Math.Abs(i * 2 + pos.x) <= 3.5 && Math.Abs(j * 1 + pos.y) <= 3.5)
                    {
                        emptyCells.Add(Instantiate(emptyCell, new Vector3(i * 2 + pos.x, j * 1 + pos.y, -1), Quaternion.identity));
                        emptyCells.Last().GetComponent<EmptyCell>().emptyCellClick += OnEmptyCellClicked;
                    }
                }
            }
        }
        else
        {
            DestroyEmptyCells();
        }
    }

    private void OnBishopClicked(Bishop clickedFigure)
    {
        Debug.Log($"Клик на ферзя: {clickedFigure.gameObject.name}");

        if (!figureClicked)
        {
            figureClicked = true;
            ChoosenFigure = clickedFigure.GameObject();

            Vector2 pos = clickedFigure.transform.position;

            for (float x = -3.5f; x <= 3.5f; x += 1)
            {
                float y = x - pos.x + pos.y;
                float uy = -x + pos.x + pos.y;

                if (Math.Abs(x) <= 3.5 && x != pos.x)
                {
                    if (y != pos.y && Math.Abs(y) <= 3.5)
                    {
                        emptyCells.Add(Instantiate(emptyCell, new Vector3(x, y, -1), Quaternion.identity));
                        emptyCells.Last().GetComponent<EmptyCell>().emptyCellClick += OnEmptyCellClicked;
                    }

                    if (uy != pos.y && Math.Abs(uy) <= 3.5)
                    {
                        emptyCells.Add(Instantiate(emptyCell, new Vector3(x, uy, -1), Quaternion.identity));
                        emptyCells.Last().GetComponent<EmptyCell>().emptyCellClick += OnEmptyCellClicked;
                    }
                }
            }
        }
        else
        {
            DestroyEmptyCells();
        }
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

        clickedFigure.transform.position = clickedCell.transform.position;
        DestroyEmptyCells();
    }
}
