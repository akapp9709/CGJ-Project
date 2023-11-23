using System.Collections;
using System.Collections.Generic;
using EnemyAI;
using UnityEngine;

public class PinkyChase : IState
{
    private EnemyFSM _machine;

    public PinkyChase(EnemyFSM fsm)
    {
        _machine = fsm;
    }

    public void EnterState(EnemyBehavior controller)
    {
        throw new System.NotImplementedException();
    }

    public void ExitState(EnemyBehavior controller)
    {
        throw new System.NotImplementedException();
    }

    public string GetName()
    {
        return "Chase";
    }

    public void UpdateState(EnemyBehavior controller)
    {
        throw new System.NotImplementedException();
    }
}
