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

    private Transform cam;

    private float moveSpeed = 2;
    private float rotationSpeed = 400;
    private float gravitySpeed = 0.0f;
    private float gravity = -9.81f;

    // Start is called before the first frame update
    void Start()
    {
        fsm = new FiniteStateMachine();
        fsm.Add(new PlayerStateIdle(STATE_IDLE));

        charCntrl = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = playerInputCntrl.ReadDirection();
        Vector2 look = playerInputCntrl.ReadLook();

        MovePlayerDirection2(move, look, Time.deltaTime);

        AnimationUpdate(move);
    }

    private void AnimationUpdate(Vector2 moveDir)
    {
        Vector3 camForward = Vector3.Scale(cam.up, new Vector3(1, 0, 1)).normalized;
        Vector3 move = moveDir.y * camForward + moveDir.x * cam.right;

        if (move.magnitude > 1)
        {
            move.Normalize();
        }

        Vector3 moveInput = move;

        Vector3 localMove = transform.InverseTransformDirection(moveInput);
        float turnAmount = localMove.x;
        float forwardAmount = localMove.z;

        animator.SetFloat("turn", turnAmount, 0.1f, Time.deltaTime);
        animator.SetFloat("forward", forwardAmount, 0.1f, Time.deltaTime);
    }

    private bool IsPlayerMoving(Vector2 direction)
    {
        return((int)direction.magnitude != 0);
    }

    private void MovePlayerDirection2(Vector2 move, Vector2 look,  float dt)
    {
        if (!charCntrl.isGrounded)
        {
            gravitySpeed += gravity * Time.deltaTime;
        }
        else
        {
            gravitySpeed = 0.0f;
        }

        Vector3 moveDir = cameraObject.forward * move.y;
        moveDir = moveDir + cameraObject.right * move.x;
        moveDir.y = 0.0f;
        moveDir.Normalize();

        Vector3 hortMovement = moveSpeed * moveDir;
        Vector3 vertMovement = Vector3.up * gravitySpeed;

        charCntrl.Move(dt * (vertMovement + hortMovement));

        Ray ray = Camera.main.ScreenPointToRay(look);

        RaycastHit hit;
        Vector3 lookPos = Vector3.zero;

        if (Physics.Raycast(ray, out hit, 100))
        {
            lookPos = hit.point;
        }

        Vector3 lookDir = lookPos - transform.position;
        lookDir.y = 0;

        transform.LookAt(transform.position + lookDir, Vector3.up);

        //Quaternion targetRotation = Quaternion.LookRotation(transform.position + lookDir);
        //Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * dt);

        //transform.rotation = playerRotation;
    }
}
