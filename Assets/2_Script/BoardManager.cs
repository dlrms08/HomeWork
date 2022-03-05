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
    public List<Dot> matchDots = new List<Dot>();

    void Start()
    {
        BoardInit();
        DotsInit();
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
                boards.Add(board);
            }
        }
    }

    //도트 초기화 
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

        CheckMatch();
    }

    void CheckMatch()
    {
        for(int i = 0; i < dots.Count; i++)
        {
            Dot target = FindDotObject(dots[i]);
            if (target == null)
                continue;

            if (dots[i].dotType == FindDotObject(dots[i]).dotType)
            {
                Debug.Log(dots[i].pos + " 와(과) " + FindDotObject(dots[i]).pos + " 는 매치!");
            }
        }
    }

    Dot FindDotObject(Dot origin)
    {
        for(int i = 0; i < dots.Count; i++)
        {
            Vector2Int pos = GetNeighbor(origin.pos, Direction.RightUp);
            if (pos == null)
                continue;

            if (dots[i].pos == pos)
            {
                return dots[i];
            }
        }
        return null;
    }


    void CheckBlink()
    {
        for(int i = 0; i < boards.Count; i++)
        {
            if(!dots[i].gameObject.activeSelf)
            {
                Debug.Log(dots[i].pos);
            }
        }
    }


    public Vector3 GetPosition(Vector2Int pos)
    {
        int x = pos.x;
        int y = pos.y;
        return new Vector3((x - (boardSize.x / 2)) * 1.775f, (y - (boardSize.y / 2)) * 0.975f, 0f);
    }

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
            case Direction.RightUpOffset:
                return new Vector2Int(origin.x + 1, origin.y + 3);
            case Direction.RightOffset:
                return new Vector2Int(origin.x + 2, origin.y);
            case Direction.RightDownOffset:
                return new Vector2Int(origin.x + 1, origin.y - 3);
            case Direction.LeftDownOffset:
                return new Vector2Int(origin.x - 1, origin.y - 3);
            case Direction.LeftOffset:
                return new Vector2Int(origin.x - 2, origin.y);
            case Direction.LeftUpOffset:
                return new Vector2Int(origin.x - 1, origin.y + 3);
        }
        return Vector2Int.zero;
    }

}
