using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{

    public DotType dotType = DotType.Top;
    public Color[] colors;

    public int column;
    public int row;
    public float targetX;
    public float targetY;
    public bool isMatched = false;
    private GameObject otherDot;
    private BoardManager boardManager;
    private Vector2 firstTouchPosition;
    private Vector2 finaltouchPosition;
    private Vector2 tempPosition;
    public float swipeAngle = 0;

    private void Awake()
    {
        boardManager = FindObjectOfType<BoardManager>();
        Vector3 realpos = boardManager.GetPosition(new Vector2Int(column, row));
        targetX = realpos.x;
        targetY = realpos.y;
    }

    private void OnEnable()
    {
        int rnd = Random.Range(0, 6);
        dotType = (DotType)rnd;
        GetComponent<SpriteRenderer>().color = colors[rnd];
    }

    private void Update()
    {
        FindMatches();

        if (isMatched)
        {
            SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
            mySprite.color = new Color(0, 0, 0, .2f);
        }

        Vector3 realpos = boardManager.GetPosition(new Vector2Int(column, row));

        targetX = realpos.x;
        targetY = realpos.y;

        if (Mathf.Abs(targetX - transform.position.x) > .1)
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
        else if (swipeAngle > -60 && swipeAngle < 0 && column < boardManager.boardSize.x && row > 0)
        {
            Debug.Log("오른쪽 아래");
            otherDot = boardManager.allDots[column + 1, row - 1];
            otherDot.GetComponent<Dot>().column -= 1;
            otherDot.GetComponent<Dot>().row += 1;
            column += 1;
            row -= 1;
        }
    }

    void FindMatches()
    {
        if (row > 0 && row < boardManager.boardSize.y - 1)
        {
            //수직매치 확인
            Dot upDot1 = UpDot();
            Dot downDot1 = DownDot();
            if (upDot1 != null && downDot1 != null)
            {
                DotType upDot1Type = upDot1.dotType;
                DotType downDot1Type = downDot1.dotType;
                if (upDot1Type == downDot1Type)
                {
                    isMatched = true;
                    upDot1.isMatched = true;
                    downDot1.isMatched = true;
                }
            }

            //슬래시 매치
            Dot rightUpDot1 = RightUpDot();
            Dot leftDownDot1 = LeftDownDot();
            if (rightUpDot1 != null && leftDownDot1 != null)
            {
                DotType rightUpDot1Type = rightUpDot1.dotType;
                DotType leftDown1Type = leftDownDot1.dotType;
                if (rightUpDot1Type == leftDown1Type)
                {
                    isMatched = true;
                    rightUpDot1.isMatched = true;
                    leftDownDot1.isMatched = true;
                }
            }

            //역슬래시 매치
            Dot rightDownDot1 = RightDownDot();
            Dot leftUpDot1 = LeftUpDot();
            if (rightDownDot1 != null && leftUpDot1 != null)
            {
                DotType rightDownDot1Type = rightDownDot1.dotType;
                DotType leftUpDot1Type = leftUpDot1.dotType;

                if (rightDownDot1Type == leftUpDot1Type)
                {
                    isMatched = true;
                    rightDownDot1.isMatched = true;
                    leftUpDot1.isMatched = true;
                }
            }
        }
    }

    //바로 밑의 Dot 체크 및 참조
    public Dot DownDot()
    {
        if (column == 0 || column == 6)
        {
            if (row > 3)
            {
                return boardManager.allDots[column, row - 2].GetComponent<Dot>();
            }
        }
        else if (column == 1 || column == 5)
        {
            if (row > 2)
            {
                return boardManager.allDots[column, row - 2].GetComponent<Dot>();
            }
        }
        else if (column == 2 || column == 4)
        {
            if (row > 1)
            {
                return boardManager.allDots[column, row - 2].GetComponent<Dot>();
            }
        }
        else
        {
            if (row > 0)
            {
                return boardManager.allDots[column, row - 2].GetComponent<Dot>();
            }
        }
        return null;
    }

    //바로 위의 Dot 체크 및 참조
    public Dot UpDot()
    {
        if (column == 0 || column == 6)
        {
            if (row < 7)
            {
                return boardManager.allDots[column, row + 2].GetComponent<Dot>();
            }
        }
        else if (column == 1 || column == 5)
        {
            if (row < 8)
            {
                return boardManager.allDots[column, row + 2].GetComponent<Dot>();
            }
        }
        else if (column == 2 || column == 4)
        {
            if (row < 9)
            {
                return boardManager.allDots[column, row + 2].GetComponent<Dot>();
            }
        }
        else
        {
            if (row < 10)
            {
                return boardManager.allDots[column, row + 2].GetComponent<Dot>();
            }
        }
        return null;
    }

    //우측 위의 Dot 체크 및 참조
    public Dot RightUpDot()
    {
        switch (column)
        {
            case 0:
                if (row == 7 || row == 5 || row == 3) { return boardManager.allDots[column + 1, row + 1].GetComponent<Dot>(); }
                break;
            case 1:
                if (row == 2 || row == 4 || row == 6 || row == 8) { return boardManager.allDots[column + 1, row + 1].GetComponent<Dot>(); }
                break;
            case 2:
                if (row == 1 || row == 3 || row == 5 || row == 7 || row == 9) { return boardManager.allDots[column + 1, row + 1].GetComponent<Dot>(); }
                break;
            case 3:
                if (row == 0 || row == 2 || row == 4 || row == 6 || row == 8) { return boardManager.allDots[column + 1, row + 1].GetComponent<Dot>(); }
                break;
            case 4:
                if (row == 1 || row == 3 || row == 5 || row == 7) { return boardManager.allDots[column + 1, row + 1].GetComponent<Dot>(); }
                break;
            case 5:
                if (row == 2 || row == 4 || row == 6) { return boardManager.allDots[column + 1, row + 1].GetComponent<Dot>(); }
                break;
            case 6:
                break;
        }
        return null;

    }

    //좌측 아래의 Dot 체크 및 참조
    public Dot LeftDownDot()
    {
        switch (column)
        {
            case 0:
                break;
            case 1:
                if (row == 4 || row == 6 || row == 8) { return boardManager.allDots[column - 1, row - 1].GetComponent<Dot>(); }
                break;
            case 2:
                if (row == 3 || row == 5 || row == 7 || row == 9) { return boardManager.allDots[column - 1, row - 1].GetComponent<Dot>(); }
                break;
            case 3:
                if (row == 2 || row == 4 || row == 6 || row == 8 || row == 10) { return boardManager.allDots[column - 1, row - 1].GetComponent<Dot>(); }
                break;
            case 4:
                if (row == 1 || row == 3 || row == 5 || row == 7 || row == 9) { return boardManager.allDots[column - 1, row - 1].GetComponent<Dot>(); }
                break;
            case 5:
                if (row == 2 || row == 4 || row == 6 || row == 8) { return boardManager.allDots[column - 1, row - 1].GetComponent<Dot>(); }
                break;
            case 6:
                if (row == 3 || row == 5 || row == 7) { return boardManager.allDots[column - 1, row - 1].GetComponent<Dot>(); }
                break;
        }

        return null;
    }

    //우측 아래의 Dot 체크 및 참조
    public Dot RightDownDot()
    {
        switch (column)
        {
            case 0:
                if (row == 4 || row == 5 || row == 7) { return boardManager.allDots[column + 1, row - 1].GetComponent<Dot>(); }
                break;
            case 1:
                if (row == 2 || row == 4 || row == 6 || row == 8) { return boardManager.allDots[column + 1, row - 1].GetComponent<Dot>(); }
                break;
            case 2:
                if (row == 1 || row == 3 || row == 5 || row == 7 || row == 9) { return boardManager.allDots[column + 1, row - 1].GetComponent<Dot>(); }
                break;
            case 3:
                if (row == 2 || row == 4 || row == 6 || row == 8 || row == 10) { return boardManager.allDots[column + 1, row - 1].GetComponent<Dot>(); }
                break;
            case 4:
                if (row == 3 || row == 5 || row == 7 || row == 9) { return boardManager.allDots[column + 1, row - 1].GetComponent<Dot>(); }
                break;
            case 5:
                if (row == 4 || row == 6 || row == 8) { return boardManager.allDots[column + 1, row - 1].GetComponent<Dot>(); }
                break;
            case 6:
                break;
        }

        return null;
    }

    //우측 위의 Dot 체크 및 참조
    public Dot LeftUpDot()
    {
        switch (column)
        {
            case 0:
                break;
            case 1:
                if (row == 2 || row == 4 || row == 6) { return boardManager.allDots[column - 1, row + 1].GetComponent<Dot>(); }
                break;
            case 2:
                if (row == 1 || row == 3 || row == 5 || row == 7) { return boardManager.allDots[column - 1, row + 1].GetComponent<Dot>(); }
                break;
            case 3:
                if (row == 0 || row == 2 || row == 4 || row == 6 || row == 8) { return boardManager.allDots[column - 1, row + 1].GetComponent<Dot>(); }
                break;
            case 4:
                if (row == 1 || row == 3 || row == 5 || row == 7 || row == 9) { return boardManager.allDots[column - 1, row + 1].GetComponent<Dot>(); }
                break;
            case 5:
                if (row == 2 || row == 4 || row == 6 || row == 8) { return boardManager.allDots[column - 1, row + 1].GetComponent<Dot>(); }
                break;
            case 6:
                if (row == 3 || row == 5 || row == 7) { return boardManager.allDots[column - 1, row + 1].GetComponent<Dot>(); }
                break;
        }

        return null;
    }
}
