using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class DemandManager : MonoBehaviour
{
    [SerializeField] private List<DemandSO> availableDemandTypes;
    [SerializeField] private float spawnInterval = 20f;

    [ShowInInspector]
    private List<Demand> _activeDemands = new List<Demand>();

    private float _timer;
    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= spawnInterval)
        {
            _timer = 0f;
            SpawnDemand();
        }
        UpdateDemand();

    }
    private void SpawnDemand()
    {
        var data = availableDemandTypes[Random.Range(0, availableDemandTypes.Count)];
        var demand = new Demand(data);
        EventManager<Demand>.TriggerEvent(EventType.OnDemandCreated, demand);
        _activeDemands.Add(demand);
    }
    private void UpdateDemand()
    {
        for (int i = _activeDemands.Count - 1; i >= 0; i--)
        {
            var d = _activeDemands[i];

            d.demandDuration -= Time.deltaTime;

            if (d.demandDuration <= 0)
            {
                RemoveDemand(i);
            }
        }
    }
    private void RemoveDemand(int index)
    {
        var demand = _activeDemands[index];
        _activeDemands.RemoveAt(index);
        EventManager<Demand>.TriggerEvent(EventType.OnDemandExpired, demand);
    }
    //Skill tree unlocks new demand types;
    private void UnlockNewDemandType(DemandSO demandSO)
    {
        if(!availableDemandTypes.Contains(demandSO))
        {
            availableDemandTypes.Add(demandSO);
        }
    }
}
