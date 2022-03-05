using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public Vector2Int boardSize;

    public  Board boardPrefab;
    public List<Board> boards = new List<Board>();

    public Dot dotPrefab;
    public Transform dotsParent;
    public List<Dot> dots = new List<Dot>();

    void Start()
    {
        BoardInit();
        DotsInit();
    }

    void BoardInit()
    {
        for(int x = 0; x < boardSize.x; x++)
        {
            for(int y = 0; y < boardSize.y; y++)
            {
                if ((x + y) % 2 == 0)
                {
                    continue;
                }

                if ((x == 0 && y == 1) || (x == 0 && y == boardSize.y - 2) || (x == 1 && y == 0) || (x == 1 && y == boardSize.y - 1))
                    continue;

                if ((x == boardSize.x - 1 && y == 1) || (x == boardSize.x - 1 && y == boardSize.y - 2) || (x == boardSize.x - 2 && y == 0) || (x == boardSize.x - 2 && y == boardSize.y - 1))
                    continue;

                var pos = new Vector2Int(x, y);
                var board = Instantiate(boardPrefab);
                board.pos = pos;
                board.transform.SetParent(transform, false);
                board.transform.position = GetPosition(pos);
                boards.Add(board);
            }
        }
    }


    void DotsInit()
    {
        for(int i = 0; i < boards.Count; i++)
        {
            var dot = Instantiate(dotPrefab);
            dot.pos = boards[i].pos;
            dot.transform.SetParent(dotsParent, false);
            dot.transform.position = boards[i].transform.position;
            dots.Add(dot);
        }
    }


    public Vector3 GetPosition(Vector2Int pos)
    {
        int x = pos.x;
        int y = pos.y;
        return new Vector3((x - (boardSize.x / 2)) * 1.775f, (y - (boardSize.y / 2)) * 0.975f, 0f);
    }

}
