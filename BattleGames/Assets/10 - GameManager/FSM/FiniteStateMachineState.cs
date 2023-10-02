using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FiniteStateMachineState 
{
    public string Name { get; private set; }

    public FiniteStateMachineState(string name)
    {
        Name = name;
    }

    public abstract void OnEnter();
    public abstract void OnExit();
    public abstract string OnUpdate();
}
