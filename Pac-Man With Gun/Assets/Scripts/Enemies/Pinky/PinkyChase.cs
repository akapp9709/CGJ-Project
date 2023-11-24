using System.Collections;
using System.Collections.Generic;
using EnemyAI;
using Pathfinding;
using UnityEngine;

public class PinkyChase : IState
{
    private EnemyBrain _machine;
    private Transform _target;
    private AIDestinationSetter _agent;
    private PinkyBehavior _behavior;

    public PinkyChase(EnemyFSM fsm)
    {
        _machine = (EnemyBrain)fsm;
    }

    public void EnterState(EnemyBehavior controller)
    {
        Debug.Log("Entering state");
        _behavior = (PinkyBehavior)controller;
        _target = (Transform)_machine.GetValue("Player");
        _agent = _behavior.Agent;
        _agent.target = _target;
        _agent.enabled = true;
    }

    public void ExitState(EnemyBehavior controller)
    {
    }

    public string GetName()
    {
        return "Chase";
    }

    public void UpdateState(EnemyBehavior controller)
    {
    }
}
