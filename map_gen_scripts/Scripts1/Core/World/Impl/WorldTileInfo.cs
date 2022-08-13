using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTileInfo : MonoBehaviour, IWorldTileInfo
{
    public ITileResource Resource { get; set; }
    public ISettlement Settlement { get; set; }
    public IQuestArea QuestArea { get; set; }
    public IRoad Road { get; set; }
}
