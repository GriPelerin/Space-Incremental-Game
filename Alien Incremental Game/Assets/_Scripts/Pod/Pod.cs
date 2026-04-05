using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pod : MonoBehaviour
{
    [BoxGroup("Capacity")]
    [ShowInInspector]
    [GUIColor("RGB(0, 1, 0)")]
    private int _requiredSupplies;
    [BoxGroup("Capacity")]
    [ShowInInspector]
    [ReadOnly]
    [GUIColor("RGB(1, 0, 0)")]
    private int _currentSupplies;
}
