using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Demand", menuName = "Demand/Demand Type")]
public class DemandSO : ScriptableObject
{
    [PreviewField]
    public Sprite demanderIcon;
    public string demanderName;
    [TextArea]
    public string demandDescription;

    [EnumToggleButtons]
    [GUIColor(1, 0f, 0f)]
    public SupplyType supplyType;
    [EnumToggleButtons]
    [GUIColor(1f, 1f, 0f)]
    public CurrencyType currencyType;
    public int baseSupplyDemandAmount;
    public int baseRewardAmount;

    public float duration;
}
