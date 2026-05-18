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

    [Header("Supply Text")]
    [SerializeField] private TextMeshProUGUI foodText;
    [SerializeField] private TextMeshProUGUI gunText;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI shieldText;
    [SerializeField] private TextMeshProUGUI medicalText;

    [Header("Supply")]
    [SerializeField] private GameObject foodSupply;
    [SerializeField] private GameObject gunSupply;
    [SerializeField] private GameObject ammoSupply;
    [SerializeField] private GameObject shieldSupply;
    [SerializeField] private GameObject medicalSupply;

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
