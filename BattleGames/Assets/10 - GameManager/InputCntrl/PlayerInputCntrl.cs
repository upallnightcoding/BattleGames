using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputCntrl : MonoBehaviour
{
    private PlayerInputAction playerInputAction;

    private InputAction movement;
    private InputAction mouseLook;

    private void Awake()
    {
        playerInputAction = new PlayerInputAction();
    }

    public Vector2 ReadDirection()
    {
        return (movement.ReadValue<Vector2>());
    }

    public Vector2 ReadLook()
    {
        return (mouseLook.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        movement = playerInputAction.Player.Movement;
        mouseLook = playerInputAction.Player.MouseLook;

        movement.Enable();
        mouseLook.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
        mouseLook.Disable();
    }
}
