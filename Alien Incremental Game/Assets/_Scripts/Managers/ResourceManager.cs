using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurrencyType
{
    CleanMoney,
    DirtyMoney
}
public class ResourceManager : MonoBehaviour
{
    private List<GameObject> _activeSupplyType;

    private Dictionary<SupplyType, int> _supplyCounts;

    private int _currentMoney;
    private int _currentDirtyMoney;

    public int CurrentMoney => _currentMoney;
    public int CurrentDirtyMoney => _currentDirtyMoney;

    private void OnEnable()
    {
        EventManager<Demand>.Subscribe(EventType.OnDemandCompleted, AddCurrency);
    }
    private void OnDisable()
    {
        EventManager<Demand>.Unsubscribe(EventType.OnDemandCompleted, AddCurrency);
    }
    public void AddSupply(Supply supply, int amount)
    {
        if (!_supplyCounts.ContainsKey(supply.SupplyData.supplyType))
        {
            _supplyCounts[supply.SupplyData.supplyType] = 0;
        }

        _supplyCounts[supply.SupplyData.supplyType] += amount;
    }
    public void AddCurrency(Demand demand)
    {
        if(_supplyCounts.TryGetValue(demand.demandSO.supplyType, out int currentCount) && currentCount >= demand.demandSO.baseRewardAmount)
        {
            _supplyCounts[demand.demandSO.supplyType] -= demand.demandSO.baseSupplyDemandAmount;

            int reward = CalculateMoneyReward(demand.demandSO.supplyType);
            switch (demand.demandSO.currencyType)
            {
                case CurrencyType.CleanMoney:
                    _currentMoney += reward;
                    break;
                case CurrencyType.DirtyMoney:
                    _currentDirtyMoney += reward;
                    break;
            }

        }

    }
    public void SpendCurrency(CurrencyType currencyType, int amount)
    {
        switch(currencyType)
        {
            case CurrencyType.CleanMoney:
                if(_currentMoney >= amount)
                {
                    _currentMoney -= amount;
                }
                break;
            case CurrencyType.DirtyMoney:
                if(_currentDirtyMoney >= amount)
                {
                    _currentDirtyMoney -= amount;
                }
                break;
        }
    }
    private int CalculateMoneyReward(SupplyType supplyType)
    {
        switch(supplyType)
        {
            case SupplyType.Food:
                return 100;
            case SupplyType.Gun:
                return 200;
            case SupplyType.Ammo:
                return 150;
            case SupplyType.Shield:
                return 250;
            case SupplyType.Medical:
                return 300;
            default:
                return 0;
        }
    }
    private void UnlockSupplyType(GameObject go)
    {
        if(!_activeSupplyType.Contains(go))
        {
            _activeSupplyType.Add(go);
        }
    }
}
