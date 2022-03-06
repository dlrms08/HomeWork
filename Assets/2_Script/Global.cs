using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global
{
    public const float dotSwapSpeed = 16f;
    public const float dotDropSpeed = 12f;
}

public enum DotType
{
    Red = 0,
    Yellow = 1,
    Blue = 2,
    Green = 3,
    Purple = 4,
    Orange = 5,
    Top = 6
}

public enum Direction
{
    None = 0,
    Up = 1,
    RightUp = 2,
    RightDown = 3,
    Down = 4,
    LeftDown = 5,
    LeftUp = 6,
    //이웃보다 한칸더 떨어진 방향 (각 요소를 제곱해서 더한후 곱하기10)
    //RightUpOffset = 50,
    //RightOffset = 130,
    //RightDownOffset = 250,
    //LeftDownOffset = 410,
    //LeftOffset = 610,
    //LeftUpOffset = 370,
}


