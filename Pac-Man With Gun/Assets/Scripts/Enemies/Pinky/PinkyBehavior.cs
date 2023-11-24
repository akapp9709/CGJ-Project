using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PinkyBehavior : PacManEnemyBehavior
{
    public List<Transform> waypoints;
    public float waitTime;
    private AIDestinationSetter _agent;

    public AIDestinationSetter Agent => _agent;
    private EnemyBrain brain;
    private Transform _player;
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        _player = FindObjectOfType<PlayerController>().transform;

        _agent = GetComponent<AIDestinationSetter>();
        GetComponent<AIPath>().maxSpeed = speed;


        brain = new PinkyFSM();
        brain.AddToDictionary("waypoints", waypoints);
        brain.AddToDictionary("wait time", waitTime);
        brain.AddToDictionary("Player", _player);
        brain.StartFSM("Chase", this);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        brain.UpdateFSM(this);
        if (_playerDetected)
        {
            Debug.Log("Player Found");
        }

    }
}
