using System;
using UnityEngine;

public static class MapBuilder
{
     
    public static Tile[,] BuildTileMap(Vector3 origin, Vector3 bottomRight, float TileSize)
    {
        float width = Math.Abs(bottomRight.x - origin.x);
        float height = Math.Abs(bottomRight.z - origin.z);

        int cols = (int) (width / TileSize);
        int rows = (int) (height / TileSize);
        
        Vector3 XBasis = new Vector3(1,0,0);
        Vector3 YBasis = new Vector3(0,0,-1);

        Tile[,] tiles = new Tile[cols, rows];

        for (int x = 0; x < cols; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                Vector3 OffsetX = x * TileSize * XBasis;
                Vector3 OffsetY = y * TileSize * YBasis;

                Vector3 ActualPhysicalLocation = origin + OffsetX + OffsetY;
                Tile t = new Tile(x, y);
                t.TopLeft = ActualPhysicalLocation;
                tiles[x, y] = t;
            }
        }

        return tiles;
    }
}
