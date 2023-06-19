using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenager : MonoBehaviour
{
    [HideInInspector]
    public Grid Grid;

    [HideInInspector]
    public Snake Snake;

    [HideInInspector]
    public SnakeMovement SnakeMovement;

    private void Start()
    {
        Grid = GetComponent<Grid>();
        SnakeMovement = GetComponent<SnakeMovement>();
        Snake = GetComponent<Snake>();
    }

    public void StartGame()
    {
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

}
