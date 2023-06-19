using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    private IEnumerator carutinGame;

    private Vector2Int direction = Vector2Int.right;

    private bool gameRun = true;

    private Grid grid;

    private Vector2Int newDirection;

    private Vector2Int oldDirection;

    private Snake snake;

    private float speed = 1f;

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public KeyCode downKey;
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode upKey;


    private void Start()
    {
        snake = gameObject.GetComponent<Snake>();
        grid = gameObject.GetComponent<Grid>();
    }

    private void ChangePosition(Vector2Int position)
    {
        if (position.x == -1)
        {
            snake.MoveSnakeFoward(new Vector2(grid.GetGridSize() - 1, (int)position.y));
        }

        if (position.y == -1)
        {
            snake.MoveSnakeFoward(new Vector2(position.x, grid.GetGridSize() - 1));
        }

        if (position.x == grid.GetGridSize())
        {
            snake.MoveSnakeFoward(new Vector2(0, position.y));
        }

        if (position.y == grid.GetGridSize())
        {
            snake.MoveSnakeFoward(new Vector2(position.x, 0));
        }
    }

    private bool ChceckPositionInGrid(Vector2Int nextPosition)
    {
        if (nextPosition.x < 0 || nextPosition.x >= grid.GetGridSize() || nextPosition.y < 0 || nextPosition.y >= grid.GetGridSize())
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void CheckOldPosition()
    {
        if (oldDirection == -newDirection)
        {
            return;
        }
        else
        {
            direction = newDirection;
        }

        oldDirection = newDirection;
    }

    private IEnumerator ExampleCoroutine()
    {
        while (gameRun)
        {
            Move();

            yield return new WaitForSeconds(speed);

            if (gameRun)
            {
                grid.SpawnObiectPower();
            }
            
        }
    }

    private void Move()
    {
        var temp = snake.GetSnakHeadPosition();

        Vector2Int nextPosition = new Vector2Int((int)temp.x, (int)temp.y) + direction;

        if (ChceckPositionInGrid(nextPosition))
        {
            snake.MoveSnakeFoward(nextPosition);
        }
        else
        {
            ChangePosition(nextPosition);
        }
    }

    private void Update()
    {
        if (!Input.anyKey || !gameRun)
        {
            return;
        }

        if (Input.GetKeyDown(upKey))
        {
            newDirection = Vector2Int.up;
            return;
        }

        if (Input.GetKeyDown(downKey))
        {
            newDirection = Vector2Int.down;
            return;
        }

        if (Input.GetKeyDown(leftKey))
        {
            newDirection = Vector2Int.left;
            return;
        }

        if (Input.GetKeyDown(rightKey))
        {
            newDirection = Vector2Int.right;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            snake.addTail = true;
        }

        CheckOldPosition();
    }

    public void StartGame()
    {
        carutinGame = ExampleCoroutine();
        gameRun = true;

        StartCoroutine(carutinGame);
    }

    public bool GameRun
    {
        get { return gameRun; }
        set { gameRun = value; }
    }

    public void GameOver()
    {
        StopCoroutine(ExampleCoroutine());
    }
}
