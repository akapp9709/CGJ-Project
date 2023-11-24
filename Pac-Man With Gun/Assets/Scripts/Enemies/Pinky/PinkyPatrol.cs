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
    private Timer _waitTimer;
    private bool _waiting;
    private int _pntIndex;
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
        TickTimer(_waitTimer, _behavior.delta);


        if (_waiting)
            return;
        if (_agent.GetComponent<AIPath>().reachedEndOfPath)
        {
            _waiting = true;
            _waitTimer = new Timer(_wait, NextPoint);
        }

    }

    private void NextPoint()
    {
        _pntIndex++;
        _pntIndex %= waypoints.Count;

        _agent.target = waypoints[_pntIndex];
        _waiting = false;
    }

    private void TickTimer(Timer timer, float delta)
    {
        if (timer != null)
            timer.Tick(delta);
    }
}
