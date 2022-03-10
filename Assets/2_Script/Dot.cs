using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    public enum DotState
    {
        defult,
        drop
    }

    public DotState dotState = DotState.defult;

    public DotType dotType = DotType.Top;
    public Color[] colors;
    public Sprite itemImage;
    public int column;
    public int row;
    public int previousColumn;
    public int previousRow;
    public float targetX;
    public float targetY;
    public bool isMatched = false;
    private GameObject otherDot;
    private Vector2 firstTouchPosition;
    private Vector2 finaltouchPosition;
    private Vector2 tempPosition;
    public float swipeAngle = 0;
    public float swipeResist = 1f;
    float timer = 0;
    int checkRow = 0;

    private void Start()
    {
        Vector3 realpos = BoardUtil.GetPosition(new Vector2Int(column, row));
        targetX = realpos.x;
        targetY = realpos.y;
        previousColumn = column;
        previousRow = row;
    }

    private void OnEnable()
    {
        DotTypeSetting();
    }

    public void DotTypeSetting()
    {
        int rnd = Random.Range(0, colors.Length);
        dotType = (DotType)rnd;
        //Debug.Log(column + ", " + row + " 타입 = " + dotType);

        int color = (int)dotType;
        GetComponent<SpriteRenderer>().color = colors[color];
    }

    private void Update()
    {
        if (dotState == DotState.drop)
            return;

        FindMatchsRow();
        FindMatchsSlash();
        FindMatchsSlash2();

        if (isMatched)
        {
            //SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
            //mySprite.color = new Color(0, 0, 0, .2f);

        }
       
        Vector3 realpos = BoardUtil.GetPosition(new Vector2Int(column, row));

        targetX = realpos.x;
        targetY = realpos.y;

        if (Mathf.Abs(targetX - transform.position.x) > .1)
        {
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .6f);
            if (BoardManager.intance.allDots[column, row] != this.gameObject)
            {
                BoardManager.intance.allDots[column, row] = this.gameObject;
            }
        }
        else
        {
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = tempPosition;
        }

        if (Mathf.Abs(targetY - transform.position.y) > .1)
        {
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .6f);
            if (BoardManager.intance.allDots[column, row] != this.gameObject)
            {
                BoardManager.intance.allDots[column, row] = this.gameObject;
            }
        }
        else
        {
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = tempPosition;
        }
    }

    
    public IEnumerator MoveDown()
    {
        while(true)
        {
            Dot dot = MatchUtil.DownDot(column, row);
            if (dot == null)
            {
                dot.row -= 1;
                yield return new WaitForSeconds(.5f);
            }
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

    public IEnumerator CheckMoveCo()
    {
        yield return new WaitForSeconds(.5f);
        if(otherDot != null)
        {
            if(!isMatched && !otherDot.GetComponent<Dot>().isMatched)
            {
                otherDot.GetComponent<Dot>().column = column;
                otherDot.GetComponent<Dot>().row = row;
                column = previousColumn;
                row = previousRow;
            }
            else
            {
                BoardManager.intance.DestoryMatches();
            }
            otherDot = null;
        }
    }

    //드래그 앵글 계산.
    private void CalculateAngle()
    {
        if (Mathf.Abs(finaltouchPosition.y - firstTouchPosition.y) > swipeResist || Mathf.Abs(finaltouchPosition.x - firstTouchPosition.x) > swipeResist)
        {
            swipeAngle = Mathf.Atan2(finaltouchPosition.y - firstTouchPosition.y, finaltouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
            MovePices();
        }
    }

    void MovePices()
    {
        if (swipeAngle > 0 && swipeAngle <= 60 && column < BoardManager.intance.boardSize.x && row < BoardManager.intance.boardSize.y)
        {
            //Debug.Log("오른쪽 위");
            otherDot = BoardManager.intance.allDots[column + 1, row + 1];
            otherDot.GetComponent<Dot>().column -= 1;
            otherDot.GetComponent<Dot>().row -= 1;
            column += 1;
            row += 1;
        }
        else if (swipeAngle > 60 && swipeAngle <= 120 && row < BoardManager.intance.boardSize.y)
        {
            //Debug.Log("위");
            otherDot = BoardManager.intance.allDots[column, row + 2];
            otherDot.GetComponent<Dot>().row -= 2;
            row += 2;
        }
        else if (swipeAngle > 120 && swipeAngle < 180 && column > 0 && row < BoardManager.intance.boardSize.y)
        {
            //Debug.Log("왼쪽 위");
            otherDot = BoardManager.intance.allDots[column - 1, row + 1];
            otherDot.GetComponent<Dot>().column += 1;
            otherDot.GetComponent<Dot>().row -= 1;
            column -= 1;
            row += 1;
        }
        else if (swipeAngle > -180 && swipeAngle <= -120 && column > 0 && row > 0)
        {
            //Debug.Log("왼쪽 아래");
            otherDot = BoardManager.intance.allDots[column - 1, row - 1];
            otherDot.GetComponent<Dot>().column += 1;
            otherDot.GetComponent<Dot>().row += 1;
            column -= 1;
            row -= 1;
        }
        else if (swipeAngle > -120 && swipeAngle <= -60 && row > 0)
        {
            //Debug.Log("아래");
            otherDot = BoardManager.intance.allDots[column, row - 2];
            otherDot.GetComponent<Dot>().row += 2;
            row -= 2;
        }
        else if (swipeAngle > -60 && swipeAngle < 0 && column < BoardManager.intance.boardSize.x && row > 0)
        {
            //Debug.Log("오른쪽 아래");
            otherDot = BoardManager.intance.allDots[column + 1, row - 1];
            otherDot.GetComponent<Dot>().column -= 1;
            otherDot.GetComponent<Dot>().row += 1;
            column += 1;
            row -= 1;
        }
        StartCoroutine(CheckMoveCo());
    }

    void FindMatchsRow()
    {
        Dot upDot1 = MatchUtil.UpDot(column, row);
        Dot downDot1 = MatchUtil.DownDot(column, row);
        if (upDot1 != null && downDot1 != null)
        {
            if (dotType == upDot1.dotType)
            {
                if (upDot1.dotType == downDot1.dotType)
                {
                    isMatched = true;
                    upDot1.isMatched = true;
                    downDot1.isMatched = true;
                    //Debug.Log("상하매치!");
                }

            }
        }
    }

    void FindMatchsSlash()
    {
        Dot rightUpDot1 = MatchUtil.RightUpDot(column, row);
        Dot leftDownDot1 = MatchUtil.LeftDownDot(column, row);
        if (rightUpDot1 != null && leftDownDot1 != null)
        {
            if (dotType == rightUpDot1.dotType)
            {
                if (rightUpDot1.dotType == leftDownDot1.dotType)
                {
                    isMatched = true;
                    rightUpDot1.isMatched = true;
                    leftDownDot1.isMatched = true;
                    //Debug.Log("대각선 매치!");
                }
            }
        }
    }

    void FindMatchsSlash2()
    {
        //역슬래시 매치
        Dot rightDownDot1 = MatchUtil.RightDownDot(column, row);
        Dot leftUpDot1 = MatchUtil.LeftUpDot(column, row);
        if (rightDownDot1 != null && leftUpDot1 != null)
        {
            if (dotType == rightDownDot1.dotType)
            {
                if (rightDownDot1.dotType == leftUpDot1.dotType)
                {
                    isMatched = true;
                    rightDownDot1.isMatched = true;
                    leftUpDot1.isMatched = true;
                    //Debug.Log("역대각선 매치!");
                }
            }
        }
    }


    /*
    public void DropDown()
    {
        dotState = DotState.drop;
        StartCoroutine(DropDownCo());
    }

    IEnumerator DropDownCo()
    {
        bool Changer = true;
        while(dotState == DotState.drop)
        {
            
            Dot dot = MatchUtil.DownDot(column, row);
            Dot dotL = MatchUtil.LeftDownDot(column, row);
            Dot dotR = MatchUtil.RightDownDot(column, row);

            if (dotL != null && dotR != null && dot != null)
            {
                dotState = DotState.defult;
            }

            if (dot == null)
            {
                column = dot.column;
            }
            else
            {
                if(dotL == null && dotR == null)
                {
                    Changer = !Changer;
                    if(Changer)
                    {
                        column = dotL.column;
                        row = dotL.row;
                    }
                    else
                    {
                        column = dotR.column;
                        row = dotR.row;
                    }
                }
                if(dotL == null && dotR != null)
                {
                    column = dotL.column;
                    row = dotL.row;
                }
                if(dotL != null && dotR == null)
                {
                    column = dotR.column;
                    row = dotR.row;
                }
            }

            transform.position = BoardUtil.GetPosition(new Vector2Int(column ,row));
            yield return new WaitForSeconds(.2f);
        }
    }
    */
}
