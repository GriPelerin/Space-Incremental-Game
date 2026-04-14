using System;
using UnityEngine;
using DG.Tweening;
using UnityEditor.ShaderGraph.Internal;
using Sirenix.OdinInspector;

public class Supply : MonoBehaviour, IInteractable, ICollectible
{
    [AssetsOnly]
    [SerializeField] private SupplySO supplyData;

    public static Action<Supply> OnSupplyCollected;
    public string InteractableName => "Supply";

    private Rigidbody _rb;

    private Vector3 _targetPos;
    private Vector3 _startPos;

    private bool _isCollecting;
    private float _collectDuration = 0.3f;
    private float _collectTimer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        if (!_isCollecting) return;
        MoveTowardsTarget();
    }
    private void MoveTowardsTarget()
    {
        _collectTimer += Time.deltaTime;

        float t = _collectTimer / _collectDuration;
        t = Mathf.SmoothStep(0, 1, t);


        transform.position = Vector3.Lerp(_startPos, _targetPos, t);

        if (t >= 1f)
        {
            CompleteCollect();
        }
    }
    private void CompleteCollect()
    {
        OnSupplyCollected?.Invoke(this);

        _isCollecting = false;

        Destroy(gameObject);
    }
    public void Collect(Vector3 targetPosition)
    {
        _targetPos = targetPosition;
        _startPos = transform.position;

        _collectTimer = 0f;
        _isCollecting = true;
    }
    public void Interact()
    {
    }


}
