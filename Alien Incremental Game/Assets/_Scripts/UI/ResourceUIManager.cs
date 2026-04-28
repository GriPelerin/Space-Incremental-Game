using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceUIManager : MonoBehaviour
{
    [Header("Money UI")]
    [SerializeField] private TextMeshProUGUI cleanMoneyText;
    [SerializeField] private TextMeshProUGUI dirtyMoneyText;

    [Header("Supply UI")]
    [SerializeField] private TextMeshProUGUI foodText;
    [SerializeField] private TextMeshProUGUI gunText;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI shieldText;
    [SerializeField] private TextMeshProUGUI medicalText;

    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
    private void UpdateUI()
    {
        foodText.gameObject.SetActive(false);
    }
}
