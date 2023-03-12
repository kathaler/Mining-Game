using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    public Chunk GenerateChunk(Tile tile, Camera cam, int size, int[] bottom_right)
    {
        Tile[,] tiles = new Tile[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                int x = i + bottom_right[0];
                int y = j + bottom_right[1];

                var spawnedTile = Instantiate(tile,
                    new Vector3(x, y), Quaternion.identity);

                spawnedTile.name = $"Tile {i} {j}";
                spawnedTile.transform.SetParent(transform);

                //spawnedTile.setLocalPosition(new int[] { i, j });
                spawnedTile.setGlobalPosition(new Vector2(x,y));

                tiles[i,j] = spawnedTile;
            }
        }
        Chunk c = new Chunk(tiles, size, bottom_right);
        return c;
    }
}
