using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComp : MonoBehaviour
{
    private string STATE_IDLE = "Idle";

    [SerializeField] private PlayerInputCntrl playerInputCntrl;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform cameraObject;

    private CharacterController charCntrl;

    private FiniteStateMachine fsm;

    private float moveSpeed = 5;
    private float rotationSpeed = 10;
    private float gravitySpeed = 0.0f;
    private float gravity = -9.81f;

    // Start is called before the first frame update
    void Start()
    {
        fsm = new FiniteStateMachine();
        fsm.Add(new PlayerStateIdle(STATE_IDLE));

        charCntrl = GetComponentInChildren<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = playerInputCntrl.ReadMovement();

        if (IsPlayerMoving(direction))
        {
            MovePlayerDirection(direction, Time.deltaTime);

            animator.SetFloat("hortDir", direction.x, 0.1f, Time.deltaTime);
            animator.SetFloat("vertDir", direction.y, 0.1f, Time.deltaTime);
        }
    }

    private void MovePlayerDirection(Vector2 direction, float dt)
    {
        Vector3 move = new Vector3(direction.x, 0.0f, direction.y);

        charCntrl.Move(move * moveSpeed * dt);
    }

    private bool IsPlayerMoving(Vector2 playerDirection)
    {
        return((int)playerDirection.magnitude != 0);
    }

    private void MovePlayerDirection1(Vector2 playerDirection, float dt)
    {
        if (!charCntrl.isGrounded)
        {
            gravitySpeed += gravity * Time.deltaTime;
        }
        else
        {
            gravitySpeed = 0.0f;
        }

        Vector3 direction = cameraObject.forward * playerDirection.y;
        direction = direction + cameraObject.right * playerDirection.x;
        direction.y = 0.0f;
        direction.Normalize();

        Vector3 hortMovement = moveSpeed * direction;
        Vector3 vertMovement = Vector3.up * gravitySpeed;

        charCntrl.Move(dt * (vertMovement + hortMovement));

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * dt);

        //transform.rotation = playerRotation;
    }

    private void MovePlayer()
    {
        Vector2 movement = playerInputCntrl.ReadMovement();

        Vector3 forward = (transform.forward * movement.y);
        Vector3 sideways = (transform.right * movement.x);
        Vector3 direction = forward + sideways;

        charCntrl.Move(direction * moveSpeed * Time.deltaTime);

        Debug.Log($"Movement: {movement.x},{movement.y}");

        fsm.Update();
    }
}
