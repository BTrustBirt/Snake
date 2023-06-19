using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Grid grid;

    private List<GameObject> snakeBody = new List<GameObject>();

    [SerializeField]
    private GameObject snakeBodyPrefab;

    [HideInInspector]
    public bool addTail = false;

    [HideInInspector]
    public bool destroyTail = false;

    [HideInInspector]
    public bool MoveRevers = false;

    private GameMenager gameMenager;

    private void Start()
    {
        grid = gameObject.GetComponent<Grid>();
        gameMenager = GetComponent<GameMenager>();
    }

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
        gameMenager.CameraFallow(tempGameObiect.transform);
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
        grid.ChcekGridPosition(ConwertToVector2Init(movePosition));

        Vector2 prevPosition = snakeBody[0].transform.position;
        
        for (int i = 0; i < snakeBody.Count; i++)
        {
            if (i == 0)
            {
                grid.CleanGridPosition(ConwertToVector2Init(prevPosition));
                snakeBody[i].transform.position = movePosition;
                grid.AddToGrid(ConwertToVector2Init(movePosition), snakeBody[i]);
            }
            else
            {
                grid.CleanGridPosition(ConwertToVector2Init(snakeBody[i].transform.position));
                Vector3 currentPosition = snakeBody[i].transform.position;
                snakeBody[i].transform.position = prevPosition;
                grid.AddToGrid(ConwertToVector2Init(prevPosition), snakeBody[i]);
                prevPosition = currentPosition;
            }
        }

        if (addTail)
        {
            addTail = false;
            GameObject tempGameObject = Instantiate(snakeBodyPrefab, prevPosition, Quaternion.identity);
            snakeBody.Add(tempGameObject);
            grid.AddToGrid(ConwertToVector2Init(prevPosition), tempGameObject);
            tempGameObject.GetComponent<ActionColision>().GetRef(gameMenager);
        }

        if (destroyTail && snakeBody.Count > 1)
        {
            Destroy(snakeBody.Last().gameObject);
            snakeBody.RemoveAt(snakeBody.Count - 1);
        }
    }

    public void CleanSnake()
    {
        foreach (var item in snakeBody)
        {
            if (item != null)
            {
                Destroy(item);
            }
        }
        snakeBody.Clear();
        snakeBody = new List<GameObject>();
    }
}
