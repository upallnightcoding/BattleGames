using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    private Dictionary<string, FiniteStateMachineState> states;

    private FiniteStateMachineState currentState = null;

    private string nextState = null;

    public FiniteStateMachine() 
    {
        states = new Dictionary<string, FiniteStateMachineState>();
    }

    public void Add(FiniteStateMachineState state)
    {
        states.Add(state.Name, state);

        if (currentState == null)
        {
            currentState = state;
        }
    }

    public void Update()
    {
        if (nextState == null)
        {
            nextState = currentState.OnUpdate();
        } else
        {
            //Try Value Dictionary
        }
    }
}
