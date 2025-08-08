using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class UserInput : MonoBehaviour
{
    public PlayerInput playerInput;

    public static Vector2 MoveInput { get; set; }

    public static bool IsThrowPressed { get; set; }
    public static bool hasTouch { get; set; }
    public static float targetX { get; set; }

    private InputAction _moveAction;
    private InputAction _throwAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        _moveAction = playerInput.actions["Move"];
        _throwAction = playerInput.actions["Throw"];
    }

    private void Update()
    {
        MoveInput = _moveAction.ReadValue<Vector2>();

        IsThrowPressed = _throwAction.WasPressedThisFrame();
    }
    public void OnTouchPosition(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 touchPos = context.ReadValue<Vector2>();
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, 0f));
            targetX = worldPos.x;
            hasTouch = true;
        }
    }


}
