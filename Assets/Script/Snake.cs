using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Snake : MonoBehaviour
{

    [SerializeField]
    private Grid grid;
    private List<GameObject> snakeBody = new List<GameObject>();

    [SerializeField]
    private GameObject snakeBodyPrefab;

    public bool addTail = false;

    public bool destroyTail = false;

    public bool MoveRevers = false;

    private Vector2Int ConwertToVector2Init(Vector2 vector)
    {
        return new Vector2Int((int)vector.x, (int)vector.y);
    }

    public void AddSnakeTaill(Vector2 transform)
    {
        GameObject tempGameObiect = Instantiate(snakeBodyPrefab, transform, Quaternion.identity);
        snakeBody.Add(tempGameObiect);
    }

    public void CreatSnakeHead(Vector2 transform)
    {
        GameObject tempGameObiect = Instantiate(snakeBodyPrefab, transform, Quaternion.identity);
        snakeBody.Add(tempGameObiect);
    }

    public GameObject GetPositionGameObiectFromGrid()
    {
        if (snakeBody.Count > 1)
        {
            return snakeBody[1];
        }
        return null;
    }

    public Vector2Int GetSnakHeadPosition()
    {
        return ConwertToVector2Init(snakeBody.First().transform.position);
    }

    public void MoveSnakeAtPoint(Vector2 pos)
    {
        snakeBody.First().transform.position = pos;
    }

    public void MoveSnakeFoward(Vector2 movePosition)
    {
        Vector2 prevPosition = snakeBody[0].transform.position;


        for (int i = 0; i < snakeBody.Count; i++)
        {
            if (i == 0)
            {
                grid.CleanGridPosition(ConwertToVector2Init(prevPosition));
                snakeBody.First().transform.position = movePosition;
                grid.AddToGrid(ConwertToVector2Init(movePosition), snakeBody.First());
            }
            else
            {
                grid.CleanGridPosition(ConwertToVector2Init(snakeBody[i].transform.position));
                snakeBody[i].transform.position = prevPosition;
                grid.AddToGrid(ConwertToVector2Init(prevPosition), snakeBody[i]);
                prevPosition = snakeBody[i].transform.position;
            }
        }

        if (addTail)
        {
            addTail = false;
            GameObject tempGameObiect = Instantiate(snakeBodyPrefab, prevPosition, Quaternion.identity);
            snakeBody.Add(tempGameObiect);
            grid.AddToGrid(ConwertToVector2Init(prevPosition), tempGameObiect);

        }

        if (destroyTail && snakeBody.Count > 1)
        {
            Destroy(snakeBody.Last().gameObject);
            snakeBody.RemoveAt(snakeBody.Count - 1);
        }
    }
}
