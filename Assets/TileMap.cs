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
    // Start is called before the first frame update
    void Start()
    {
        myCam = Camera.main; 
        
        
    }

    public void OnGUI()
    {
        Vector3 mouseWorldPosition = myCam.ScreenToWorldPoint(Input.mousePosition); 
        GUI.Label(new Rect(50, 50, 600, 30), $"Mouse: {Input.mousePosition} In cell space: {myTilemap. WorldToCell(mouseWorldPosition)} "); 
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
