using System.Collections;
using System.Collections.Generic;
using EnemyAI;
using UnityEngine;

public class PinkyFSM : EnemyBrain
{
    public PinkyFSM()
    {
        AddState("Patrol", new PinkyPatrol(this));
        AddState("Chase", new PinkyChase(this));
    }
}
