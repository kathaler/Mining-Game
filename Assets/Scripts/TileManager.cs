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
    }

    private void Update()
    {
        if (Blackboard.PlayerMoved())
        {
            viewportBottom = (int)player.transform.position.y - viewportHeight / 2;
            viewportLeft = (int)player.transform.position.x - viewportWidth / 2;

            UpdateTiles();
        }

        Vector2 pos = Blackboard.wasTileDestoryed();
        if(pos.y >= 0)
        {
            UpdateSurroundingTiles(pos);
        }
    }

    private void UpdateSurroundingTiles(Vector2 pos)
    {
        int x = (int)pos.x;
        int y = (int)pos.y;

        if(tiles.HasTile(x+1,y))
        {
            CheckTile(x + 1, y);
        }
        if (tiles.HasTile(x - 1, y))
        {
            CheckTile(x - 1, y);
        }
        if (tiles.HasTile(x, y + 1))
        {
            CheckTile(x, y + 1);
        }
        if (tiles.HasTile(x, y - 1))
        {
            CheckTile(x, y - 1);
        }

    }

    private void CheckTile(int x, int y)
    {
        Tile up = null;
        Tile down = null;
        Tile left = null;
        Tile right = null;

        if (tiles.HasTile(x + 1, y))
        {
            right = tiles.GetTile(x + 1, y);
        }
        if (tiles.HasTile(x - 1, y))
        {
            left = tiles.GetTile(x - 1, y);
        }
        if (tiles.HasTile(x, y + 1))
        {
            up = tiles.GetTile(x, y + 1);
        }
        if (tiles.HasTile(x, y - 1))
        {
            down = tiles.GetTile(x, y - 1);
        }

        Tile[] surroundingTiles = new Tile[] { up, down, left, right };
        int sum = 0;
        for (int i = 0; i < 4; i++)
        {
            if (surroundingTiles[i] == null || surroundingTiles[i].WasDestroyed())
            {
                sum += (int)Mathf.Pow(3, i);
            }
            else
            {
                //print(surroundingTiles[i].name);
            }
        }
        AssignFaceToTile(tiles.GetTile(x, y), sum);
    }

    private void AssignFaceToTile(Tile t, int value)
    {
        switch (value)
        {
            case 0:
                t.setFace(0);
                break;
            case 1:
                // up
                t.setFace(8);
                break;
            case 3:
                // down
                t.setFace(1);
                break;
            case 4:
                // up down
                t.setFace(9);
                break;
            case 9:
                // left
                t.setFace(5);
                break;
            case 10:
                // up left
                t.setFace(13);
                break;
            case 12:
                // down left
                t.setFace(2);
                break;
            case 13:
                // up down left
                t.setFace(10);
                break;
            case 27:
                // right
                t.setFace(7);
                break;
            case 28:
                // up right
                t.setFace(15);
                break;
            case 30:
                // down right
                t.setFace(4);
                break;
            case 31:
                // up down right
                t.setFace(12);
                break;
            case 36:
                // left right
                t.setFace(6);
                break;
            case 37:
                // up left right
                t.setFace(14);
                break;
            case 39:
                // down left right
                t.setFace(3);
                break;
            case 40:
                // up down left right
                t.setFace(11);
                break;
            default:
                print("ERROR, no case was found");
                break;
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
