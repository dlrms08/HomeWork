using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public Dot selectDot;
    public List<Dot> curMatchDots = new List<Dot>();

    public const float dragDistance = 1.5f;
    private bool isReady = true;
    private BoardManager boardManager;

    public Dot[] backUpBlock = new Dot[2];

    private void Awake()
    {
        boardManager = FindObjectOfType<BoardManager>();
    }

    private void Update()
    {
        if (!isReady) return;
        //if (selectDot != null) return;
        if (Input.GetMouseButtonDown(0))
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hitBlock = Physics2D.Raycast(mousePos, Vector2.zero, 0f);
            //Debug.Log(hitBlock.transform.position);

            if(hitBlock.collider == null)
            {
                isReady = true;
                return;
            }

            if (hitBlock.collider != null && hitBlock.collider.gameObject.GetComponent<Dot>() != null)
            {
                selectDot = hitBlock.collider.gameObject.GetComponent<Dot>();
                //StartCoroutine(CoWaitDrag(selectDot.transform.position));
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isReady = false;

            if (selectDot == null)
            {
                selectDot = null;
                isReady = true;
                return;
            }

            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float distance = Vector3.Distance(selectDot.transform.position, mousePos);

            if (distance > dragDistance)
            {
                //클릭종료 
                Direction dir = boardManager.GetDirection(selectDot.transform.position, mousePos);
                Vector2Int chagePos = boardManager.GetNeighbor(selectDot.pos, dir);
                Dot changeDot = boardManager.dots.Find(x => x.pos == chagePos);
                
                Board startOnBoard = boardManager.boards.Find(x => x.pos == selectDot.pos);
                Board changeOnboard = boardManager.boards.Find(x => x.pos == chagePos);

                if (startOnBoard == null || changeOnboard == null)
                {
                    selectDot = null;
                    isReady = true;
                    return;
                }

                selectDot.Moving(changeOnboard.transform.position);
                changeDot.Moving(startOnBoard.transform.position);
                selectDot.pos = changeOnboard.pos;
                changeDot.pos = startOnBoard.pos;

                StartCoroutine(CheckMatch(changeDot));
            }
        }
    }

    IEnumerator CheckMatch(Dot dot)
    {
        yield return new WaitForSeconds(.15f);
        boardManager.LineMatch(selectDot);
        boardManager.LineMatch(dot);
        boardManager.CheckMatch();
        
        yield return new WaitForSeconds(.15f);
        boardManager.ClearMatch();
        selectDot = null;
        isReady = true;
    }


    /*
    IEnumerator CoWaitDrag(Vector3 initPos)
    {
        isReady = false;
        while (true)
        {
            if (Input.GetMouseButtonUp(0))break;
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(mousePos);
            float distance = Vector3.Distance(initPos, mousePos);

            if(distance > dragDistance)
            {
                //클릭종료 
                Direction dir = boardManager.GetDirection(initPos, mousePos);
                Vector2Int chagePos = boardManager.GetNeighbor(selectDot.pos, dir);
                Dot changeDot = boardManager.dots.Find(x => x.pos == chagePos);
                //Debug.Log(changeDot.pos);
                Board startOnBoard = boardManager.boards.Find(x => x.pos == selectDot.pos);
                Board changeOnboard = boardManager.boards.Find(x => x.pos == chagePos);
                if (changeDot == null)
                {
                    Debug.Log("없어");
                    break;
                }
                selectDot.transform.position = changeOnboard.transform.position;
                changeDot.transform.position = startOnBoard.transform.position;
                selectDot.pos = changeOnboard.pos;
                changeDot.pos = startOnBoard.pos;
            }
            yield return null;
        }
        //초기화
        selectDot = null;
        isReady = true;    
    }
    */
}
