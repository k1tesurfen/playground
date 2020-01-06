using System.Collections.Generic;
using UnityEngine;

public class GridFire : MonoBehaviour
{

    public Collider gridPlane;

    public float cellSize;
    public GameObject[,] grid;

    private Vector3 gridStart = Vector3.zero;
    private int gridX;
    private int gridY;

    //GameObject to select where the fire should start. 
    public GameObject fireStart;
    //Prefab that represents the fire in one cell.
    public GameObject fire;

    Vector2Int fireStartCell = new Vector2Int();
    private float timer = 2f;


    // Start is called before the first frame update
    void Start()
    {
        gridPlane = gameObject.GetComponent<BoxCollider>();
        gridStart = gridPlane.bounds.min;

        gridX = (int)Mathf.Ceil(gridPlane.bounds.extents.x * 2f / cellSize);
        gridY = (int)Mathf.Ceil(gridPlane.bounds.extents.z * 2f / cellSize);
        grid = new GameObject[gridX, gridY];
        Debug.Log("created new grid with " + gridX * gridY + " cells");

        StartFire();
        fireStart.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer < 0f)
        {
                List<Vector2Int> nextFireCells = new List<Vector2Int>();
                //for each cell check something
                for (int x = 0; x < gridX; x++)
                {
                    for (int y = 0; y < gridY; y++)
                    {
                        //if there is a fire in the cell spawn new fire around it.
                        if (grid[x, y] != null)
                        {
                            if (Random.Range(0f, 1f) > 0.9f)
                            {
                                nextFireCells.AddRange(GetEmptyNeighbours(new Vector2Int(x, y)));
                            }
                        }
                    }
                }
                foreach (Vector2Int pos in nextFireCells)
                {
                    if (grid[pos.x, pos.y] == null)
                    {
                        RaycastHit hit;
                        //shoots a ray from the top of the fire boundary collider towards the ground.  
                        if (Physics.Raycast(new Vector3(gridStart.x + pos.x * cellSize, gridPlane.bounds.max.y, gridStart.z + pos.y * cellSize), Vector3.down, out hit)){
                            GameObject cellFire = Instantiate(fire, hit.point, Quaternion.identity, transform) as GameObject;
                            grid[pos.x, pos.y] = cellFire;
                        }
                    }
                }

            timer = Random.Range(0.5f, 1.5f);
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    public void StartFire()
    {
        float minDist = Mathf.Infinity;
        if (grid.Length > 0)
        {
            for (int x = 0; x < gridX; x++)
            {
                for (int y = 0; y < gridY; y++)
                {
                    float currentDist = Vector3.Distance(new Vector3(gridStart.x + x * cellSize, gridStart.y, gridStart.z + y * cellSize), fireStart.transform.position);
                    if (currentDist < minDist)
                    {
                        minDist = currentDist;
                        fireStartCell.x = x;
                        fireStartCell.y = y;
                    }
                }
            }
            GameObject cellFire = Instantiate(fire, new Vector3(gridStart.x + fireStartCell.x * cellSize, gridStart.y, gridStart.z + fireStartCell.y * cellSize), Quaternion.identity, transform) as GameObject;
            grid[fireStartCell.x, fireStartCell.y] = cellFire;
        }
    }

    //Gets all empty neighbor cells. A neighbor is either north south west or east of the currentCell.
    private List<Vector2Int> GetEmptyNeighbours(Vector2Int currentCell)
    {
        List<Vector2Int> emptyCells = new List<Vector2Int>();

        if (IsEmptyCell(currentCell.x - 1, currentCell.y))
        {
            emptyCells.Add(new Vector2Int(currentCell.x - 1, currentCell.y));
        }

        if (IsEmptyCell(currentCell.x + 1, currentCell.y))
        {
            emptyCells.Add(new Vector2Int(currentCell.x + 1, currentCell.y));
        }

        if (IsEmptyCell(currentCell.x, currentCell.y - 1))
        {
            emptyCells.Add(new Vector2Int(currentCell.x, currentCell.y - 1));
        }

        if (IsEmptyCell(currentCell.x + 1, currentCell.y + 1))
        {
            emptyCells.Add(new Vector2Int(currentCell.x, currentCell.y + 1));
        }

        return emptyCells;
    }

    //Checks if the cell at the given grid coordinates is inside the grid and is empty.
    private bool IsEmptyCell(int x, int y)
    {
        bool outOfBounds = (x >= gridX) || (x < 0) | (y >= gridY) || (y < 0);
        if (!outOfBounds)
        {
            if (grid[x, y] == null)
            {
                return true;
            }
        }
        return false;
    }


}
