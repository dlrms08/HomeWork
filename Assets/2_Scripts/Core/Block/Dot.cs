using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    public Vector2Int pos;
    public DotType blockType = DotType.Top;
    public Color[] color;
    private SpriteRenderer spriteRenderer;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        int rnd = Random.Range(1, 7);
        spriteRenderer.color = color[rnd];
        blockType = (DotType)rnd;
    }

}

public enum DotType
{
    Top = 0,
    Blue = 1,
    Green = 2,
    Orange = 3,
    Purple = 4,
    Red = 5,
    Yellow = 6,
}
