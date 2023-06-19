using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMenager : MonoBehaviour
{
    [HideInInspector]
    public Grid Grid;

    [HideInInspector]
    public Snake Snake;

    [HideInInspector]
    public SnakeMovement SnakeMovement;

    [SerializeField]
    private GameObject canvas;

    [SerializeField]
    private TMP_Text textGridSize;

    [SerializeField]
    private GameObject panelGameOver;

    [SerializeField]
    private GameObject panelGameUp;

    private void Start()
    {
        Grid = GetComponent<Grid>();
        SnakeMovement = GetComponent<SnakeMovement>();
        Snake = GetComponent<Snake>();

        canvas.SetActive(true);
        panelGameUp.SetActive(true);
        panelGameOver.SetActive(false);
        Grid.GridSize = 16;
        SchowGridSizeText();
    }

    public void GridAdd()
    {
        if (Grid.GridSize >= 30)
        {
            Grid.GridSize = 30;
            return;
        }
        Grid.GridSize += 2;
        SchowGridSizeText();
    }

    public void GridDecres()
    {
        if (Grid.GridSize <= 10)
        {
            Grid.GridSize = 10;
            return;
        }

        Grid.GridSize -= 2;
        SchowGridSizeText();
    }

    private void SchowGridSizeText()
    {
        textGridSize.text = Grid.GridSize.ToString();
    }

    public void StartGame()
    {
        Grid.CreatGrid();
        Vector2 vec = new Vector2Int(Grid.GetGridSize() / 2, Grid.GetGridSize() / 2);

        Snake.CreatSnakeHead(vec);

        canvas.SetActive(false);

        SnakeMovement.StartGame();
    }

    public void RestarGame()
    {
        panelGameOver.SetActive(false);
        panelGameUp.SetActive(true);
    }

    public void GameOver()
    {
        SnakeMovement.GameRun = false;
        canvas.SetActive(true);
        panelGameOver.SetActive(true);
    }

}
