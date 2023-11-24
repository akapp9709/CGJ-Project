using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClydeFSM : EnemyBrain
{
    public ClydeFSM()
    {
        AddState("Chase", new ClydeChase(this));
    }
}
