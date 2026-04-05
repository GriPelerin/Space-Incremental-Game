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

    private float _spawnInterval;

    private void Awake()
    {
        _spawnInterval = visualEffect.GetFloat("ParticleLifeTime");
    }
    private void Start()
    {
        StartCoroutine(SpawnSupplyWithDelay());
    }
    private IEnumerator SpawnSupplyWithDelay()
    {
        visualEffect.Play();
        yield return new WaitForSeconds(_spawnInterval);
        GameObject obj = Instantiate(supplyPrefab, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
        obj.transform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.5f, 1);
        obj.GetComponent<Rigidbody>().isKinematic = true;
    }
}
