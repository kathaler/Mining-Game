using System;
using System.Collections;
using System.Collections.Generic;

public class TileStorage
{
    private Dictionary<Tuple<int, int>, Tile> tiles;

    public TileStorage()
	{
        tiles = new Dictionary<Tuple<int, int>, Tile>();
    }

    public void AddTile(Tile tile)
    {
        Tuple<int, int> key = new Tuple<int, int>((int)tile.GetGlobalPosition().x, (int)tile.GetGlobalPosition().y);
        tiles[key] = tile;
    }

    public Tile GetTile(int x, int y)
    {
        Tuple<int, int> key = new Tuple<int, int>(x, y);
        if (tiles.TryGetValue(key, out Tile tile))
        {
            return tile;
        }
        return null;
    }

    public void RemoveTile(int x, int y)
    {
        if(HasTile(x,y))
        {
            tiles.Remove(new Tuple<int, int>(x, y));
        }
    }

    public void RemoveTile(Tile t)
    {
        int x = (int)t.GetGlobalPosition().x;
        int y = (int)t.GetGlobalPosition().y;
        if (HasTile(x, y))
        {
            tiles.Remove(new Tuple<int, int>(x, y));
        }
    }

    public Dictionary<Tuple<int, int>, Tile> GetTiles()
    {
        return tiles;
    }

    public bool HasTile(int x, int y)
    {
        Tuple<int, int> key = new Tuple<int, int>(x, y);
        if(tiles.ContainsKey(key))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetSize()
    {
        return tiles.Count;
    }

    override
    public String ToString()
    {
        String s = "";
        foreach (KeyValuePair<Tuple<int, int>, Tile> kvp in tiles)
        {
            //textBox3.Text += ("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            s += string.Format("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            s += " | ";
        }
        return s;
    }
}

