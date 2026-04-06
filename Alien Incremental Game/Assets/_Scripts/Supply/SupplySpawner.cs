using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SupplySpawner : MonoBehaviour
{
    [SerializeField] private VisualEffect visualEffect;
    [SerializeField] private GameObject supplyPrefab;

    private ObjectPooler _objectPooler;
    private float _spawnInterval;

    private void Awake()
    {
        _spawnInterval = visualEffect.GetFloat("ParticleLifeTime");
    }
    private void Start()
    {
        StartCoroutine(SpawnSupplyWithDelay());
        _objectPooler = ObjectPooler.Instance;
    }
    private IEnumerator SpawnSupplyWithDelay()
    {
        visualEffect.Play();
        yield return new WaitForSeconds(_spawnInterval);
        GameObject obj = _objectPooler.SpawnFromPool("Supply", transform.position, Quaternion.identity);
        obj.transform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.5f, 1);
    }
}
