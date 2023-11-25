using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class InkyBehavior : PacManEnemyBehavior
{
    private AIDestinationSetter _agent;
    public AIDestinationSetter Agent => _agent;
    private EnemyBrain _brain;
    private Transform _player;
    [SerializeField] private Transform followTarget, spawnPoint;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float projectileSpeed = 5;
    [SerializeField] private float maxDistance, minDistance, positionChangeTime = 5;

    // Start is called before the first frame update
    protected override void Start()
    {
        _player = FindObjectOfType<PlayerController>().transform;
        _agent = GetComponent<AIDestinationSetter>();
        GetComponent<AIPath>().maxSpeed = speed;

        _brain = new InkyBrain();
        _brain.AddToDictionary("Player", _player);
        _brain.AddToDictionary("Follow Target", followTarget);
        _brain.AddToDictionary("Spawn", spawnPoint);
        _brain.AddToDictionary("Max Distance", maxDistance);
        _brain.AddToDictionary("Min Distance", minDistance);
        _brain.AddToDictionary("Speed", projectileSpeed);
        _brain.AddToDictionary("Projectile", projectile);
        _brain.AddToDictionary("Change Time", positionChangeTime);
        _brain.StartFSM("Chase", this);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        _brain.UpdateFSM(this);
    }
}
