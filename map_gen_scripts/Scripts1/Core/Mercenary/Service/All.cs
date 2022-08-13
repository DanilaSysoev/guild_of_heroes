using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class All : MonoBehaviour
{
    [SerializeField]
    private List<CharacteristicId> characteristics;
    [SerializeField]
    private List<SkillId> skills;
    [SerializeField]
    private List<AttributeId> attributes;

    [SerializeField]
    private List<Race> races;
    [SerializeField]
    private List<Class> classes;

    public static IReadOnlyList<CharacteristicId> Characteristics { get; private set; }
    public static IReadOnlyList<SkillId> Skills { get; private set; }
    public static IReadOnlyList<AttributeId> Attributes { get; private set; }

    public IReadOnlyList<Race> Races { get; private set; }
    public IReadOnlyList<Class> Classes { get; private set; }

    private void Awake()
    {
        Characteristics = characteristics;
        Skills = skills;
        Attributes = attributes;

        Races = races;
        Classes = classes;
    }
}
