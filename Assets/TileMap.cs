using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.Tilemaps;


public class TileMap : MonoBehaviour
{
    public Tilemap myTilemap;
    public Camera myCam;
    public TileBase testTileBase;
    public TileBase cellLive; 
    public int[,] multidimensionalMap = new int[25, 25];
    public int[,] TempMap = new int[25, 25];
    // Start is called before the first frame update
    void Start()
    {
        myCam = Camera.main; 

        for(int y = 0; y < multidimensionalMap.GetLength(1); y++) 
        {
            for(int x = 0; x < multidimensionalMap.GetLength(0); x++)
            {               
                
                    multidimensionalMap[x, y] = Random.Range(0, 2);
                TempMap[x, y] = multidimensionalMap[x, y]; 
            }
        }

        DrawToTilemap();
    }

    public void OnGUI()
    {
        Vector3 mouseWorldPosition = myCam.ScreenToWorldPoint(Input.mousePosition); 
        GUI.Label(new Rect(50, 50, 600, 30), $"Mouse: {Input.mousePosition} In cell space: {myTilemap. WorldToCell(mouseWorldPosition)} "); 
    }


    void DrawToTilemap()
    {
        for (int y = 0; y < multidimensionalMap.GetLength(1); y++)
        {
            for (int x = 0; x < multidimensionalMap.GetLength(0); x++)
            {
                if (multidimensionalMap[x, y] == 0)
                myTilemap.SetTile(new Vector3Int(x, y, 0), null);
                else
                    myTilemap.SetTile(new Vector3Int(x, y, 0), cellLive);
            }
        }
    }

    int CountNeighbouringCells(int x, int y, int[,] map) 
    {
        int count = 0;

        for (int b = y - 1; b < y + 2; b++)
        {
            for (int a = x - 1; a < x + 2; a++)
            {

                if (a < 0 || b < 0 || a >= multidimensionalMap.GetLength(0) || b >= multidimensionalMap.GetLength(1) || (a == x && b == y))
                    continue;
                Debug.Log($"Checking position x {a} and y {b}. Position is {map[a,b]}. Is position 1? {map[a, b] == 1}");
                if (map[a, b] == 1)
                {
                    count++;
                    Debug.Log($"Position x {a} and y {b}. value is 1! counting! Count is now {count}");
                }
            }
        }

        return count; 
    }

    // LIVE CELL AND NEIGHBOURS COUNT < 2 --> dead cell
    // LIVE CELL AND NEIGHBOURS COUNT == 2 or == 3 --> Do nothing
    // LIVE CELL AND NEIGHBOURS COUNT > 3 --> dead cell
    // DEAD CELL AND NEIGHBOURS COUNT == 3 --> live cell

    // Update is called once per frame
    int should_advance = 0;
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            should_advance++;
        } else
        {
            return;
        }     


        for(int j = 0; j < multidimensionalMap.GetLength(1); j++)
        {
            for (int i = 0; i < multidimensionalMap.GetLength(0); i++)
            {
                if (multidimensionalMap[i, j] == 1)
                {
                    if (CountNeighbouringCells(i, j, multidimensionalMap) < 2 || CountNeighbouringCells(i, j, multidimensionalMap) > 3) 
                        {
                        TempMap[i, j] = 0;   //The cell dies
                        }

                }
                else 
                {
                    if(CountNeighbouringCells(i, j, multidimensionalMap) == 3)
                    {
                        TempMap[i, j] = 1; // A live cell is born
                    }
                }                

            }
        }

        for (int j = 0; j < multidimensionalMap.GetLength(1); j++)
        {
            for (int i = 0; i < multidimensionalMap.GetLength(0); i++)
            {
                multidimensionalMap[i, j] = TempMap[i, j]; 
            }

        }


        DrawToTilemap();
    }
}
