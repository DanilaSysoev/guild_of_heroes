using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWorldTileInfo
{    
    ITileResource Resource { get; set; }

    ISettlement Settlement { get; set; }
    IQuestArea QuestArea { get; set; }
    IRoad Road { get; set; }
}