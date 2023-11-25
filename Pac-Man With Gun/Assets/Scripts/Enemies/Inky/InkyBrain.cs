using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkyBrain : EnemyBrain
{
    public InkyBrain()
    {
        AddState("Chase", new InkyChase(this));
    }
}
