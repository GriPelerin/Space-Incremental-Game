using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
public class PlayerGun : MonoBehaviour
{
    [Required]
    [SerializeField] private SupplySO[] _supplyDatabase;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private ParticleSystem vacuumEffect;

    [BoxGroup("UI Elements")]
    [SerializeField] private TextMeshProUGUI supplyTypeText;
    [SerializeField] private TextMeshProUGUI supplyAmountText;
    [SerializeField] private SpriteRenderer supplyTypeIcon;

    [BoxGroup("Layer Masks")]
    [SerializeField] private LayerMask collectibleMask;
    [SerializeField] private LayerMask obstacleMask;

    // For deposited supply types and their amounts
    private Dictionary<SupplyType, int> _supplyStorage = new Dictionary<SupplyType, int>();
    

    // For holding supply types and current selected supply type for shooting
    private SupplyType[] _supplyTypes;
    private SupplyType _currentSupplyType;


    // For cycling through supply types
    private int _supplyLimitAmount;
    private int _currentSupplyTypeIndex;

    // tracking supplies inside the overlap box for collecting
    private List<Supply> _collectingSupplies = new();

    private InputManager _input;
    private void Awake()
    {
        _input = GetComponentInParent<InputManager>();

        _supplyTypes = (SupplyType[])Enum.GetValues(typeof(SupplyType));
        _currentSupplyType = _supplyTypes[0];
    }

    private void Start()
    {
        UpdateWeaponUI();
    }
    private void Update()
    {
        if (_input.RightMouseInput)
        {
            CollectSupply();
        }
        else
        {
            vacuumEffect.Stop();
        }
        if (_input.LeftMouseInput)
        {
            ShootSupply();
        }
        if(_input.MiddleMouseInput)
        {
            ChangeSupply();
        }
        UpdateCollectingSupplies();
    }
    private void CollectSupply()
    {
        Collider[] hits = Physics.OverlapBox(shootPoint.position, new Vector3(1, 1, 1.5f), shootPoint.rotation, collectibleMask);

        foreach (Collider hit in hits)
        {
            if (!hit.TryGetComponent(out Supply supply))
                continue;

            if (supply.IsCollecting)
                continue;

            supply.Collect();
            _collectingSupplies.Add(supply);
        }

        vacuumEffect.Play();
    }
    private void UpdateCollectingSupplies()
    {
        for (int i = _collectingSupplies.Count - 1; i >= 0; i--)
        {
            Supply supply = _collectingSupplies[i];

            if (supply == null)
            {
                _collectingSupplies.RemoveAt(i);
                continue;
            }

            supply.CollectTimer += Time.deltaTime;

            float t = supply.CollectTimer / 0.5f;

            t = Mathf.Clamp01(t);

            supply.transform.position = Vector3.Lerp(supply.transform.position,shootPoint.position, t);

            supply.transform.localScale = Vector3.Lerp(supply.InitialScale,Vector3.zero, t);

            if (t >= 1f)
            {
                SupplyType type = supply.SupplyData.supplyType;
                if(_supplyStorage.TryGetValue(type, out int amount))
                {
                    _supplyStorage[type] = amount + 1;
                }
                else
                {
                    _supplyStorage.Add(type, 1);
                }
                UpdateWeaponUI();
                supply.CompleteCollect();
                _collectingSupplies.RemoveAt(i);
            }
        }
    }
    private void ShootSupply()
    {
        if (!_supplyStorage.ContainsKey(_currentSupplyType))
            return;

        if (_supplyStorage[_currentSupplyType] <= 0)
            return;

        Debug.DrawRay(Helpers.Camera.transform.position, Helpers.Camera.transform.forward * 5f, Color.red, 1f);

        if (Physics.Raycast(Helpers.Camera.transform.position, Helpers.Camera.transform.forward, out RaycastHit hitwallCheckDistance, 5f, obstacleMask))
        {
            return;
        }

        SupplySO data = Array.Find(_supplyDatabase, s => s.supplyType == _currentSupplyType);
        GameObject go = Instantiate(data.supplyPrefab, shootPoint.position, Quaternion.identity);
        Rigidbody rigidbody = go.GetComponent<Rigidbody>();
        rigidbody.velocity = shootPoint.forward * 10f;

        _supplyStorage[_currentSupplyType]--;

        UpdateWeaponUI();
    }
    private void ChangeSupply()
    {
        _currentSupplyTypeIndex++;
        if(_currentSupplyTypeIndex >= Helpers.GetSupplyTypeAmount())
        {
            _currentSupplyTypeIndex = 0;
        }

        _currentSupplyType = _supplyTypes[_currentSupplyTypeIndex];
        UpdateWeaponUI();
    }
    private void UpdateWeaponUI()
    {
        supplyTypeText.text = _currentSupplyType.ToString();
        if(_supplyStorage.TryGetValue(_currentSupplyType, out int amount))
        {
            supplyAmountText.text = amount.ToString();
        }
        else
        {
            supplyAmountText.text = "0";
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;

        Matrix4x4 rotationMatrix =Matrix4x4.TRS(shootPoint.position, shootPoint.rotation, Vector3.one);

        Gizmos.matrix = rotationMatrix;

        Gizmos.DrawWireCube(Vector3.zero, new Vector3(2, 2, 3));
    }
}
