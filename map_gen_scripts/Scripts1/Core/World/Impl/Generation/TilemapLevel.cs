using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "TilemapLevel")]
public class TilemapLevel : ScriptableObject, ITilemapLevel
{
    [SerializeField]
    private string levelName;
    public string LevelName { get { return levelName; } }

    [SerializeField]
    private int relativeHeight;
    public int RelativeHeight { get { return relativeHeight; } }
}
