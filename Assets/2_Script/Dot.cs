using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    public enum MoveType
    {
        None, Goal, Follow
    }

    public MoveType moveType;

    public DotType dotType = DotType.Top;
    public Color[] colors;
    private SpriteRenderer renderer;

    public float stopDistance = 0.1f;
    public Vector2Int pos;
    public Vector3 goalPos;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();

    }

    public bool IsMoving()
    {
        return moveType != MoveType.None;
    }

    private void OnEnable()
    {
        int rnd = Random.Range(0, 6);
        dotType = (DotType)rnd;
        renderer.color = colors[rnd];
    }

    public void Moving(Vector3 pos)
    {
        goalPos = pos;
        moveType = MoveType.Goal;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (moveType == MoveType.None) return;

        if (moveType == MoveType.Goal)
        {
            transform.position = Vector3.MoveTowards(transform.position, goalPos, Global.dotSwapSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, goalPos) < stopDistance)
                moveType = MoveType.None;
        }
    }
}
