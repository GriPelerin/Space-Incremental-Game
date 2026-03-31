using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "New Supply", menuName = "Supply/Supply Type")]
public class SupplySO : ScriptableObject
{
    [PreviewField(50)]
    public Sprite supplyIcon;
    public string supplyName;
}
