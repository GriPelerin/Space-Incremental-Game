using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class DemandUIItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI demandDescriptionText;
    [SerializeField] private TextMeshProUGUI rewardAmountText;
    [SerializeField] private TextMeshProUGUI rewardProgressText;

    [SerializeField] private Button claimButton;

    private Demand _demand;

    private void Start()
    {
        UpdateUI();
    }
    public void Initialize(Demand demand)
    {
        claimButton.interactable = false;
        _demand = demand;
        claimButton.onClick.AddListener(ClaimReward);

        UpdateUI();
    }

    private void UpdateUI()
    {
        claimButton.interactable = _demand.IsCompleted;
        demandDescriptionText.text = _demand.demandDescription;
        rewardAmountText.text = _demand.rewardAmount.ToString();
    }


    private void ClaimReward()
    {
        if (!_demand.IsCompleted) return;

        EventManager<Demand>.TriggerEvent(EventType.OnDemandCompleted, _demand);
    }
}
