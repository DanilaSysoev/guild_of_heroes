using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settlement : MonoBehaviour, ISettlement
{
    private SettlementType settlementType;
    internal void SetSettlementType(SettlementType settlementType) { this.settlementType = settlementType; }
    public ISettlementType SettlementType { get { return settlementType; } }    

    private List<Race> mercenaryRaces;
    internal void SetMercenaryRaces(List<Race> mercenaryRaces) { this.mercenaryRaces = mercenaryRaces; }
    public IReadOnlyList<IRace> MercenaryRaces { get { return mercenaryRaces; } }

    private List<Class> mercenaryClasses;
    internal void SetMercenaryClasses(List<Class> mercenaryClasses) { this.mercenaryClasses = mercenaryClasses; }
    public IReadOnlyList<IClass> MercenaryClasses { get { return mercenaryClasses; } }

    private List<Vector2Int> tilesPositions;
    internal void SetTiles(List<Vector2Int> tiles) { this.tilesPositions = tiles; }
    public IReadOnlyList<Vector2Int> TilesPositions { get { return tilesPositions; } }
    
    private List<List<Vector2Int>> tilesPositionsByDistance;
    internal void SetTilesByDistance(List<List<Vector2Int>> tilesByDistance) { this.tilesPositionsByDistance = tilesByDistance; }
    public IReadOnlyList<IReadOnlyList<Vector2Int>> TilesPositionsByDistance { get { return tilesPositionsByDistance; } }

    private Vector2Int position;
    internal void SetPosition(Vector2Int position) { this.position = position; }
    public Vector2Int Position { get { return position; } }
}
