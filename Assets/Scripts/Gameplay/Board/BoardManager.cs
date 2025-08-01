using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Random = Unity.Mathematics.Random;


public class BoardManager : MonoBehaviour
{
    public static BoardManager instance;
    public GameObject borderCellPrefab;
    public float cellSize = 1f;
    public int boardSize = 8;
    public List<SquareType> amountSquares = new List<SquareType>();
    public List<GameObject> squares = new List<GameObject>();
    [SerializeField] private SpawnPlayer spawnPlayer;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GenerateBorderBoard();
        spawnPlayer.Spawn(squares[0].transform.position);

    }
    public void GenerateBorderBoard()
    {
        Square square = gameObject.AddComponent<Square>();
        Shuffle(amountSquares);

        for (int x = 0; x < (boardSize-1)*4; x++)
        {
            if (x <= boardSize-1)
            {
                Vector3 pos = new Vector3(0 * cellSize -3.5f, x * cellSize - 3.5f, 0f);
                square.position = pos;

                if (x == 0)
                {
                    square.squareType = SquareType.Start;
                }
                else if (x == boardSize - 1)
                {
                    square.squareType = SquareType.ShopSquare;
                }
                else
                {
                    square.squareType = amountSquares[x-1];
                }
                
            }
            else if ((boardSize-1)< x && x <= (boardSize-1)*2)
            {
                Vector3 pos = new Vector3((x-boardSize+1)* cellSize -3.5f, (boardSize -1)  * cellSize - 3.5f, 0f);
                square.position = pos;
                if (x == (boardSize - 1) * 2)
                {
                    square.squareType = SquareType.ShopSquare;
                }
                else
                {
                    square.squareType = amountSquares[x-boardSize];
                }
            }
            else if ((boardSize-1)*2 < x && x <= (boardSize-1)*3)
            {
                Vector3 pos = new Vector3((boardSize -1)* cellSize -3.5f, ((boardSize-1)*3-x)  * cellSize - 3.5f, 0f);
                square.position = pos;
                if (x == (boardSize - 1) * 3)
                {
                    square.squareType = SquareType.ShopSquare;
                }
                else
                {
                    square.squareType = amountSquares[x-boardSize*2+1];
                }
            }
            else if ( (boardSize-1)*3 < x && x < (boardSize-1)*4)
            {
                Vector3 pos = new Vector3( ((boardSize-1)*4-x) * cellSize -3.5f, 0  * cellSize - 3.5f, 0f);
                square.position = pos;
                square.squareType = amountSquares[x-boardSize*3+2];
            }
            
            GameObject cell = Instantiate(borderCellPrefab, square.position, Quaternion.identity, transform);
            SquareController controller = cell.gameObject.GetComponent<SquareController>();
            controller.Initialize(square);
            squares.Add(cell);


        }
    }

    public void DestroyBorderBoard()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }
    private void Shuffle<T>(IList<T> list)
    {
        var rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            int k = rng.Next(n--);
            T tmp = list[n];
            list[n] = list[k];
            list[k] = tmp;
        }
    }

    


}
