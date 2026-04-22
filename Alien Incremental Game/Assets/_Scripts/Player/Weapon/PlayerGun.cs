using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private ParticleSystem vacuumEffect;
    [SerializeField] private LayerMask collectibleMask;
    [SerializeField] private float rayLength = 5f;
    [SerializeField] private GameObject supplyProjectilePrefab;

    private InputManager _input;

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

    }
    private void CollectSupply()
    {
        Debug.DrawRay(Helpers.Camera.transform.position, Helpers.Camera.transform.forward * rayLength, Color.magenta);

        if (Physics.Raycast(Helpers.Camera.transform.position, Helpers.Camera.transform.forward, out RaycastHit hit2, rayLength, collectibleMask))
        {
            Debug.Log("Ray hit: " + hit2.collider.name);
            if (hit2.collider.TryGetComponent(out ICollectible collectible))
            {
                vacuumEffect.Play();
                collectible.Collect(shootPoint.position);
            }
        }
    }
    private void ShootSupply()
    {
        GameObject go = Instantiate(supplyProjectilePrefab, shootPoint.position, Quaternion.identity);
        go.GetComponent<Rigidbody>().velocity = shootPoint.forward * 10f;
    }
}
