using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputCntrl : MonoBehaviour
{
    private PlayerInputAction playerInputAction;

    private InputAction movement;

    private void Awake()
    {
        playerInputAction = new PlayerInputAction();
    }

    public Vector2 ReadMovement()
    {
        return (movement.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        movement = playerInputAction.Player.Movement;

        movement.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
    }
}
