using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Direction
{
    private static NorthWestDirection northWest;
    public static IDirection NorthWest { get { return northWest; } }
    private static NorthEastDirection northEast;
    public static IDirection NorthEast { get { return northEast; } }
    private static EastDirection east;
    public static IDirection East { get { return east; } }
    private static SouthEastDirection southEast;
    public static IDirection SouthEast { get { return southEast; } }
    private static SouthWestDirection southWest;
    public static IDirection SouthWest { get { return southWest; } }
    private static WestDirection west;
    public static IDirection West { get { return west; } }

    private static List<IDirection> directions;
    public static IReadOnlyList<IDirection> Directions
    {
        get
        {
            return directions;
        }
    }

    public static IDirection GetDirection(Vector2Int from, Vector2Int to)
    {
        if (northWest.GetPositionInDirection(from) == to)
            return northWest;
        if (northEast.GetPositionInDirection(from) == to)
            return northEast;
        if (east.GetPositionInDirection(from) == to)
            return east;
        if (southEast.GetPositionInDirection(from) == to)
            return southEast;
        if (southWest.GetPositionInDirection(from) == to)
            return southWest;
        if (west.GetPositionInDirection(from) == to)
            return west;

        return null;
    }

    static Direction()
    {
        northWest = new NorthWestDirection();
        northEast = new NorthEastDirection();
        east = new EastDirection();
        southEast = new SouthEastDirection();
        southWest = new SouthWestDirection();
        west = new WestDirection();

        FillDirections();
    }

    private static void FillDirections()
    {
        directions = new List<IDirection>();
        directions.Add(northWest);
        directions.Add(northEast);
        directions.Add(east);
        directions.Add(southEast);
        directions.Add(southWest);
        directions.Add(west);
    }

    private class NorthWestDirection : IDirection
    {
        public int GetBitMask()
        {
            return 1 << 5;
        }
        public Vector2Int GetPositionInDirection(Vector2Int sourcePosition)
        {
            return new Vector2Int(sourcePosition.x - 1 + sourcePosition.y % 2, sourcePosition.y + 1);
        }
        public Vector2Int GetPositionInDirection(int sourceX, int sourceY)
        {
            return GetPositionInDirection(new Vector2Int(sourceX, sourceY));
        }
    }
    private class NorthEastDirection : IDirection
    {
        public int GetBitMask()
        {
            return 1 << 4;
        }
        public Vector2Int GetPositionInDirection(Vector2Int sourcePosition)
        {
            return new Vector2Int(sourcePosition.x + sourcePosition.y % 2, sourcePosition.y + 1);
        }
        public Vector2Int GetPositionInDirection(int sourceX, int sourceY)
        {
            return GetPositionInDirection(new Vector2Int(sourceX, sourceY));
        }
    }
    private class EastDirection : IDirection
    {
        public int GetBitMask()
        {
            return 1 << 3;
        }
        public Vector2Int GetPositionInDirection(Vector2Int sourcePosition)
        {
            return new Vector2Int(sourcePosition.x + 1, sourcePosition.y);
        }
        public Vector2Int GetPositionInDirection(int sourceX, int sourceY)
        {
            return GetPositionInDirection(new Vector2Int(sourceX, sourceY));
        }
    }
    private class SouthEastDirection : IDirection
    {
        public int GetBitMask()
        {
            return 1 << 2;
        }
        public Vector2Int GetPositionInDirection(Vector2Int sourcePosition)
        {
            return new Vector2Int(sourcePosition.x + sourcePosition.y % 2, sourcePosition.y - 1);
        }
        public Vector2Int GetPositionInDirection(int sourceX, int sourceY)
        {
            return GetPositionInDirection(new Vector2Int(sourceX, sourceY));
        }
    }
    private class SouthWestDirection : IDirection
    {
        public int GetBitMask()
        {
            return 1 << 1;
        }
        public Vector2Int GetPositionInDirection(Vector2Int sourcePosition)
        {
            return new Vector2Int(sourcePosition.x - 1 + sourcePosition.y % 2, sourcePosition.y - 1);
        }
        public Vector2Int GetPositionInDirection(int sourceX, int sourceY)
        {
            return GetPositionInDirection(new Vector2Int(sourceX, sourceY));
        }
    }
    private class WestDirection : IDirection
    {
        public int GetBitMask()
        {
            return 1;
        }
        public Vector2Int GetPositionInDirection(Vector2Int sourcePosition)
        {
            return new Vector2Int(sourcePosition.x - 1, sourcePosition.y);
        }
        public Vector2Int GetPositionInDirection(int sourceX, int sourceY)
        {
            return GetPositionInDirection(new Vector2Int(sourceX, sourceY));
        }
    }
}
