using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "New Supply", menuName = "Supply/Supply Type")]
public class SupplySO : ScriptableObject
{
    [PreviewField(50)]
    public GameObject supplyPrefab;
    [VerticalGroup("Supply Info")]
    [EnumToggleButtons]
    public SupplyType supplyType;
    public int coinValue;
    public string supplyName;
}
public enum SupplyType
{
    Food,
    Gun,
    Ammo,
    Shield,
    Medical,
}
