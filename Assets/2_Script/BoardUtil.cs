using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoardUtil
{
    //블록간 거리 계산 
    public static Vector3 GetPosition(Vector2Int pos)
    {
        int x = pos.x;
        int y = pos.y;
        return new Vector3((x - (BoardManager.intance.boardSize.x / 2)) * 1.775f, (y - (BoardManager.intance.boardSize.y / 2)) * 0.975f, 0f);
    }

    //체크방향 기준 계산 
    public static Vector2Int GetNeighbor(Vector2Int origin, Direction dir)
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
            case Direction.DoubleDown:
                return new Vector2Int(origin.x, origin.y - 4);
            case Direction.DoubleLeftUp:
                return new Vector2Int(origin.x - 2, origin.y + 2);
            case Direction.DoubleLeftDown:
                return new Vector2Int(origin.x - 2, origin.y - 2);
        }
        return Vector2Int.zero;
    }
}
