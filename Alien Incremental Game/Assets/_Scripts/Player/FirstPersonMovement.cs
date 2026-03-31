using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonMovement : MonoBehaviour
{
    [GUIColor(0.4f, 0.8f, 1f)]
    [BoxGroup("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [GUIColor(0.4f, 0.8f, 1f)]
    [BoxGroup("Movement Settings")]
    [SerializeField] private float sprintMultiplier = 2f;
    [Space]
    [GUIColor(1, 0.8f, 0)]
    [BoxGroup("Jump Settings")]
    [SerializeField] private float jumpForce = 5f;
    [GUIColor(1, 0.8f, 0)]
    [BoxGroup("Jump Settings")]
    [SerializeField] private float gravity = 9.81f;
    [Space]
    [GUIColor(0, 0.8f, 0.4f)]
    [BoxGroup("Look Sensitivity")]
    [SerializeField] private float mouseSensitivity = 2f;
    [GUIColor(0, 0.8f, 0.4f)]
    [BoxGroup("Look Sensitivity")]
    [SerializeField] private float upDownRange = 80f;

    private float verticalRotation;
    private Vector3 _currentMovement = Vector3.zero;

    private CharacterController _characterController;
    private InputManager _input;
    private Camera _camera;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _input = GetComponent<InputManager>();
        _camera = GetComponentInChildren<Camera>();
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        HandleMovement();
        HandleRotation();
    }
    private void HandleMovement()
    {
        var speedMultiplier = _input.SprintInput ? sprintMultiplier : 1;
        var horizontalSpeed = _input.HorizontalInput;
        var verticalSpeed = _input.VerticalInput;
        var horizontalMovement = new Vector3(horizontalSpeed, 0, verticalSpeed).normalized;
        horizontalMovement = transform.rotation * horizontalMovement;

        HandleJump();

        _currentMovement.x = horizontalMovement.x * walkSpeed * speedMultiplier;
        _currentMovement.z = horizontalMovement.z * walkSpeed * speedMultiplier;

        _characterController.Move(_currentMovement * Time.deltaTime);
    }
    private void HandleRotation()
    {
        float mouseXRotation = _input.MouseXInput * mouseSensitivity;
        transform.Rotate(0, mouseXRotation, 0);

        verticalRotation -= _input.MouseYInput * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
        _camera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }
    private void HandleJump()
    {
        if (_characterController.isGrounded)
        {
            _currentMovement.y = -0.5f;

            if (_input.JumpInput)
            {
                _currentMovement.y = jumpForce;
            }
        }
        else
        {
                _currentMovement.y -= gravity * Time.deltaTime;
        }
    }
}
