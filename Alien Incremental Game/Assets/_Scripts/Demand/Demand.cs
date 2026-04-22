using UnityEngine;

public class Demand
{
    public DemandSO demandSO;
    public Sprite demanderIcon;

    public string demanderName;
    public string demandDescription;

    public int requiredSupplyAmount;
    public int currentSupplyAmount;
    public int rewardAmount;

    public float demandDuration;
    public bool IsCompleted => currentSupplyAmount >= requiredSupplyAmount;
    public Demand(DemandSO demandSO)
    {
        this.demandSO = demandSO;
        this.demanderIcon = demandSO.demanderIcon;

        this.demanderName = demandSO.demanderName;
        this.demandDescription = demandSO.demandDescription;

        this.demandDuration = demandSO.duration;
        this.rewardAmount = demandSO.baseRewardAmount;
        this.currentSupplyAmount = 0;
    }
}
