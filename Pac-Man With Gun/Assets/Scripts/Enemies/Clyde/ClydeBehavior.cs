using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class ClydeBehavior : PacManEnemyBehavior
{
    private AIDestinationSetter _agent;
    public AIDestinationSetter Agent => _agent;
    private EnemyBrain _brain;
    private Transform _player;
    [SerializeField] private Transform followTarget;

    protected override void Start()
    {
        _player = FindObjectOfType<PlayerController>().transform;
        _agent = GetComponent<AIDestinationSetter>();
        GetComponent<AIPath>().maxSpeed = speed;

        _brain = new ClydeFSM();
        _brain.AddToDictionary("Player", _player);
        _brain.AddToDictionary("Follow Target", followTarget);
        _brain.StartFSM("Chase", this);
    }

    protected override void Update()
    {
        base.Update();
        _brain.UpdateFSM(this);
    }
}
