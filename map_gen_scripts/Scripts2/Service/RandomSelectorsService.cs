using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class RandomSelectorsService
{
    /// <summary>
    /// Осуществляет выбор случайной сущности из списка, используя список весов
    /// </summary>
    /// <typeparam name="EntityType">Тип сущности</typeparam>
    /// <param name="entities">Список сущностей</param>
    /// <param name="weights">Веса сущностей</param>
    /// <returns>Выбраная сущность</returns>
    public static EntityType GetEntityByWeights<EntityType>(List<EntityType> entities, List<int> weights)
    {
        int sum = weights.Sum();
        int rValue = UnityEngine.Random.Range(0, sum);

        int buff = 0;
        for (int i = 0; i < entities.Count; ++i)
        {
            if (buff <= rValue && rValue < buff + weights[i])
                return entities[i];

            buff += weights[i];
        }
        return entities[entities.Count - 1];
    }

    /// <summary>
    /// Выбор для сущностей случайных значений
    /// с использованием селекторов
    /// </summary>
    /// <typeparam name="EntityType">Тип сущности</typeparam>
    /// <param name="entities">Список сущностей</param>
    /// <param name="minValues">Список минимальных значений</param>
    /// <param name="maxValues">Список максимальных значений</param>
    /// <param name="rangeSelectors">Список селекторов из диапазона</param>
    /// <returns>Словарь сущностей со значениями</returns>
    public static Dictionary<EntityType, int> GetEntitiesWithValue<EntityType>(
        List<EntityType> entities, 
        List<int> minValues,
        List<int> maxValues,
        List<FromRangeRandomSelector> rangeSelectors)
    {
        Dictionary<EntityType, int> result = new Dictionary<EntityType, int>();
        for(int i = 0; i < entities.Count; ++i)
        {
            result.Add(entities[i], rangeSelectors[i].CalculateValue(minValues[i], maxValues[i]));
        }
        return result;
    }
}
