using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacManEnemyBehavior : EnemyBehavior
{
    public float detectionRadius;
    public float speed;
    public float health;
    public LayerMask playerLayer;
    protected bool _playerDetected = false;


    protected override void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    protected override void Start()
    {
        base.Start();

    }

    protected override void Update()
    {
        base.Update();
        _playerDetected = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);
    }
}
