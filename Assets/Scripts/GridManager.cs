using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int _width, _height;
    [SerializeField] private Camera _cam;
    private int cam_x;
    private int cam_y;
    public int chunk_size;
    public ChunkGenerator chunkGenerator;
    private Chunk start_chunk;
    public Tile tile;

    private void Start()
    {
        cam_x = (int)(_cam.transform.position.x);
        cam_y = (int)(_cam.transform.position.y);
        start_chunk = chunkGenerator.GenerateChunk(tile, _cam, chunk_size, new int[] { 0, 0 });
    }

    private void Update()
    {
        int[] pos = Blackboard.wasTileDestoryed();
        if(pos != null)
        {
            UpdateChunk(start_chunk, pos);
        }
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 20), "Hit me!"))
        {
            Print_Chunk(start_chunk.GetTiles());
        }
    }

    private void Print_Chunk(Tile[,] chunk)
    {
        for (int i = 0; i < chunk_size; i++)
        {
            string s = "";
            for (int j = 0; j < chunk_size; j++)
            {
                s += chunk[i, j] + " ";
            }
            print(s + "\n");
        }
    }

    private void UpdateChunk(Chunk chunk, int[] pos)
    {
        Tile[,] tiles = chunk.GetTiles();
        int x = pos[0];
        int y = pos[1];

        print("Tile destroyed: " + x + ", " + y);

        if (withinBounds(x+1) && tiles[x + 1, y] != null)
        {
            print("Checking RIGHT tile: ");
            checkSurroundingTile(x + 1, y, tiles);
        }
        if (withinBounds(x-1) && tiles[x - 1, y] != null)
        {
            print("\nChecking LEFT tile: ");
            checkSurroundingTile(x - 1, y, tiles);
        }
        if (withinBounds(y+1) && tiles[x, y + 1] != null)
        {
            print("\nChecking UP tile: ");
            checkSurroundingTile(x, y + 1, tiles);
        }
        if (withinBounds(y-1) && tiles[x, y - 1] != null)
        {
            print("\nChecking DOWN tile: ");
            checkSurroundingTile(x, y - 1, tiles);
        }

    }

    // Focuses on one of the tiles that is around the destroyed tile
    // Checks that tile for it's surround tiles
    // puts all 4 tiles in an array
    private void checkSurroundingTile(int x, int y, Tile[,] tiles)
    {
        Tile up = null;
        Tile down = null;
        Tile left = null;
        Tile right = null;
        if (withinBounds(y + 1))
        {
            up = tiles[x, y + 1];
        }
        if (withinBounds(y - 1))
        {
            down = tiles[x, y - 1];
        }
        if (withinBounds(x + 1))
        {
            right = tiles[x+1, y];
        }
        if (withinBounds(x - 1))
        {
            left = tiles[x-1, y];
        }

        Tile[] surroundingTiles = new Tile[] { up, down, left, right };

        int sum = 0;
        for(int i = 0; i < 4; i++)
        {
            if (surroundingTiles[i] == null)
            {
                sum += (int)Mathf.Pow(3, i);
                print("tile: " + i + " adding: " + sum);
            }
            else
            {
                print(surroundingTiles[i].name);
            }
        }
        assignFaceToTile(tiles[x, y], sum);
    }

    private bool withinBounds(int i)
    {
        return i >= 0 && i < chunk_size;
    }

    private void assignFaceToTile(Tile t, int value)
    {
        switch (value)
        {
            case 0:
                t.setFace(0);
                break;
            case 1:
                // up
                t.setFace(1);
                break;
            case 3:
                // down
                t.setFace(7);
                break;
            case 9:
                // left
                t.setFace(3);
                break;
            case 10:
                // up left
                t.setFace(0);
                break;
            case 12:
                // down left
                t.setFace(6);
                break;
            case 27:
                // right
                t.setFace(5);
                break;
            case 28:
                // up right
                t.setFace(2);
                break;
            case 30:
                // down right
                t.setFace(8);
                break;
            default:
                print("ERROR, no case was found");
                break;
        }
    }
}
