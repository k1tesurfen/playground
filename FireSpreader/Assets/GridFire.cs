using System.Collections;
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

    private float timer;


    // Start is called before the first frame update
    void Start()
    {
        gridPlane = gameObject.GetComponent<BoxCollider>();
        gridStart = gridPlane.bounds.min;

        gridX = (int)Mathf.Ceil(gridPlane.bounds.extents.x * 2f / cellSize );
        gridY = (int)Mathf.Ceil(gridPlane.bounds.extents.z * 2f / cellSize );
        grid = new GameObject[gridX, gridY];
        Debug.Log("created new grid with " + gridX * gridY + " cells");

        if(grid.Length > 0)
        {
            for(int x = 0; x < gridX; x++)
            {
                for(int y = 0; y < gridY; y++)
                {
                    GameObject t = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    t.transform.position = new Vector3(gridStart.x + x * cellSize, gridStart.y, gridStart.z + y * cellSize);
                    t.transform.localScale = 0.05f * Vector3.one;
                    Debug.Log("ima cell");
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(grid.Length > 0)
        {
            for(int x = 0; x < gridX; x++)
            {
                for(int y = 0; y < gridY; y++)
                {
                    GameObject t = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    t.transform.position = new Vector3(gridStart.x + x * cellSize, gridStart.y, gridStart.z + y * cellSize);
                    t.transform.localScale = 0.05f * Vector3.one;
                    Debug.Log("ima cell");
                }
            }
        }
    }

    
}
