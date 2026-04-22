using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemandManager : MonoBehaviour
{
    [SerializeField] private List<DemandSO> availableDemands;
    [SerializeField] private float spawnInterval = 5f;

    private List<Demand> _activeDemands = new List<Demand>();

    private float timer;
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnDemand();
        }
        UpdateDemand();

    }
    private void SpawnDemand()
    {
        var data = availableDemands[Random.Range(0, availableDemands.Count)];
        var demand = new Demand(data);

        _activeDemands.Add(demand);
    }
    private void UpdateDemand()
    {
        for (int i = _activeDemands.Count - 1; i >= 0; i--)
        {
            var d = _activeDemands[i];

            d.demandDuration -= Time.deltaTime;

            if (d.IsCompleted)
            {
                CompleteDemand(d);
                _activeDemands.RemoveAt(i);
            }
            else if (d.demandDuration <= 0)
            {
                Debug.Log("Demand Failed");
                _activeDemands.RemoveAt(i);
            }
        }
    }
    private void CompleteDemand(Demand demand)
    {

    }

    private void UnlockDemand()
    {

    }
}
