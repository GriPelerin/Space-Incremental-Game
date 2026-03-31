using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private LayerMask collectibleMask;
    [SerializeField] private float rayLength = 5f;

    [ShowInInspector]
    private List<Supply> _supplies = new List<Supply>();
    private InputManager _input;

    private void Awake()
    {
        _input = GetComponentInParent<InputManager>();
    }
    private void Update()
    {
        if (_input.RightMouseInput)
        {
            CollectRay();
        }
        if(_input.LeftMouseInput)
        {
            ShootRay();
        }

    }
    private void CollectRay()
    {
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * rayLength, Color.magenta);

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit2, rayLength, collectibleMask))
        {
            Debug.Log("Ray hit: " + hit2.collider.name);
            if (hit2.collider.TryGetComponent(out ICollectible collectible))
            {
                collectible.Collect(shootPoint);
            }
        }
    }
    private void ShootRay()
    {

    }
    public void AddSupply(Supply supply)
    {
        _supplies.Add(supply);
        Debug.Log("Supply added. Count: " + _supplies.Count);
    }
}
