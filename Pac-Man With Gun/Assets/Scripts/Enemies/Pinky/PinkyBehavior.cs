using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkyBehavior : PacManEnemyBehavior
{

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        var brain = new PinkyFSM();
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
