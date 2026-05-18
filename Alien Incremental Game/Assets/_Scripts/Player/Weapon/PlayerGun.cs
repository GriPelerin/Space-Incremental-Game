using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private ParticleSystem vacuumEffect;
    [SerializeField] private LayerMask collectibleMask;
    [SerializeField] private LayerMask obstacleMask;

    [PreviewField(80, ObjectFieldAlignment.Left)]
    [SerializeField] private GameObject supplyProjectilePrefab;

    // Supply type and amount
    private Dictionary<SupplyType, int> _supplyStorage = new Dictionary<SupplyType, int>();
    private InputManager _input;

    private int _currentSupplyAmount;
    private int _supplyLimitAmount;

    private List<Supply> _collectingSupplies = new();
    private void Awake()
    {
        _input = GetComponentInParent<InputManager>();
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
        UpdateCollectingSupplies();
    }
    private void CollectSupply()
    {
        Collider[] hits = Physics.OverlapBox(shootPoint.position, new Vector3(1, 1, 1.5f), shootPoint.rotation, collectibleMask);

        foreach (Collider hit in hits)
        {
            Debug.Log("Overlap hit: " + hit.name);

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

            supply.transform.position = Vector3.Lerp(
                supply.transform.position,
                shootPoint.position,
                t);

            supply.transform.localScale = Vector3.Lerp(
                supply.InitialScale,
                Vector3.zero,
                t);

            if (t >= 1f)
            {
                supply.CompleteCollect();

                _collectingSupplies.RemoveAt(i);
            }
        }
    }
    private void ShootSupply()
    {
        Debug.DrawRay(Helpers.Camera.transform.position, Helpers.Camera.transform.forward * 5f, Color.red, 1f);

        if (Physics.Raycast(Helpers.Camera.transform.position, Helpers.Camera.transform.forward, out RaycastHit hitwallCheckDistance, 5f, obstacleMask))
        {
            return;
        }

        GameObject go = Instantiate(supplyProjectilePrefab, shootPoint.position, Quaternion.identity);
        go.GetComponent<Rigidbody>().velocity = shootPoint.forward * 10f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;

        Matrix4x4 rotationMatrix =Matrix4x4.TRS(shootPoint.position, shootPoint.rotation, Vector3.one);

        Gizmos.matrix = rotationMatrix;

        Gizmos.DrawWireCube(Vector3.zero, new Vector3(2, 2, 3));
    }
}
