using System;
public class Chunk
{
	private Tile[,] tiles;
	private int size;
	private int[] bottom_right;
	public Chunk(Tile[,] tiles, int size, int[] bottom_right)
	{
		this.tiles = tiles;
		this.size = size;
		this.bottom_right = bottom_right;
	}

	public Tile[,] GetTiles()
	{
		return tiles;
	}
}

