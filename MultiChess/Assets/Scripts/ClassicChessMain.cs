using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClassicChessMain : MonoBehaviour
{
    private const byte _fieldSize = 8;

    public GameObject bgCell;
    public GameObject emptyCell;
    public King whiteKing;

    public static bool figureClicked;


    void Start()
    {
        //FigureClick += FigureClickHandle;

        figureClicked = false;

        for (int i = 0; i < _fieldSize; i++)
        {
            for (int j = 0; j < _fieldSize; j++)
            {                
                bgCell.GetComponent<SpriteRenderer>().color = (i + j + 2) % 2 == 0 ? Color.white : Color.black;
                bgCell.name = "bgCell [" + i + ';' + j + "]";

                Instantiate(bgCell, new Vector2(j - 3.5f, - i + 3.5f), Quaternion.identity);
            }
        }

        whiteKing.transform.position = new Vector3(4 - 3.5f, -7 + 3.5f, -1);

        if (whiteKing != null)
            whiteKing.FigureClick += OnKingClicked;
        else
            Debug.LogError("Необходимо назначить короля в ClassicChessMain!");
    }

    void Update()
    {
        if (figureClicked) 
        { 
        
        }
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

            Vector2 pos = clickedKing.transform.position;

            Instantiate(emptyCell, new Vector2(pos.x + 1, pos.y + 1), Quaternion.identity);
            Instantiate(emptyCell, new Vector2(pos.x + 1, pos.y - 1), Quaternion.identity);
            Instantiate(emptyCell, new Vector2(pos.x - 1, pos.y + 1), Quaternion.identity);
            Instantiate(emptyCell, new Vector2(pos.x - 1, pos.y - 1), Quaternion.identity);

            Instantiate(emptyCell, new Vector2(pos.x, pos.y + 1), Quaternion.identity);
            Instantiate(emptyCell, new Vector2(pos.x, pos.y - 1), Quaternion.identity);
            Instantiate(emptyCell, new Vector2(pos.x + 1, pos.y), Quaternion.identity);
            Instantiate(emptyCell, new Vector2(pos.x - 1, pos.y), Quaternion.identity);
        }


    }

    void FigureClickHandle()
    {
        Debug.Log("Shit");
    }
}
