using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComp : MonoBehaviour
{
    private string STATE_IDLE = "Idle";

    [SerializeField] private PlayerInputCntrl playerInputCntrl;
    [SerializeField] private Animation anim;

    private CharacterController charCntrl;

    private FiniteStateMachine fsm;

    private float moveSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        fsm = new FiniteStateMachine();
        fsm.Add(new PlayerStateIdle(STATE_IDLE));

        charCntrl = GetComponentInChildren<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
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
