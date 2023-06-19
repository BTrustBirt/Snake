using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenager : MonoBehaviour
{

    private IEnumerator speedTimeCarutine;

    private float timeSpeed;
    public Grid Grid;

    public Snake Snake;

    public SnakeMovement SnakeMovement;

    public float Speed = 1.2f;

    private IEnumerator SpeedMovment()
    {
        SnakeMovement.interval = Speed;
        yield return new WaitForSeconds(timeSpeed);
        ResetTTime();
    }

    private void Start()
    {
        Grid = GetComponent<Grid>();
        SnakeMovement = GetComponent<SnakeMovement>();
        Snake = GetComponent<Snake>();

        Grid.CreatGrid();
        Vector2 vec = new Vector2Int(Grid.GetGridSize() / 2, Grid.GetGridSize() / 2);

        Snake.CreatSnakeHead(vec);

        SnakeMovement.StartGame();
    }

    public void GameOver()
    {
        SnakeMovement.GameRun = false;
        Debug.Log("GAME OVER");
    }

    public void IngressSpeed()
    {
        if (Speed < 0.5f)
        {
            timeSpeed = +2f;
            speedTimeCarutine.Reset();
            return;
        }

        Speed = -0.1f;
        if (speedTimeCarutine == null)
        {
            speedTimeCarutine = SpeedMovment();
            StartCoroutine(SpeedMovment());
        }
    }

    public void ResetTTime()
    {
        Speed = 1.2f;
        StopCoroutine(speedTimeCarutine);
        speedTimeCarutine = null;
        SnakeMovement.interval = Speed;
    }
}
