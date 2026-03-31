using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class GunSway : MonoBehaviour
{
    [BoxGroup("Sway Settings")]
    [SerializeField] private float smooth;
    [BoxGroup("Sway Settings")]
    [SerializeField] private float swayMultiplier;

    [BoxGroup("Bob Settings")]
    [SerializeField] private float walkBobSpeed = 6f;
    [BoxGroup("Bob Settings")]
    [SerializeField] private float runBobSpeed = 10f;
    [BoxGroup("Bob Settings")]
    [SerializeField] private float bobAmount = 0.05f;

    private InputManager _inputManager;
    private CharacterController _characterController;

    private Vector3 initialPosition;
    private float timer;
    private void Awake()
    {
        _inputManager = GetComponentInParent<InputManager>();
        _characterController = GetComponentInParent<CharacterController>();
        initialPosition = transform.localPosition;
    }
    private void Update()
    {
        HandleGunSway();
        HandleGunBob();
    }

    private void HandleGunSway()
    {
        float mouseX = _inputManager.MouseXInput * swayMultiplier;
        float mouseY = _inputManager.MouseYInput * swayMultiplier;

        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationY * rotationX;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
    }
    private void HandleGunBob()
    {
        float moveAmount = Mathf.Abs(_inputManager.HorizontalInput) + Mathf.Abs(_inputManager.VerticalInput);
        if (moveAmount > 0.1f && _characterController.isGrounded)
        {
            float bobSpeed = _inputManager.SprintInput ? runBobSpeed : walkBobSpeed;
            timer += Time.deltaTime * bobSpeed;

            float bobX = Mathf.Cos(timer) * bobAmount;
            float bobY = Mathf.Sin(timer * 2) * bobAmount;

            Vector3 targetPos = initialPosition + new Vector3(bobX, bobY, 0);

            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                targetPos,
                smooth * Time.deltaTime
            );
        }
        else
        {
            // hareket yoksa merkeze dön
            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                initialPosition,
                smooth * Time.deltaTime
            );
        }
    }
}
