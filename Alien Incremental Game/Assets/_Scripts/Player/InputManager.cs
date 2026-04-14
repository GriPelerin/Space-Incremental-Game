using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float HorizontalInput { get; private set; }
    public float VerticalInput { get; private set; }

    public float MouseXInput { get; private set; }
    public float MouseYInput { get; private set; }

    public bool SprintInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool InteractInput { get; private set; }

    public bool LeftMouseInput { get; private set; }
    public bool RightMouseInput { get; private set; }


    private void Update()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");

        MouseXInput = Input.GetAxisRaw("Mouse X");
        MouseYInput = Input.GetAxisRaw("Mouse Y");

        SprintInput = Input.GetKey(KeyCode.LeftShift);
        JumpInput = Input.GetKeyDown(KeyCode.Space);
        InteractInput = Input.GetKeyDown(KeyCode.E);

        LeftMouseInput = Input.GetKeyDown(KeyCode.Mouse0);
        RightMouseInput = Input.GetKeyDown(KeyCode.Mouse1);

    }
}
