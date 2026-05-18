using System;
using UnityEngine;
using UnityEditor.ShaderGraph.Internal;
using Sirenix.OdinInspector;

public class Supply : MonoBehaviour, IInteractable
{
    [AssetsOnly]
    [SerializeField] private SupplySO supplyData;
    [SerializeField] private float collectSpeed = 5f;
    public SupplySO SupplyData => supplyData;
    public string InteractableName => "Supply";
    public bool IsCollecting { get; private set; }
    public float CollectTimer { get; set; }
    public Vector3 InitialScale { get; private set; }

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        InitialScale = transform.localScale;

    }
    public void CompleteCollect()
    {
        EventManager<Supply>.TriggerEvent(EventType.OnSupplyCollected, this);

        Destroy(gameObject);
    }
    public void Collect()
    {
        if(IsCollecting) return;

        IsCollecting = true;

        _rb.useGravity = false;
        _rb.isKinematic = true;

        CollectTimer = 0f;    }
    public void Interact()
    {
    }


}
