using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Атрибут классовой модификации скила
/// </summary>
[Serializable]
public class ClassSkillModificationRuleAttribute
{
    [SerializeField]
    [Tooltip("Модификатор скила")]
    private SkillModifier skillModifier;
    /// <summary>
    /// Модификатор скила
    /// </summary>
    public SkillModifier SkillModifier { get { return skillModifier; } }

    [SerializeField]
    [Tooltip("Вес модификатора (чем больше, тем выше шанс выпадения модификатора)")]
    private int weight;
    /// <summary>
    /// Вес модификатора (чем больше, тем выше шанс выпадения модификатора)
    /// </summary>
    public int Weight { get { return weight; } }

    /// <summary>
    /// Метод для автоматической настройки. 
    /// Вызывается генератором атрибутов.
    /// </summary>
    /// <param name="skillModifier">Модификатор</param>
    /// <param name="weight">Вес</param>
    public void Set(SkillModifier skillModifier, int weight = 1)
    {
        this.skillModifier = skillModifier;
        this.weight = weight;
    }
}

/// <summary>
/// Правило классовой модификации скилов.
/// Отдает предпочтения расовым бонусам.
/// Шанс скила с расовым штрафом ниже.
/// </summary>
[Serializable]
[CreateAssetMenu(fileName = "newClassModificationRule", menuName = "New/Class modificatio rule")]
public class ClassSkillModificationRule : ScriptableObject
{
    /// <summary>
    /// Коэффициент положительного расового модификатора.
    /// Применяется, если есть расовый бонус в данном скиле.
    /// Умножается на максимальное значение расового модификатора.
    /// Результат добавляется к весу.
    /// </summary>
    [SerializeField]
    [Tooltip("Коэффициент положительного расового модификатора.\n" +
             "Применяется, если есть расовый бонус в данном скиле.\n" +
             "Умножается на максимальное значение расового модификатора.\n" +
             "Результат добавляется к весу.")]
    float racePositiveEffectCoeff = 1;
    /// <summary>
    /// Коэффициент отрицательного расового модификатора.
    /// Применяется, если есть расовый штраф в данном скиле.
    /// Умножается на минимальное значение расового модификатора.
    /// Результат добавляется к весу.
    /// </summary>
    [SerializeField]
    [Tooltip("Коэффициент отрицательного расового модификатора.\n" +
             "Применяется, если есть расовый штраф в данном скиле.\n" +
             "Умножается на минимальное значение расового модификатора.\n" +
             "Результат добавляется к весу.")]
    float raceNegativeEffectCoeff = 2;

    [SerializeField]
    private PersonClass personClass;
    /// <summary>
    /// Класс персонажа
    /// </summary>
    public PersonClass Class { get { return personClass; } }

    /// <summary>
    /// Модификаторы скиллов с весами.
    /// Чем больше вес, тем выше шанс получить модификатор
    /// </summary>
    [SerializeField]
    private List<ClassSkillModificationRuleAttribute> modifiers;
    /// <summary>
    /// Количество модификаторов, применяемых для данноо класса согласно данному правилу
    /// </summary>
    [SerializeField]
    int modifiersCount;

    /// <summary>
    /// Выбор модификаторов
    /// </summary>
    /// <param name="raceAttr">Применяемые расовые модификаторы</param>
    /// <returns>Значения модификаторов скилов (пары скил-модификатор)</returns>
    public Dictionary<Skill, int> GetModifiers(RaceSkillModificationAttribute raceAttr)
    {
        // Если расовые модификаторы есть - берем из, иначе юзаем пустой список
        /////////////////////////////////////////////////////////////////////////
        var raceModList = raceAttr != null ? raceAttr.SkillModifiers.ToList() : new List<SkillModifier>();
        /////////////////////////////////////////////////////////////////////////

        Dictionary<Skill, int> res = new Dictionary<Skill, int>();

        // Копируем атрибуды модификации, т.к. список будет изменяться
        ////////////////////////////////////////////////////////////////
        List<ClassSkillModificationRuleAttribute> mods = new List<ClassSkillModificationRuleAttribute>(modifiers);
        ////////////////////////////////////////////////////////////////

        // Выбираем модификаторы в количестве modifiersCount
        /////////////////////////////////////////////////////
        for (int i = 0; i < modifiersCount; ++i)
        {
            // Для этого в отдельные списки кладем модификаторы и их веса
            // и производим корректировку весов в соответствии с расовыми модификаторами
            //////////////////////////////////////////////////////////////////////////////
            var sMods = mods.Select(m => m.SkillModifier).ToList();
            var sWeights = mods.Select(m => m.Weight).ToList();
            WeightsCorrection(raceModList, sMods, sWeights);
            //////////////////////////////////////////////////////////////////////////////

            // Выбираем модификатор, используя механизм выбора случайного взвешенного
            // и добавляем его к списку модификаторов.
            // Удаляем отобраный модификатор.
            ///////////////////////////////////////////////////////////////////////////
            var mod = RandomSelectorsService.GetEntityByWeights<SkillModifier>(sMods, sWeights);
            mods.RemoveAll(csmra => csmra.SkillModifier == mod);
            res.Add(mod.Skill, mod.GetNextModifierValue());
            ///////////////////////////////////////////////////////////////////////////
        }
        /////////////////////////////////////////////////////

        return res;
    }

    /// <summary>
    /// Корректирует веса модификаторов скилов в соответствии с расовыми модификаторами
    /// </summary>
    /// <param name="raceModList">Список расовых модификаторов</param>
    /// <param name="sMods">Список модификаторов скилов</param>
    /// <param name="sWeights">Список весов модификаторов скилов</param>
    private void WeightsCorrection(List<SkillModifier> raceModList, List<SkillModifier> sMods, List<int> sWeights)
    {
        // Для всех модификаторов скилов производим коррекцию весов
        /////////////////////////////////////////////////////////////
        for (int k = 0; k < sMods.Count; ++k)  // корректируем веса модификаторов в зависимости от расовых модификаторов. Расовые модификаторы получают приоритет
        {
            // Выбираем расовый модификатор текущего скила
            ////////////////////////////////////////////////
            var rsm = raceModList.Find(sm => sm.Skill == sMods[k].Skill);
            ////////////////////////////////////////////////
            
            // Если такой нашелся
            ////////////////////////////////////////////////
            if (rsm != null)
            {
                // Добавляем к весу РАСОВЫЙ МОДИФИКАТОР, умноженный на коэффициент.
                // Если модификатор положительный - берем положительный коэффициент,
                // иначе - отрицательный
                // Таким образом, чем больше расовый бонус, 
                // тем выше шанс получить модификатор скила.
                /////////////////////////////////////////////////////////////////////
                if (rsm.IsPositive)
                    sWeights[k] += (int)(rsm.MaxModifierValue * racePositiveEffectCoeff);
                else
                    sWeights[k] += (int)(rsm.MinModifierValue * raceNegativeEffectCoeff);
                /////////////////////////////////////////////////////////////////////
            }
            ////////////////////////////////////////////////
        }
        /////////////////////////////////////////////////////////////
        
        // Если получились отрицательные веса
        // корректируем, загоняя их в 1.
        // Т. о. модификаторов с весом <= 0 не будет
        // и шанс есть для всех в списке.
        //////////////////////////////////////////////
        int minW = sWeights.Min();
        if (minW < 0)
            for (int k = 0; k < sWeights.Count; ++k)
                sWeights[k] += minW + 1;
        //////////////////////////////////////////////
    }

    /// <summary>
    /// Метод для Editor
    /// Загружает модификаторы скилов из базы ассетов (по префиксу в имени)
    /// и создает на их основе атрибут для модификации.
    /// </summary>
    [ContextMenu("Set default")]
    public void SetDefaultSkillModifiers()
    {
        modifiers = new List<ClassSkillModificationRuleAttribute>();

        var modsGUIDs = AssetDatabase.FindAssets(Class.ClassName + " t: SkillModifier");
        foreach(var m in modsGUIDs)
        {
            var mod = AssetDatabase.LoadAssetAtPath<SkillModifier>(AssetDatabase.GUIDToAssetPath(m));
            ClassSkillModificationRuleAttribute csma = new ClassSkillModificationRuleAttribute();
            csma.Set(mod);
            modifiers.Add(csma);
        }
    }
}
