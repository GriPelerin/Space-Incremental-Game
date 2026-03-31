using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [BoxGroup("References")]
    [SerializeField]
    private PlayerUI _playerUI;
    [Header("Interaction Fields")]
    [SerializeField] private LayerMask interactableMask;
    [SerializeField] private float rayLength;

    private RaycastHit _hit;
    private InputManager _input;
    private void Awake()
    {
        _playerUI = GetComponent<PlayerUI>();
        _input = GetComponent<InputManager>();
    }
    private void Update()
    {
        if(_input.InteractInput)
        {
            Interaction();
        }
    }
    private void Interaction()
    {
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Camera.main.transform.forward * rayLength, Color.red);
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Camera.main.transform.forward, out _hit, rayLength, interactableMask))
        {
            if (_hit.collider.gameObject.CompareTag("Obstacle")) return;

            if (_hit.collider.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                interactable.Interact();
                //_playerUI.InteractText.text = interactable.InteractableName;
            }
        }
    }
}
