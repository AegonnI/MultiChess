using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class ClassicChessMain : MonoBehaviour
{
    public byte _fieldSize = 8;
    public byte _scale = 1;
    public static float border;
    public static float scale;
    public static string SkinPath;
    public static float scalingFactor;

    public GameObject bgCell;
    public GameObject emptyCell;
    private static GameObject MoveCell;

    //White
    public Sprite whiteKingSprite;

    public Vector2[] whiteKingsCoords;
    public King whiteKing;
    public Vector2[] whiteQueensCoords;
    public Queen whiteQueen;

    public Vector2[] whiteRooksCoords;
    public Rook whiteRook;

    public Vector2[] whiteKnightsCoords;
    public Knight whiteKnight;

    public Vector2[] whiteBishopsCoords;
    public Bishop whiteBishop;

    public Vector2[] whitePawnsCoords;
    public Pawn whitePawn;

    //Black

    public Vector2[] blackKingsCoords;
    public King blackKing;
    public Vector2[] blackQueensCoords;
    public Queen blackQueen;

    public Vector2[] blackRooksCoords;
    public Rook blackRook;

    public Vector2[] blackKnightsCoords;
    public Knight blackKnight;

    public Vector2[] blackBishopsCoords;
    public Bishop blackBishop;

    public Vector2[] blackPawnsCoords;
    public Pawn blackPawn;
    //

    public Champion champion;
    private List<GameObject> figures2;

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
        border = 0.5f * (_fieldSize - 1);
        scale = _scale;
        SkinPath = "PixelClassic";
        scalingFactor = scale / 0.16f;

        cellOccupied = new GameObject[_fieldSize, _fieldSize];
        for (int i = 0; i < cellOccupied.GetLength(0); i++)
        {
            for (int j = 0; j < cellOccupied.GetLength(0); j++)
            {
                cellOccupied[i,j] = null;
            }
        }

        figures = new List<GameObject>();

        //figures2 = new List<GameObject>();
        figures.Add(Instantiate(champion.gameObject, new Vector3(1f / 2, -1f / 2, -1), Quaternion.identity));
        figures.Last().GetComponent<Champion>().FigureClick += OnFigureClick;

        SpawnFigure(whiteKing, whiteKingsCoords);
        SpawnFigure(whiteQueen, whiteQueensCoords);
        SpawnFigure(whiteRook, whiteRooksCoords);
        SpawnFigure(whiteKnight, whiteKnightsCoords);
        SpawnFigure(whiteBishop, whiteBishopsCoords);
        SpawnFigure(whitePawn, whitePawnsCoords);

        SpawnFigure(blackKing, blackKingsCoords);
        SpawnFigure(blackQueen, blackQueensCoords);
        SpawnFigure(blackRook, blackRooksCoords);
        SpawnFigure(blackKnight, blackKnightsCoords);
        SpawnFigure(blackBishop, blackBishopsCoords);
        SpawnFigure(blackPawn, blackPawnsCoords);

        foreach (GameObject figure in figures)
        {
            FillcellOccupied(figure);
        }

        bgCell.transform.localScale = new Vector2(scale, scale);
        for (int i = 0; i < _fieldSize; i++)
        {
            for (int j = 0; j < _fieldSize; j++)
            {                
                bgCell.GetComponent<SpriteRenderer>().color = (i + j + 2) % 2 == 0 ? new Color(0.9f, 0.9f, 0.8f, 1f) : new Color(0.27f, 0.3f, 0.37f, 1f);
                bgCell.name = "bgCell [" + i + ';' + j + "]";

                Instantiate(bgCell, new Vector2(scale * (j - border), scale * (- i + border)), Quaternion.identity);
            }
        }
    }

    private void SpawnFigure<T>(T figure, Vector2[] coords) where T : IFigure<T>, IGameObjectProvider
    {
        for (int i = 0; i < coords.Length; i++)
        {
            figures.Add(Instantiate(figure.GetGameObject(), LeftBottonCoords(coords[i].x, coords[i].y), Quaternion.identity));
            figures.Last().GetComponent<T>().FigureClick += OnFigureClick;
        }
    }

    private Vector3 LeftBottonCoords(float x, float y)
    {
        return new Vector3(_scale * (x - 1 - border), _scale * (y - 1 - border), -1);
    }

    private static Vector2 GetCoords(float x, float y)
    {
        return new Vector2((x/scale) + border, -(y/scale) + border);
    }

    public static GameObject GetAnOccupier(float x, float y)
    {
        return cellOccupied[(int)GetCoords(x, y).x, (int)GetCoords(x, y).y];
    }

    public static void SetAnOccupier(float x, float y, GameObject newOccupier)
    {
        cellOccupied[(int)GetCoords(x, y).x, (int)GetCoords(x, y).y] = newOccupier;
    }

    private void FillcellOccupied(GameObject gameObject)
    {
        SetAnOccupier(gameObject.transform.position.x, gameObject.transform.position.y, gameObject);
    }

    public static bool isCellOccupied(float x, float y)
    {
        return GetAnOccupier(x, y) != null;
    }

    public static bool IsOpponentOnTheCell(float x, float y, bool isWhite)
    {
        return isCellOccupied(x, y) && GetAnOccupier(x, y).GetComponent<Cell>().isWhite != isWhite;
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
            whiteKing.GetComponent<King>().FigureClick -= OnFigureClick;
    }

    private void OnFigureClick<T>(T clickedFigure, Action<Vector2, bool> action) where T : IFigure<T>, IGameObjectProvider
    {
        //Debug.Log($"Клик на: {clickedFigure.gameObject.name}");

        if (!figureClicked && isWhiteMove == clickedFigure.IsWhite)
        {
            figureClicked = true;
            ChoosenFigure = clickedFigure.GetGameObject();

            action(clickedFigure.GetGameObject().transform.position, clickedFigure.IsWhite);
        }
        else
        {
            if (ChoosenFigure != null && ChoosenFigure.GetComponent<Cell>().isWhite != clickedFigure.IsWhite)
            {
                KillFigure(clickedFigure.GetGameObject());
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
                SetAnOccupier(figurePos.x, figurePos.y, null);
                Destroy(clickedFigure.gameObject);

                MoveTheFigure(ChoosenFigure, emptyCellPos.x, emptyCellPos.y);
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

    private static void MoveTheFigure(GameObject figure, float newX, float newY)
    {
        SetAnOccupier(figure.transform.position.x, figure.transform.position.y, null);

        figure.transform.position = new Vector3(newX, newY, -1);
        
        SetAnOccupier(figure.transform.position.x, figure.transform.position.y, figure);
    }

    private static void OnEmptyCellClicked(EmptyCell clickedCell, GameObject clickedFigure)
    {
        //Debug.Log($"Клик на клетку для хода: {clickedCell.gameObject.name}");

        MoveTheFigure(clickedFigure, clickedCell.transform.position.x, clickedCell.transform.position.y);
        isWhiteMove = !isWhiteMove;

        if (clickedFigure.GetComponent<Pawn>())
        {
            clickedFigure.GetComponent<Pawn>().isFirstTurn = false;
        }

        DestroyEmptyCells();
    }
}