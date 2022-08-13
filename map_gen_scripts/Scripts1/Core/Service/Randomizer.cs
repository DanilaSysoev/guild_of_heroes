using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Randomizer
{
    public static T GetItemFromRow<T>(IReadOnlyList<T> items, IReadOnlyList<float> weights)
    {
        float sum = 0;
        float val = Random.Range(0, weights.Sum());

        //if (items.Count != weights.Count)
        //    Debug.Log("Oops!");

        for(int i = 0; i < items.Count; ++i)
        {
            if (val >= sum && val <= sum + weights[i])
                return items[i];
            sum += weights[i];
        }

        return items[items.Count - 1];
    }

    public static List<T> GetUniqueSetFromRow<T>(IReadOnlyList<T> items, IReadOnlyList<float> weights, int count)
    {
        List<T> res = new List<T>();

        var itemsCopy = new List<T>(items);
        var weightsCopy = new List<float>(weights);

        for (int i = 0; i < count; ++i)
        {
            T item = Randomizer.GetItemFromRow<T>(itemsCopy, weightsCopy);
            res.Add(item);
            weightsCopy.RemoveAt(itemsCopy.IndexOf(item));
            itemsCopy.Remove(item);
        }

        return res;
    }
    public static List<T> GetMultipleSetFromRow<T>(IReadOnlyList<T> items, IReadOnlyList<float> weights, int count)
    {
        List<T> res = new List<T>();

        for (int i = 0; i < count; ++i)
        {
            T item = Randomizer.GetItemFromRow<T>(items, weights);
            res.Add(item);
        }

        return res;
    }
}
