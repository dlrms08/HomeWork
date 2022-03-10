using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager intance;

    public Vector2Int boardSize;

    public Board boardPrefab;
    public Board[,] boards;

    public Dot dotPrefab;
    public Transform dotsParent;
    public GameObject[,] allDots;

    private ObjPool dotsPool;

    void Awake()
    {
        intance = this;
        dotsPool = FindObjectOfType<ObjPool>();
        allDots = new GameObject[boardSize.x, boardSize.y];
        boards = new Board[boardSize.x, boardSize.y];

        while (true)
        {
            BoardInit();
            if (!CheckAllMatch()) break;
            DeleteBoard();
        }

    }

    private void Update()
    {

    }

    //생성된 보드를 삭제 
    public void DeleteBoard()
    {
        for (int i = 0; i < boardSize.x; i++)
        {
            for (int j = 0; j < boardSize.y; j++)
            {
                if (allDots[i, j] != null)
                {
                    dotsPool.ObjReset();
                    Destroy(boards[i, j]);
                }
            }
        }
    }

    //보드 생성
    void BoardInit()
    {
        int nullCount = 0;
        for (int x = 0; x < boardSize.x; x++)
        {
            for (int y = 0; y < boardSize.y; y++)
            {
                if ((x + y) % 2 == 0)
                {
                    continue;
                }

                if ((x == 0 && y == 1) || (x == 0 && y == boardSize.y - 2) || (x == 1 && y == 0) || (x == 1 && y == boardSize.y - 1))
                    continue;

                if ((x == boardSize.x - 1 && y == 1) || (x == boardSize.x - 1 && y == boardSize.y - 2) || (x == boardSize.x - 2 && y == 0) || (x == boardSize.x - 2 && y == boardSize.y - 1))
                    continue;

                if (allDots[x, y] == null)
                {
                    nullCount++;
                }

                var pos = new Vector2Int(x, y);
                var board = Instantiate(boardPrefab);
                board.pos = pos;
                board.transform.SetParent(transform, false);
                board.transform.position = BoardUtil.GetPosition(pos);
                board.name = "(" + x + ", " + y + ")";
                boards[x,y] = board;



                var dot = dotsPool.GetPooledObject().GetComponent<Dot>();
                dot.transform.SetParent(dotsParent, false);
                dot.name = "(" + x + ", " + y + ")";
                dot.column = x;
                dot.row = y;
                dot.previousColumn = x;
                dot.previousRow = y;
                Vector2 tempPosition = new Vector2(x, y + 100);
                dot.transform.position = tempPosition;
                allDots[x, y] = dot.gameObject;
                allDots[x, y].SetActive(true);

            }
            //Debug.Log(nullCount);
            nullCount = 0;
        }
        CheckAllMatch();
    }

    //생성후의 모든 매치를 체크
    bool CheckAllMatch()
    {
        for(int i = 0; i < boardSize.x; i++)
        {
            for(int j = 0; j < boardSize.y; j++)
            {
                if(allDots[i, j] != null)
                {
                    if (ChecKDownMatch(i, j))
                        return true;
                    if (CheckLeftUpMatch(i, j))
                        return true;
                    if (CheckLeftDownMatch(i, j))
                        return true;
                }
            }
        }
        return false;
    }

    //아래 체크
    bool ChecKDownMatch(int column, int row)
    {
        Dot StartDot = allDots[column, row].GetComponent<Dot>();
        Dot SecoundDot = MatchUtil.DownDot(column, row);
        Dot ThirdDot = MatchUtil.DoubleDownDot(column, row);

        if (StartDot != null && SecoundDot != null && ThirdDot != null)
        {
            if (StartDot.dotType == SecoundDot.dotType)
            {
                if (SecoundDot.dotType == ThirdDot.dotType)
                {
                    Debug.Log(StartDot.column + ", " + StartDot.row + " " + SecoundDot.column + ", " + SecoundDot.row + " " + ThirdDot.column + ", " + ThirdDot.row);
                    return true;
                }
            }
        }
        return false;
    }

    //역대각 아래 체크 
    bool CheckLeftDownMatch(int column, int row)
    {
        Dot StartDot = allDots[column, row].GetComponent<Dot>();
        Dot SecoundDot = MatchUtil.LeftDownDot(column, row);
        Dot ThirdDot = MatchUtil.DoubleLeftDownDot(column, row);

        if (StartDot != null && SecoundDot != null && ThirdDot != null)
        {
            if (StartDot.dotType == SecoundDot.dotType)
            {
                if (SecoundDot.dotType == ThirdDot.dotType)
                {
                    Debug.Log(StartDot.column + ", " + StartDot.row + " " + SecoundDot.column + ", " + SecoundDot.row + " " + ThirdDot.column + ", " + ThirdDot.row);
                    return true;
                }
            }
        }
        return false;
    }

    //대각 위 채크
    bool CheckLeftUpMatch(int column, int row)
    {
        Dot StartDot = allDots[column, row].GetComponent<Dot>();
        Dot SecoundDot = MatchUtil.LeftUpDot(column, row);
        Dot ThirdDot = MatchUtil.DoubleLeftUpDot(column, row);

        if (StartDot != null && SecoundDot != null && ThirdDot != null)
        {
            if (StartDot.dotType == SecoundDot.dotType)
            {
                if (SecoundDot.dotType == ThirdDot.dotType)
                {
                    Debug.Log(StartDot.column + ", " + StartDot.row + " " + SecoundDot.column + ", " + SecoundDot.row + " " + ThirdDot.column + ", " + ThirdDot.row);
                    return true;
                }
            }
        }
        return false;
    }

    private void DestroyMatchesAt(int column, int row)
    {
        if(allDots[column, row].GetComponent<Dot>().isMatched)
        {
            allDots[column, row].SetActive(false);
            allDots[column, row].GetComponent<Dot>().isMatched = false;
            allDots[column, row] = null;
            GameManager.intance.AddScore(1);
        }
    }

    public void DestoryMatches()
    {
        for (int x = 0; x < boardSize.x; x++)
        {
            for (int y = 0; y < boardSize.y; y++)
            {
                if ((x + y) % 2 == 0)
                {
                    continue;
                }

                if ((x == 0 && y == 1) || (x == 0 && y == boardSize.y - 2) || (x == 1 && y == 0) || (x == 1 && y == boardSize.y - 1))
                    continue;

                if ((x == boardSize.x - 1 && y == 1) || (x == boardSize.x - 1 && y == boardSize.y - 2) || (x == boardSize.x - 2 && y == 0) || (x == boardSize.x - 2 && y == boardSize.y - 1))
                    continue;

                if (allDots[x, y] != null)
                {
                    DestroyMatchesAt(x, y);
                }
            }
        }
        StartCoroutine(DecreaseRowCo());
    }

    public IEnumerator DecreaseRowCo()
    {
        int nullCount = 0;
        for (int x = 0; x < boardSize.x; x++)
        {
            for (int y = 0; y < boardSize.y; y++)
            {
                if ((x + y) % 2 == 0)
                {
                    continue;
                }

                if ((x == 0 && y == 1) || (x == 0 && y == boardSize.y - 2) || (x == 1 && y == 0) || (x == 1 && y == boardSize.y - 1))
                    continue;

                if ((x == boardSize.x - 1 && y == 1) || (x == boardSize.x - 1 && y == boardSize.y - 2) || (x == boardSize.x - 2 && y == 0) || (x == boardSize.x - 2 && y == boardSize.y - 1))
                    continue;

                if (allDots[x, y] == null)
                {
                    nullCount++; 
                }
                else if(nullCount > 0)
                {
                    var vol = nullCount * 2;    
                    allDots[x, y].GetComponent<Dot>().row -= vol;
                    allDots[x, y] = null;
                }
            }
            Debug.Log(nullCount);
            nullCount = 0;
        }
        yield return new WaitForSeconds(.4f);

        StartCoroutine(FillBoardCo());
    }

    private void ReFillBoard()
    {
        for (int x = 0; x < boardSize.x; x++)
        {
            for (int y = 0; y < boardSize.y; y++)
            {
                if ((x + y) % 2 == 0)
                {
                    continue;
                }

                if ((x == 0 && y == 1) || (x == 0 && y == boardSize.y - 2) || (x == 1 && y == 0) || (x == 1 && y == boardSize.y - 1))
                    continue;

                if ((x == boardSize.x - 1 && y == 1) || (x == boardSize.x - 1 && y == boardSize.y - 2) || (x == boardSize.x - 2 && y == 0) || (x == boardSize.x - 2 && y == boardSize.y - 1))
                    continue;


                if (allDots[x, y] == null)
                {

                    Vector2 tempPosition = new Vector2(x, y + 100);
                    GameObject piece = dotsPool.GetPooledObject();
                    piece.SetActive(true);

                    piece.transform.position = tempPosition;
                    piece.GetComponent<Dot>().column = x;
                    piece.GetComponent<Dot>().row = y;
                    
                    //piece.GetComponent<Dot>().DropDown();
                    allDots[x, y] = piece;
                }
            }
        }
    }

    private bool MatchesOnBoard()
    {
        for (int x = 0; x < boardSize.x; x++)
        {
            for (int y = 0; y < boardSize.y; y++)
            {
                if (allDots[x, y] != null)
                {
                    if(allDots[x, y].GetComponent<Dot>().isMatched)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private IEnumerator FillBoardCo()
    {
        ReFillBoard();
        yield return new WaitForSeconds(.5f);

        while(MatchesOnBoard())
        {
            yield return new WaitForSeconds(.5f);
            DestoryMatches();
        }

    }
}

