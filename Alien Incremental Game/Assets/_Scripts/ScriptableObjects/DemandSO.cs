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
    public SupplyType supplyType;

    public int baseSupplyDemandAmount;
    public int baseRewardAmount;

    public float duration;
}
