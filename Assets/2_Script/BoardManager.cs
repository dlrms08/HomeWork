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
    public List<Dot> dots = new List<Dot>();
    public List<Dot> matchDots = new List<Dot>();
    List<Dot> matchBank = new List<Dot>();

    void Start()
    {
        BoardInit();
        while(true)
        {
            DotsInit();
            if (matchBank.Count == 0) break;
            ClearBoard();
        }
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
        if (matchDots.Count > 0)
            ClearBoard();
    }

    //보드 삭제 
    void ClearBoard()
    {
        for(int i = 0; i < dots.Count ; i++)
        {
            Destroy(dots[i].gameObject);
        }
        dots.Clear();
        matchDots.Clear();
        matchBank.Clear();

        DotsInit();
    }

    //매치 클리어
    public void ClearMatch()
    {
        foreach(var dot in matchDots)
        {
            dot.gameObject.SetActive(false);
        }
        matchDots.Clear();
        matchBank.Clear();
    }

    //라인 매치확인
    public void CheckMatch()
    {
        foreach(var dot in dots)
        {
            LineMatch(dot);
        }
        matchDots = matchBank.Distinct().ToList();
    }

    //직선매치 확인
    public void LineMatch(Dot dot)
    {
        foreach (var it in Enum.GetValues(typeof(Direction)))
        {
            Direction dir = (Direction)it;
            if (dir == Direction.None)
                continue;
            
            var neiver = FindDotObject(dot, dir);
            if (neiver == null)
                continue;

            if (dot.dotType == neiver.dotType)
            {
                var neiver2 = FindDotObject(neiver, dir);
                if (neiver2 == null)
                    return;

                if (neiver.dotType == neiver2.dotType)
                {
                    Debug.Log(dot.pos + " 와(과) " + neiver.pos + " 와(과) " + neiver2.pos + " 는 매치");
                    matchBank.Add(dot);
                    matchBank.Add(neiver);
                    matchBank.Add(neiver2);
                }
            }
        }
    }

    //라인 매치된 블록 찾기
    Dot FindDotObject(Dot origin, Direction dir)
    {
        for(int i = 0; i < dots.Count; i++)
        {
            Vector2Int pos = GetNeighbor(origin.pos, dir);
            if (pos == null)
                continue;

            if (dots[i].pos == pos)
            {
                return dots[i];
            }
        }
        return null;
    }

    //보드의 빈 공간 찾기 
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

    public Direction GetDirection(Vector3 startPos, Vector3 endPos)
    {
        float degree = GetDirectionDegree(startPos, endPos);
        if (degree <= 60f) return Direction.RightUp;
        if (degree <= 120f) return Direction.Up;
        if (degree <= 180f) return Direction.LeftUp;
        if (degree <= 240f) return Direction.LeftDown;
        if (degree <= 300f) return Direction.Down;
        return Direction.RightDown;
    }

    public static float GetDirectionDegree(Vector3 origin, Vector3 lookAt)
    {
        Vector3 direction = lookAt - origin;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (angle < 0f) angle += 360f;
        return angle;
    }
}
