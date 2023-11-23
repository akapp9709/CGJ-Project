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
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        _agent = GetComponent<AIDestinationSetter>();
        var brain = new PinkyFSM();
        brain.AddToDictionary("waypoints", waypoints);
        brain.AddToDictionary("wait time", waitTime);
        brain.StartFSM("Patrol", this);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (_playerDetected)
        {
            Debug.Log("Player Found");
        }

    }

    public virtual void Move(Vector3 destination)
    {

    }
}
