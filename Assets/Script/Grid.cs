using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private int countGrid;

    private int gridSize;

    public int GridSize
    {
        get { return gridSize; }
        set { gridSize = value; }
    }


    private GameMenager gameMenager;

    private bool isOccupied;

    private int SpawnIntervval = 5;

    [SerializeField]
    private ActionColision[] spawnObjeck;

    private GameObject[,] grid { get; set; }

    private void Start()
    {
        gameMenager= GetComponent<GameMenager>();
    }

    /// <summary>
    /// Private function that spawns a game object at a random unoccupied grid position.
    /// </summary>
    private void SpawnObject()
    {
        int randx = Random.Range(0, spawnObjeck.Length);
        int randgridX;
        int randgridY;

        GameObject tempObject = null;

        while (isOccupied)
        {
            randgridX = Random.Range(0, grid.GetLength(0));
            randgridY = Random.Range(0, grid.GetLength(1));
            
            if (grid[randgridX, randgridY] == null)
            {
                countGrid = 0;
                isOccupied = false;

                tempObject = Instantiate(spawnObjeck[randx].gameObject);
                tempObject.transform.position = new Vector2(randgridX, randgridY);
                AddToGrid(new Vector2Int(randgridX, randgridY), tempObject);
                tempObject.GetComponent<ActionColision>().GetRef(gameMenager);
            }
        }
    }

    /// <summary>
    /// Public function that checks the grid position for any collision and triggers the associated action.
    /// </summary>
    public void ChcekGridPosition(Vector2Int position)
    {
        ActionColision tempAction;

        if (grid[position.x,position.y] != null)
        {
            tempAction = grid[position.x,position.y].gameObject.GetComponent<ActionColision>();
            
            tempAction.Use();
        }
    }

    /// <summary>
    /// Public function that adds a game object to the grid at the specified grid position.
    /// </summary>
    public void AddToGrid(Vector2Int positionGrig, GameObject snakeTransform)
    {
        grid[positionGrig.x, positionGrig.y] = snakeTransform;

    }

    /// <summary>
    /// Public function that cleans the grid position by removing the game object reference.
    /// </summary>
    public void CleanGridPosition(Vector2Int positionGrig)
    {
        grid[positionGrig.x, positionGrig.y] = null;
    }

    /// <summary>
    /// Public function that creates the grid with the specified size.
    /// </summary>
    public void CreatGrid()
    {
        grid = new GameObject[gridSize, gridSize];
    }

    /// <summary>
    /// Public function that returns the size of the grid.
    /// </summary>
    public int GetGridSize()
    {
        return gridSize;
    }

    /// <summary>
    /// Public function that triggers the spawning of a power-up game object at regular intervals.
    /// </summary>
    public void SpawnObiectPower()
    {
        countGrid++;

        if (countGrid >= SpawnIntervval)
        {
            isOccupied = true;
            SpawnObject();
        }
    }

    public void CleanGrid()
    {
        
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if (grid[i, j] != null) 
                {
                    Destroy(grid[i, j].gameObject);
                    grid[i, j] = null;
            }   }
        }

        grid = null;
    }
}
