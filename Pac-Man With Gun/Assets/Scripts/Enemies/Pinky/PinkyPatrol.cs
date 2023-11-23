using System.Collections;
using System.Collections.Generic;
using EnemyAI;
using Pathfinding;
using UnityEngine;

public class PinkyPatrol : IState
{
    private PinkyBehavior _behavior;
    private AIDestinationSetter _agent;
    private List<Transform> waypoints;
    private Transform _trans, _currentWaypoint;
    private EnemyBrain _machine;
    private float _wait;
    public PinkyPatrol(EnemyFSM fsm)
    {
        _machine = (EnemyBrain)fsm;
    }
    public void EnterState(EnemyBehavior controller)
    {
        _behavior = (PinkyBehavior)controller;
        _agent = _behavior.Agent;
        waypoints = (List<Transform>)_machine.GetValue("waypoints");
        _wait = (float)_machine.GetValue("wait time");
        _currentWaypoint = waypoints[0];
        _agent.target = waypoints[0];
        _agent.enabled = true;
    }

    public void ExitState(EnemyBehavior controller)
    {
        throw new System.NotImplementedException();
    }

    public string GetName()
    {
        return "Patrol";
    }

    public void UpdateState(EnemyBehavior controller)
    {
        throw new System.NotImplementedException();
    }
}
