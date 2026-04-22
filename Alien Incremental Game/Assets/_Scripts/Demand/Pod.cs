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

    public void Deposit(int amount)
    {
        _currentSupplies += amount;
        Debug.Log($"Deposited {amount} supplies");
        if (_currentSupplies >= _requiredSupplies)
        {
            LaunchPod();
        }
    }

    private void LaunchPod()
    {
        _currentSupplies = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Supply supply))
        {
            Deposit(1);
            Destroy(supply.gameObject);
        }
    }
}
