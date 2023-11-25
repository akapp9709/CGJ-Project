using System.Collections;
using System.Collections.Generic;
using EnemyAI;
using UnityEngine;
using Pathfinding;
using UnityEditor.Experimental.GraphView;

public class InkyChase : IState
{
    private EnemyBrain _brain;
    private Transform _target, _follow, _spawn;
    private AIDestinationSetter _agent;
    private InkyBehavior _behavior;
    private float _speed, _changeTime;
    private GameObject _proj;

    private Vector3 _direction;
    private Timer _attackTimer, _moveTimer;
    public InkyChase(EnemyBrain brain)
    {
        _brain = brain;
    }

    public void EnterState(EnemyBehavior controller)
    {
        _behavior = (InkyBehavior)controller;
        _target = (Transform)_brain.GetValue("Player");
        _follow = (Transform)_brain.GetValue("Follow Target");
        _spawn = (Transform)_brain.GetValue("Spawn");
        _speed = (float)_brain.GetValue("Speed");
        _proj = (GameObject)_brain.GetValue("Projectile");
        _changeTime = (float)_brain.GetValue("Change Time");

        _attackTimer = new Timer(_changeTime / 2f, Attack);
        _moveTimer = new Timer(_changeTime, ChangePosition);


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
        _direction = _target.position - controller.transform.position;
        _direction.Normalize();

        _attackTimer.Tick(_behavior.delta);
        _moveTimer.Tick(_behavior.delta);
    }

    private void Attack()
    {
        var obj = GameObject.Instantiate(_proj, _spawn.position, Quaternion.identity, _spawn);
        obj.GetComponent<Rigidbody2D>().velocity = _direction * _speed;
        _attackTimer = new Timer(_changeTime / 2f, Attack);
    }
    private void ChangePosition()
    {
        _moveTimer = new Timer(_changeTime, ChangePosition);

        var center = _target.position;

        var angle = Random.Range(0f, 2f * Mathf.PI);
        float offsetX = 4 * Mathf.Cos(angle);
        float offsetY = 4 * Mathf.Sin(angle);

        Vector3 newPosition = center + new Vector3(offsetX, offsetY, 0f);

        _follow.position = newPosition;
    }
}
