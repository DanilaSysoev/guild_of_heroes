using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceTable : MonoBehaviour
{
    [SerializeField]
    private List<int> experienceTable;

    public static ExperienceTable Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public int GetExpForLevel(int level)
    {
        if (level >= 1 && level < experienceTable.Count)
            return experienceTable[level];
        return -1;
    }
    public int GetLevelForExp(int exp)
    {
        for (int i = 1; i < experienceTable.Count; ++i)
            if (exp >= experienceTable[i - 1] && exp < experienceTable[i])
                return i - 1;
        return experienceTable.Count - 1;
    }
}
