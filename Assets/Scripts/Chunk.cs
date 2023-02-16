using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] private Tile tile;

    public ArrayList GenerateChunk(int w, int h, Camera cam)
    {
        var tiles = new ArrayList();
        for (int i = 0; i < 64; i++)
        {
            for (int j = 0; j < 64; j++)
            {

                int x = i - (int)(cam.aspect * cam.orthographicSize);
                int y = j - (int)cam.orthographicSize;
                var spawnedTile = Instantiate(tile,
                    new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                spawnedTile.transform.SetParent(transform);
                tiles.Add(spawnedTile);

            }
        }
        return tiles;
    }
}
