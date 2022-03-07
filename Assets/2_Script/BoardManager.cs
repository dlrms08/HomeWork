using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public Vector2Int boardSize;

    public  Board boardPrefab;
    public List<Board> boards = new List<Board>();

    public Dot dotPrefab;
    public Transform dotsParent;
    public GameObject[,] allDots;

    void Start()
    {
        allDots = new GameObject[boardSize.x, boardSize.y];
        BoardInit();
    }

    private void Update()
    {
        
    }

    //보드 초기화 
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
                board.name = "(" + x + ", " + y + ")";
                boards.Add(board);

                var dot = Instantiate(dotPrefab, GetPosition(pos), Quaternion.identity);
                dot.transform.SetParent(dotsParent, false);
                dot.name = "(" + x + ", " + y + ")";
                dot.column = x;
                dot.row = y;
                allDots[x, y] = dot.gameObject;
            }
        }
    }

    //도트 초기화 
    void DotsInit()
    {
        for(int i = 0; i < boards.Count; i++)
        {
            
        }
    }

  

    //블록간 거리 계산 
    public Vector3 GetPosition(Vector2Int pos)
    {
        int x = pos.x;
        int y = pos.y;
        return new Vector3((x - (boardSize.x / 2)) * 1.775f, (y - (boardSize.y / 2)) * 0.975f, 0f);
    }

    //체크방향 기준 계산 
    public Vector2Int GetNeighbor(Vector2Int origin, Direction dir)
    {
        switch (dir)
        {
            case Direction.LeftUp:
                return new Vector2Int(origin.x - 1, origin.y + 1);
            case Direction.Up:
                return new Vector2Int(origin.x, origin.y + 2);
            case Direction.RightUp:
                return new Vector2Int(origin.x + 1, origin.y + 1);
            case Direction.LeftDown:
                return new Vector2Int(origin.x - 1, origin.y - 1);
            case Direction.Down:
                return new Vector2Int(origin.x, origin.y - 2);
            case Direction.RightDown:
                return new Vector2Int(origin.x + 1, origin.y - 1);
        }
        return Vector2Int.zero;
    }
}
