using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class TileManager : MonoBehaviour
{
    // Size of the viewport in tiles
    public int viewportWidth = 15;
    public int viewportHeight = 15;

    // Reference to the character object
    public Player player;

    public Tile tilePrefab;

    // Coordinates of the top-left tile in the viewport
    private int viewportBottom;
    private int viewportLeft;
 
    private TileStorage tiles = new TileStorage();
    private ArrayList activeTiles = new ArrayList();

    private void Start()
    {
        // Set the initial position of the viewport
        viewportBottom = (int)player.transform.position.y - viewportHeight / 2;
        viewportLeft = (int)player.transform.position.x - viewportWidth / 2;

        UpdateTiles();
        print(tiles.ToString());
    }

    private void Update()
    {
        if (Blackboard.PlayerMoved())
        {
            print("Current player location: " + player.GetX() + " , " + player.GetY());
            viewportBottom = (int)player.transform.position.y - viewportHeight / 2;
            viewportLeft = (int)player.transform.position.x - viewportWidth / 2;

            UpdateTiles();
        }
    }

    private void UpdateTiles()
    {
        //print("Bottom Left point:    " + viewportLeft + ", " + viewportBottom);
        for(int x = viewportLeft; x < viewportLeft + viewportWidth; x++)
        {
            for(int y = viewportBottom; y < viewportBottom + viewportHeight; y++)
            {
                if(y >= 0 && !tiles.HasTile(x,y))
                {
                    // Tile not loaded, so create a new one
                    Tile newTile = Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);

                    newTile.name = $"Tile {x} {y}";
                    newTile.transform.SetParent(transform);

                    newTile.setGlobalPosition(new Vector2(x, y));

                    tiles.AddTile(newTile);
                    activeTiles.Add(newTile);
                }
                else if(tiles.HasTile(x, y) && !tiles.GetTile(x,y).WasDestroyed())
                {
                    tiles.GetTile(x, y).Activate();
                }
            }
        }

        print(activeTiles.Count);

        foreach(Tile t in activeTiles)
        {
            int x = (int)t.GetGlobalPosition().x;
            int y = (int)t.GetGlobalPosition().y;

            if (t.IsOutOfView(this.player.transform.position))
            {
                tiles.GetTile(x, y).Deactivate();
            }
        }
    }
}
