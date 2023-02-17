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

        if(x+1 < chunk_size && tiles[x+1, y] != null)
        {
            tiles[x+1, y].setFace(4);
        }
        if (x - 1 >= 0 && tiles[x - 1, y] != null)
        {
            tiles[x-1, y].setFace(5);
        }
        if (y + 1 < chunk_size && tiles[x, y+1] != null)
        {
            if (tiles[x, y + 1].getFace() != 0)
            {
                if (tiles[x - 1, y + 1] != null)
                {
                    tiles[x, y + 1].setFace(8);
                }
                else
                {
                    tiles[x, y + 1].setFace(6);
                }
            }
            else
            {
                tiles[x, y + 1].setFace(7);
            }
        }
        if (y - 1 >= 0 && tiles[x, y-1] != null)
        {
            if(tiles[x, y - 1].getFace() != 0)
            {
                if (tiles[x-1,y-1] != null)
                {
                    tiles[x, y - 1].setFace(3);
                }
                else
                {
                    tiles[x, y - 1].setFace(1);
                }
            }
            else
            {
                tiles[x, y - 1].setFace(2);
            }
        }

    }
}
