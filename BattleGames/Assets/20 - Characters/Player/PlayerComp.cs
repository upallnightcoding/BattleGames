using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComp : MonoBehaviour
{
    private string STATE_START = "Start";

    [SerializeField] private PlayerInputCntrl playerInputCntrl;

    private FiniteStateMachine fsm;

    // Start is called before the first frame update
    void Start()
    {
        fsm = new FiniteStateMachine();
        fsm.Add(new PlayerStateIdle(STATE_START));
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = playerInputCntrl.ReadMovement();

        Debug.Log($"Movement: {movement.x},{movement.y}");

        fsm.Update();
    }
}
