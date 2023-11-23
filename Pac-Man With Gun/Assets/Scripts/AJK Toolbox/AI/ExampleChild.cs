using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleChild : EnemyBehavior
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Debug.Log("Child");
    }

    // Update is called once per frame
    protected override void Update()
    {

    }
}
