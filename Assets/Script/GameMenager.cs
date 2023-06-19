using Cinemachine;
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
    private GameObject playFilds;

    [SerializeField]
    private TMP_Text textGridSize;

    [SerializeField]
    private GameObject panelGameOver;

    [SerializeField]
    private GameObject panelGameUp;

    [SerializeField]
    private CinemachineVirtualCamera camera;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        Grid = GetComponent<Grid>();
        SnakeMovement = GetComponent<SnakeMovement>();
        Snake = GetComponent<Snake>();

        playFilds.SetActive(false);
        panelGameUp.SetActive(true);
        panelGameOver.SetActive(false);
        Grid.GridSize = 16;
        SchowGridSizeText();
        spriteRenderer = playFilds.GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Public function that increases the grid size by 2 units.
    /// </summary>
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

    /// <summary>
    /// Public function that decreases the grid size by 2 units.
    /// </summary>
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

    /// <summary>
    /// Private function that updates the text displaying the grid size.
    /// </summary>
    private void SchowGridSizeText()
    {
        textGridSize.text = Grid.GridSize.ToString();
    }

    /// <summary>
    /// Public function that starts the game, creating the grid, snake, adjusting the play field,
    /// and initiating the SnakeMovement.
    /// </summary>
    public void StartGame()
    {
        Grid.CreatGrid();
        Vector2 vec = new Vector2Int(Grid.GetGridSize() / 2, Grid.GetGridSize() / 2);

        Snake.CreatSnakeHead(vec);

        Vector3 spritsize = new Vector3(Grid.GetGridSize(), Grid.GetGridSize(), 1);
        spriteRenderer.transform.localScale= spritsize;
        spriteRenderer.transform.localPosition = new Vector3((Grid.GetGridSize() / 2) -0.5f,(Grid.GetGridSize() / 2) -0.5f, 1f);
       
        playFilds.SetActive(true);

        panelGameUp.SetActive(false);

        SnakeMovement.StartGame();
    }

    /// <summary>
    /// Public function that sets the camera to follow a specified transform.
    /// </summary>
    public void CameraFallow(Transform fallowTransform)
    {
        camera.Follow = fallowTransform;
    }

    /// <summary>
    /// Public function that restarts the game by cleaning the grid and snake, and showing the game UI panel.
    /// </summary>
    public void RestarGame()
    {
        Grid.CleanGrid();
        Snake.CleanSnake();
        panelGameOver.SetActive(false);
        panelGameUp.SetActive(true);
    }
    /// <summary>
    /// Public function that triggers game over by stopping the SnakeMovement, 
    /// hiding the play field, and showing the game over panel.
    /// </summary>
    public void GameOver()
    {
        SnakeMovement.GameRun = false;
        playFilds.SetActive(false);
        panelGameOver.SetActive(true);
        SnakeMovement.GameOver();
    }

}
