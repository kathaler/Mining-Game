using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    public Chunk GenerateChunk(Tile tile, Camera cam, int size, int[] bottom_right)
    {
        Tile[,] tiles = new Tile[size, size];
        for (int i = bottom_right[0]; i < size; i++)
        {
            for (int j = bottom_right[1]; j < size; j++)
            {
                int x = i;
                int y = j;
                var spawnedTile = Instantiate(tile,
                    new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {i} {j}";
                spawnedTile.transform.SetParent(transform);
                int[] pos = new int[] { x, y };
                spawnedTile.setPosition(pos);
                tiles[i,j] = spawnedTile;
            }
        }
        Chunk c = new Chunk(tiles, size, bottom_right);
        return c;
    }
}
