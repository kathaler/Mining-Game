using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Blackboard
{
    private static bool tileDestroyed = false;
    private static Tile tile;

    private static bool playerMoved = true;

    public static void DestroyTile(Tile t)
    {
        tile = t;
        tileDestroyed = true;
    }

    public static Vector2 wasTileDestoryed()
    {
        if (tileDestroyed)
        {
            tileDestroyed = false;
            Vector2 pos = tile.GetGlobalPosition();
            tile = null;
            return pos;
        }
        else
        {
            return new Vector2();
        }
    }

    public static void MovePlayer()
    {
        playerMoved = true;
    }

    public static bool PlayerMoved()
    {
        bool temp = playerMoved;
        playerMoved = false;
        return temp;
    }
}
