using UnityEngine;

public class Supply : MonoBehaviour, IInteractable, ICollectible, IPooledObject
{
    public string InteractableName => "";

    private Rigidbody _rb;
    private Transform _moveTo;

    private bool _isCollecting;
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
        transform.position = Vector3.MoveTowards(transform.position, _moveTo.position, 10f * Time.deltaTime);
        if (Vector3.Distance(transform.position, _moveTo.position) < 0.2f)
        {
            _isCollecting = false;

            PlayerGun gun = _moveTo.GetComponentInParent<PlayerGun>();
            gun.AddSupply(this);

            gameObject.SetActive(false);
        }
    }
    public void Collect(Transform moveTo)
    {
        _moveTo = moveTo;
        _isCollecting = true;
    }
    public void Interact()
    {
    }
    public void OnObjectSpawn()
    {
        _rb.isKinematic = true;
    }
}
