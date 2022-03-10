using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MatchUtil
{
    //바로 밑의 Dot 체크 및 참조
    public static Dot DownDot(int column, int row)
    {
        Vector2Int v = BoardUtil.GetNeighbor(new Vector2Int(column, row), Direction.Down);

        if (column == 0 || column == 6)
        {
            if (row == 5 || row == 7)
            {
                GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                if(obj != null)return obj.GetComponent<Dot>();
            }
        }
        if (column == 1 || column == 5)
        {
            if (row == 4 || row == 6 || row == 8)
            {
                GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                if (obj != null) return obj.GetComponent<Dot>();
            }
        }
        if (column == 2 || column == 4)
        {
            if (row == 3 || row == 5 || row == 7 || row == 9)
            {
                GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                if (obj != null) return obj.GetComponent<Dot>();
            }
        }
        else if(column == 3)
        {
            if (row == 4 || row == 6 || row == 8 || row == 10)
            {
                GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                if (obj != null) return obj.GetComponent<Dot>();
            }
        }
        return null;
    }

    //바로 위의 Dot 체크 및 참조
    public static Dot UpDot(int column, int row)
    {
        Vector2Int v = BoardUtil.GetNeighbor(new Vector2Int(column, row), Direction.Up);

        if (column == 0 || column == 6)
        {
            if(row == 3 || row == 5)
            {
                GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                if (obj != null) return obj.GetComponent<Dot>();
            }
        }
        if (column == 1 || column == 5)
        {
            if (row == 2 || row == 4 || row == 6)
            {
                GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                if (obj != null) return obj.GetComponent<Dot>();
            }
        }
        if (column == 2 || column == 4)
        {
            if (row == 1 || row == 3 || row == 5 || row == 7)
            {
                GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                if (obj != null) return obj.GetComponent<Dot>();
            }
        }
        else if(column == 3)
        {
            if (row == 2|| row == 4 || row == 6 || row == 8)
            {
                GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                if (obj != null) return obj.GetComponent<Dot>();
            }
        }
        return null;
    }

    //우측 위의 Dot 체크 및 참조
    public static Dot RightUpDot(int column, int row)
    {
        Vector2Int v = BoardUtil.GetNeighbor(new Vector2Int(column, row), Direction.RightUp);

        switch (column)
        {
            case 0:
                if ((row == 7 || row == 5 || row == 3))
                {
                    GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                    if (obj != null) return obj.GetComponent<Dot>();
                }
                break;
            case 1:
                if (row == 2 || row == 4 || row == 6 || row == 8)
                {
                    GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                    if (obj != null) return obj.GetComponent<Dot>();
                }
                break;
            case 2:
                if (row == 1 || row == 3 || row == 5 || row == 7 || row == 9)
                {
                    GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                    if (obj != null) return obj.GetComponent<Dot>();
                }
                break;
            case 3:
                if (row == 0 || row == 2 || row == 4 || row == 6 || row == 8)
                {
                    GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                    if (obj != null) return obj.GetComponent<Dot>();
                }
                break;
            case 4:
                if (row == 1 || row == 3 || row == 5 || row == 7)
                {
                    GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                    if (obj != null) return obj.GetComponent<Dot>();
                }
                break;
            case 5:
                if (row == 2 || row == 4 || row == 6)
                {
                    GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                    if (obj != null) return obj.GetComponent<Dot>();
                }
                break;
            case 6:
                break;
        }

        return null;

    }

    //좌측 아래의 Dot 체크 및 참조
    public static  Dot LeftDownDot(int column, int row)
    {
        Vector2Int v = BoardUtil.GetNeighbor(new Vector2Int(column, row), Direction.LeftDown);

        switch (column)
        {
            case 0:
                break;
            case 1:
                if (row == 4 || row == 6 || row == 8)
                {
                    GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                    if (obj != null) return obj.GetComponent<Dot>();
                }
                break;
            case 2:
                if (row == 3 || row == 5 || row == 7 || row == 9)
                {
                    GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                    if (obj != null) return obj.GetComponent<Dot>();
                }
                break;
            case 3:
                if (row == 2 || row == 4 || row == 6 || row == 8 || row == 10)
                {
                    GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                    if (obj != null) return obj.GetComponent<Dot>();
                }
                break;
            case 4:
                if (row == 1 || row == 3 || row == 5 || row == 7 || row == 9)
                {
                    GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                    if (obj != null) return obj.GetComponent<Dot>();
                }
                break;
            case 5:
                if (row == 2 || row == 4 || row == 6 || row == 8)
                {
                    GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                    if (obj != null) return obj.GetComponent<Dot>();
                }
                break;
            case 6:
                if (row == 3 || row == 5 || row == 7)
                {
                    GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                    if (obj != null) return obj.GetComponent<Dot>();
                }
                break;
        }

        return null;
    }

    //우측 아래의 Dot 체크 및 참조
    public static Dot RightDownDot(int column, int row)
    {
        Vector2Int v = BoardUtil.GetNeighbor(new Vector2Int(column, row), Direction.RightDown);

        switch (column)
        {
            case 0:
                if (row == 4 || row == 5 || row == 7)
                {
                    GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                    if (obj != null) return obj.GetComponent<Dot>();
                }
                break;
            case 1:
                if (row == 2 || row == 4 || row == 6 || row == 8)
                {
                    GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                    if (obj != null) return obj.GetComponent<Dot>();
                }
                break;
            case 2:
                if (row == 1 || row == 3 || row == 5 || row == 7 || row == 9)
                {
                    GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                    if (obj != null) return obj.GetComponent<Dot>();
                }
                break;
            case 3:
                if (row == 2 || row == 4 || row == 6 || row == 8 || row == 10)
                {
                    GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                    if (obj != null) return obj.GetComponent<Dot>();
                }
                break;
            case 4:
                if (row == 3 || row == 5 || row == 7 || row == 9)
                {
                    GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                    if (obj != null) return obj.GetComponent<Dot>();
                }
                break;
            case 5:
                if (row == 4 || row == 6 || row == 8)
                {
                    GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                    if (obj != null) return obj.GetComponent<Dot>();
                }
                break;
            case 6:
                return null;
        }

        return null;
    }

    //우측 위의 Dot 체크 및 참조
    public static Dot LeftUpDot(int column, int row)
    {
        Vector2Int v = BoardUtil.GetNeighbor(new Vector2Int(column, row), Direction.LeftUp);

        switch (column)
        {
            case 0:
                return null;
            case 1:
                if (row == 2 || row == 4 || row == 6)
                {
                    GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                    if (obj != null) return obj.GetComponent<Dot>();
                }
                break;
            case 2:
                if (row == 1 || row == 3 || row == 5 || row == 7)
                {
                    GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                    if (obj != null) return obj.GetComponent<Dot>();
                }
                break;
            case 3:
                if (row == 0 || row == 2 || row == 4 || row == 6 || row == 8)
                {
                    GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                    if (obj != null) return obj.GetComponent<Dot>();
                }
                break;
            case 4:
                if (row == 1 || row == 3 || row == 5 || row == 7 || row == 9)
                {
                    GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                    if (obj != null) return obj.GetComponent<Dot>();
                }
                break;
            case 5:
                if (row == 2 || row == 4 || row == 6 || row == 8)
                {
                    GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                    if (obj != null) return obj.GetComponent<Dot>();
                }
                break;
            case 6:
                if (row == 3 || row == 5 || row == 7)
                {
                    GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                    if (obj != null) return obj.GetComponent<Dot>();
                }
                break;
        }

        return null;
    }

    public static Dot DoubleDownDot(int column, int row)
    {
        Vector2Int v = BoardUtil.GetNeighbor(new Vector2Int(column, row), Direction.DoubleDown);

        if (column == 0 || column == 6)
        {
            if (row == 7)
            {
                GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                if (obj != null) return obj.GetComponent<Dot>();
            }
        }
        if (column == 1 || column == 5)
        {
            if (row == 6 || row == 8)
            {
                GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                if (obj != null) return obj.GetComponent<Dot>();
            }
        }
        if (column == 2 || column == 4)
        {
            if (row == 5 || row == 7 || row == 9)
            {
                GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                if (obj != null) return obj.GetComponent<Dot>();
            }
        }
        else if(column == 3)
        {
            if (row == 4 || row == 6 || row == 8 || row == 10)
            {
                GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                if (obj != null) return obj.GetComponent<Dot>();
            }
        }
        return null;
    }

    public static Dot DoubleLeftDownDot(int column, int row)
    {
        Vector2Int v = BoardUtil.GetNeighbor(new Vector2Int(column, row), Direction.DoubleLeftDown);

        switch (column)
        {
            case 0:
                return null;
            case 1:
                return null;
            case 2:
                if(row == 5 || row == 7 || row == 9)
                {
                    GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                    if (obj != null) return obj.GetComponent<Dot>();
                }
                    
                break;
            case 3:
                if (row == 4 || row == 6 || row == 8 || row == 10)
                {
                    GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                    if (obj != null) return obj.GetComponent<Dot>();
                }
                break;
            case 4:
                if (row == 3 || row == 5 || row == 7 || row == 9)
                {
                    GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                    if (obj != null) return obj.GetComponent<Dot>();
                }
                break;
            case 5:
                if (row == 2 || row == 4 || row == 6 || row == 8)
                {
                    GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                    if (obj != null) return obj.GetComponent<Dot>();
                }
                break;
            case 6:
                if (row == 3 || row == 5 || row == 7)
                {
                    GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                    if (obj != null) return obj.GetComponent<Dot>();
                }
                break;
        }

        return null;
    }

    //우측 위의 Dot 체크 및 참조
    public static Dot DoubleLeftUpDot(int column, int row)
    {
        Vector2Int v = BoardUtil.GetNeighbor(new Vector2Int(column, row), Direction.DoubleLeftUp);
        switch (column)
        {
            case 0:
                return null;
            case 1:
                return null;
            case 2:
                if (row == 1 || row == 3 || row == 5)
                {
                    GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                    if (obj != null) return obj.GetComponent<Dot>();
                }
                break;
            case 3:
                if (row == 0 || row == 2 || row == 4 || row == 6)
                {
                    GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                    if (obj != null) return obj.GetComponent<Dot>();
                }
                break;
            case 4:
                if (row == 1 || row == 3 || row == 5 || row == 7)
                {
                    GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                    if (obj != null) return obj.GetComponent<Dot>();
                }
                break;
            case 5:
                if (row == 2 || row == 4 || row == 6 || row == 8)
                {
                    GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                    if (obj != null) return obj.GetComponent<Dot>();
                }
                break;
            case 6:
                if (row == 3 || row == 5 || row == 7)
                {
                    GameObject obj = BoardManager.intance.allDots[v.x, v.y];
                    if (obj != null) return obj.GetComponent<Dot>();
                }
                break;
        }

        return null;
    }
}
