using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Blackboard
{
    private static bool tileDestroyed = false;
    private static Tile tile;

    public static void DestroyTile(Tile t)
    {
        tile = t;
        tileDestroyed = true;
    }

    public static int[] wasTileDestoryed()
    {
        if (tileDestroyed)
        {
            tileDestroyed = false;
            int[] pos = tile.GetPosition();
            tile = null;
            return pos;
        }
        else
        {
            return null;
        }
    }
}
