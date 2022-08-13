using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New race", menuName = "Mercenary/MercanaryRace")]
public class Race : ScriptableObject, IRace
{
    [SerializeField]
    private string raceName;
    public string Name { get { return raceName; } }
    
    [SerializeField]
    private List<CharacteristicId> dependentCharacteristicsIds;
    [SerializeField]
    private List<int> characteristicModifiers;
    private Dictionary<CharacteristicId, int> characteristicModifiersDict;
    public IReadOnlyList<CharacteristicId> DependentCharacteristicsIds { get { return dependentCharacteristicsIds; } }

    [SerializeField]
    private List<Class> specialClasses;
    [SerializeField]
    private List<float> specialClassesPriority;
    private Dictionary<IClass, float> specialClassesPriorityDict;


    private bool isSetup;

    public int GetCharacteristicModifier(CharacteristicId characteristicId)
    {
        if (characteristicModifiersDict.ContainsKey(characteristicId))
            return characteristicModifiersDict[characteristicId];

        return 0;
    }

    public void Setup()
    {
        if(!isSetup)
        {
            isSetup = true;

            characteristicModifiersDict = new Dictionary<CharacteristicId, int>();
            for (int i = 0; i < dependentCharacteristicsIds.Count; ++i)
                characteristicModifiersDict.Add(dependentCharacteristicsIds[i], characteristicModifiers[i]);

            specialClassesPriorityDict = new Dictionary<IClass, float>();
            for (int i = 0; i < specialClasses.Count; ++i)
                specialClassesPriorityDict.Add(specialClasses[i], specialClassesPriority[i]);
        }
    }

    public float GetClassPriority(IClass mercenaryClass)
    {
        if (specialClassesPriorityDict.ContainsKey(mercenaryClass))
            return specialClassesPriorityDict[mercenaryClass];
        return 1;
    }
}
