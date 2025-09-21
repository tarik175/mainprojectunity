using UnityEngine;

public class InventoryOLD : MonoBehaviour
{
    [field: SerializeField] public SerializableDictionary<Resource, int> Resources { get; set; }

    public int GetResourceCount(Resource type)
    {
        if (Resources.TryGetValue(type, out int currentCount))
        {
            return currentCount;
        }
        else
        {
            return 0;
        }
    }
    public int AddResources(Resource type, int count)
    {
        if (Resources.TryGetValue(type, out int CurrentCount))
        {
            return Resources[type] += count;
        }
        else
        {
            Resources.Add(type, count);
            return count;
        }
        
    }
}
