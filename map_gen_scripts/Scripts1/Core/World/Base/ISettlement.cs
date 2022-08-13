using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISettlement
{
    ISettlementType SettlementType { get; }

    IReadOnlyList<IRace> MercenaryRaces { get; }
    IReadOnlyList<IClass> MercenaryClasses { get; }

    IReadOnlyList<Vector2Int> TilesPositions { get; }
    IReadOnlyList<IReadOnlyList<Vector2Int>> TilesPositionsByDistance { get; }

    Vector2Int Position { get; }
}
