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

    [VerticalGroup("Enum group")]
    [EnumToggleButtons]
    [GUIColor(1, 1, .7f)]
    public SupplyType supplyType;

    [VerticalGroup("Enum group")]
    [EnumToggleButtons]
    [GUIColor(1f, .1f, .5f)]
    public CurrencyType currencyType;

    public int baseSupplyDemandAmount;
    public int baseRewardAmount;

    public float duration;
}
