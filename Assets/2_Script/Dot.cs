using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{

    public DotType dotType = DotType.Top;
    public Color[] colors;
    private SpriteRenderer renderer;

    public int column;
    public int row;
    public float targetX;
    public float targetY;
    private GameObject otherDot;
    private BoardManager boardManager; 
    private Vector2 firstTouchPosition;
    private Vector2 finaltouchPosition;
    private Vector2 tempPosition;
    public float swipeAngle = 0;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        boardManager = FindObjectOfType<BoardManager>();
        Vector3 realpos = boardManager.GetPosition(new Vector2Int(column, row));
        targetX = realpos.x;
        targetY = realpos.y;
    }

    private void OnEnable()
    {
        int rnd = Random.Range(0, 6);
        dotType = (DotType)rnd;
        renderer.color = colors[rnd];
    }

    private void Update()
    {
        Vector3 realpos = boardManager.GetPosition(new Vector2Int(column, row));

        targetX = realpos.x;
        targetY = realpos.y;
        
        if(Mathf.Abs(targetX - transform.position.x) > .1)
        {
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);
        }
        else
        {
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = tempPosition;
            boardManager.allDots[column, row] = this.gameObject;
        }

        if (Mathf.Abs(targetY - transform.position.y) > .1)
        {
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);
        }
        else
        {
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = tempPosition;
            boardManager.allDots[column, row] = this.gameObject;
        }
    }

    private void OnMouseDown()
    {
        firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        finaltouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CalculateAngle();
    }

    //드래그 앵글 계산.
    private void CalculateAngle()
    {
        swipeAngle = Mathf.Atan2(finaltouchPosition.y - firstTouchPosition.y, finaltouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
        MovePices();
    }

    void MovePices()
    {
        if (swipeAngle > 0 && swipeAngle <= 60 && column < boardManager.boardSize.x && row < boardManager.boardSize.y)
        {
            Debug.Log("오른쪽 위");
            otherDot = boardManager.allDots[column + 1, row + 1];
            otherDot.GetComponent<Dot>().column -= 1;
            otherDot.GetComponent<Dot>().row -= 1;
            column += 1;
            row += 1;
        }
        else if (swipeAngle > 60 && swipeAngle <= 120 && row < boardManager.boardSize.y)
        {
            Debug.Log("위");
            otherDot = boardManager.allDots[column, row + 2];
            otherDot.GetComponent<Dot>().row -= 2;
            row += 2;
        }
        else if (swipeAngle > 120 && swipeAngle < 180 && column > 0 && row < boardManager.boardSize.y)
        {
            Debug.Log("왼쪽 위");
            otherDot = boardManager.allDots[column - 1, row + 1];
            otherDot.GetComponent<Dot>().column += 1;
            otherDot.GetComponent<Dot>().row -= 1;
            column -= 1;
            row += 1;
        }
        else if (swipeAngle > -180 && swipeAngle <= -120 && column > 0 && row > 0)
        {
            Debug.Log("왼쪽 아래");
            otherDot = boardManager.allDots[column - 1, row - 1];
            otherDot.GetComponent<Dot>().column += 1;
            otherDot.GetComponent<Dot>().row += 1;
            column -= 1;
            row -= 1;
        }
        else if (swipeAngle > -120 && swipeAngle <= -60 && row > 0)
        {
            Debug.Log("아래");
            otherDot = boardManager.allDots[column, row - 2];
            otherDot.GetComponent<Dot>().row += 2;
            row -= 2;
        }
        else if(swipeAngle > -60 && swipeAngle < 0 && column < boardManager.boardSize.x && row > 0)
        {
            Debug.Log("오른쪽 아래");
            otherDot = boardManager.allDots[column + 1, row - 1];
            otherDot.GetComponent<Dot>().column -= 1;
            otherDot.GetComponent<Dot>().row += 1;
            column += 1;
            row -= 1;
        }
    }
}
