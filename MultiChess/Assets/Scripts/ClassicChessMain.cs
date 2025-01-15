using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClassicChessMain : MonoBehaviour
{
    private const byte _fieldSize = 8;

    public GameObject bgCell;

    void Start()
    {
        for (int i = 0; i < _fieldSize; i++)
        {
            for (int j = 0; j < _fieldSize; j++)
            {                
                bgCell.GetComponent<SpriteRenderer>().color = (i + j + 2) % 2 == 0 ? Color.white : Color.black;
                bgCell.name = "bgCell [" + i + ';' + j + "]";

                Instantiate(bgCell, new Vector2(j - 3.5f, - i + 3.5f), Quaternion.identity);
            }
        }
    }

    void Update()
    {
        
    }
}
