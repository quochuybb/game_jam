using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Random = Unity.Mathematics.Random;


public class BoardManager : MonoBehaviour
{
    public GameObject borderCellPrefab;
    public float cellSize = 1f;
    public int boardSize = 8;
    public List<SquareType> amountSquares = new List<SquareType>();

    private void Start()
    {
        GenerateBorderBoard();
    }
    public void GenerateBorderBoard()
    {
        Square square = new Square();
        for (int x = 0; x < boardSize; x++)
        {        
            Shuffle(amountSquares);

            for (int y = 0; y < boardSize; y++)
            {
                if (x == 0 || x == boardSize - 1 || y == 0 || y == boardSize - 1)
                {
                    Vector3 pos = new Vector3(x * cellSize, y * cellSize, 0f);
                    GameObject cell = Instantiate(borderCellPrefab, pos, Quaternion.identity, this.transform);
                    SquareController controller = cell.gameObject.GetComponent<SquareController>();
                    square.squareType = amountSquares[y];
                    controller.Initialize(square);
                    cell.name = $"Cell_{x}_{y}";
                }
            }
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
