using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDirection
{
    Vector2Int GetPositionInDirection(Vector2Int sourcePosition);
    Vector2Int GetPositionInDirection(int sourceX, int sourceY);
    int GetBitMask();
}
