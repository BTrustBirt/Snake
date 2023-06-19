using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private int countGrid;

    [field: SerializeField]
    public int gridSize
    {
        get;
        private set;
    }

    [SerializeField]
    private GameMenager gameMenager;

    private bool isOccupied;

    private int SpawnIntervval = 5;

    [SerializeField]
    private ActionColision[] spawnObjeck;

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
            // isOccupied = false;
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

    public void ChcekGridPosition(Vector2Int position)
    {
        ActionColision tempAction;

        if (grid[position.x,position.y] != null)
        {
            tempAction = grid[position.x,position.y].gameObject.GetComponent<ActionColision>();
            tempAction.Use();
        }
    }

    private GameObject[,] grid { get; set; }

    public void AddToGrid(Vector2Int positionGrig, GameObject snakeTransform)
    {
        grid[positionGrig.x, positionGrig.y] = snakeTransform;

    }

    public GameObject CheckGridPosition(Vector2Int positionGrid)
    {
        if (grid[positionGrid.x, positionGrid.y] != null)
        {
            return grid[positionGrid.x, positionGrid.y];
        }
        return null;
    }

    public void CleanGridPosition(Vector2Int positionGrig)
    {
        grid[positionGrig.x, positionGrig.y] = null;
    }

    public void CreatGrid()
    {
        grid = new GameObject[gridSize, gridSize];
    }


    public int GetGridSize()
    {
        return gridSize;
    }

    public void SpawnObiectPower()
    {
        countGrid++;

        if (countGrid >= SpawnIntervval)
        {
            isOccupied = true;
            SpawnObject();
        }
    }
}
