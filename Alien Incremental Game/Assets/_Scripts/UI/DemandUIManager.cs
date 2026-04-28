using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemandUIManager : MonoBehaviour
{
    [SerializeField] private GameObject parent;
    [SerializeField] private DemandUIItem demandUIPrefab;

    private Dictionary<Demand, DemandUIItem> demandUIDictionary = new();
    private void OnEnable()
    {
        EventManager<Demand>.Subscribe(EventType.OnDemandCreated, CreateDemandUI);
        EventManager<Demand>.Subscribe(EventType.OnDemandCompleted, RemoveDemandUI);
        EventManager<Demand>.Subscribe(EventType.OnDemandExpired, RemoveDemandUI);
    }

    private void OnDisable()
    {
        EventManager<Demand>.Unsubscribe(EventType.OnDemandCreated, CreateDemandUI);
        EventManager<Demand>.Unsubscribe(EventType.OnDemandCompleted, RemoveDemandUI);
        EventManager<Demand>.Unsubscribe(EventType.OnDemandExpired, RemoveDemandUI);
    }

    private void CreateDemandUI(Demand demand)
    {
        var item = Instantiate(demandUIPrefab, parent.transform);
        item.Initialize(demand);
        demandUIDictionary.Add(demand, item);

    }
    private void RemoveDemandUI(Demand demand)
    {
        if(demandUIDictionary.TryGetValue(demand, out var item))
        {
            Destroy(item.gameObject);
            demandUIDictionary.Remove(demand);
        }
    }
}
