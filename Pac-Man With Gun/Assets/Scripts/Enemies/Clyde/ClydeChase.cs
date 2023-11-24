using System.Collections;
using System.Collections.Generic;
using EnemyAI;
using UnityEngine;
using Pathfinding;

public class ClydeChase : IState
{
    private EnemyBrain _brain;
    private Transform _target, _follow;
    private AIDestinationSetter _agent;
    private ClydeBehavior _behavior;
    public ClydeChase(EnemyBrain fsm)
    {
        _brain = fsm;
    }
    public void EnterState(EnemyBehavior controller)
    {
        _behavior = (ClydeBehavior)controller;
        _target = (Transform)_brain.GetValue("Player");
        _follow = (Transform)_brain.GetValue("Follow Target");



        _agent = _behavior.Agent;
        _agent.target = _follow;
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
        _follow.position = new Vector3(_target.position.x, controller.transform.position.y);
    }
}
