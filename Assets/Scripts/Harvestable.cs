using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class Harvestable : MonoBehaviour
{
    [field: SerializeField] public ToolType HarvestingType { get; private set; }
    [field: SerializeField] public int ResourceCount { get; private set; }
    [field: SerializeField] public ParticleSystem ResourceEmitPS { get; private set; }
    [field: SerializeField] public GameObject EffectOnDestroyPrefab { get; private set; }
    private int _amountHarvested = 0;

    public bool TryHarvest(ToolType harvestingType, int amount)
    {
        if (harvestingType == HarvestingType)
        {
            Harvest(amount);
            return true;
        }
        else
        {
            return false; 
        }
        
    }

    private void Harvest(int amount)
    {
        int amountToSpawn = Mathf.Min(amount, ResourceCount - _amountHarvested);

        if (amountToSpawn > 0)
        {
            ResourceEmitPS.Emit(amountToSpawn);
            _amountHarvested += amountToSpawn;
        }
        if (_amountHarvested >= ResourceCount)
        {
            if (EffectOnDestroyPrefab)
            {
                Instantiate(EffectOnDestroyPrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }

    }
}
