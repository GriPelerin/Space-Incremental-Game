using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SupplySpawner : MonoBehaviour
{
    [PreviewField(Alignment = ObjectFieldAlignment.Left)]
    [SerializeField] private VisualEffect visualEffect;
    [SerializeField] private GameObject supplyPrefab;

    private GameObject _currentSupply;
    private float _spawnInterval = 3f;
    private bool _isOccupied;

    private void Start()
    {
        _isOccupied = false;
        _spawnInterval = 3f;
        StartCoroutine(SpawnSupplyWithDelay());
    }
    private void OnEnable()
    {
        Supply.OnSupplyCollected += RespawnSupply;
    }
    private void OnDisable()
    {
        Supply.OnSupplyCollected -= RespawnSupply;
    }
    private IEnumerator SpawnSupplyWithDelay()
    {
        if (_isOccupied) yield break;
        _isOccupied = true;
        visualEffect.Play();
        yield return new WaitForSeconds(_spawnInterval);
        _currentSupply = Instantiate(supplyPrefab, new Vector3(transform.localPosition.x, transform.position.y + 1, transform.localPosition.z), Quaternion.identity);
        _currentSupply.transform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.5f, 1);
    }
    private void RespawnSupply(Supply supply)
    {
        if(_currentSupply != supply.gameObject) return;

        _isOccupied = false;
        _currentSupply = null;
        StartCoroutine(SpawnSupplyWithDelay());
    }
}
