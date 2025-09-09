using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Joystick moveJoystick;       
    public CharacterController controller; 

    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    private Vector3 moveDirection;

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        // Get joystick input
        float horizontal = moveJoystick.Horizontal;
        float vertical = moveJoystick.Vertical;

        // Convert joystick input to movement direction (relative to world)
        moveDirection = new Vector3(horizontal, 0f, vertical);

        // Move if input is present
        if (moveDirection.sqrMagnitude > 0.001f)
        {
            controller.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
        }
    }
}