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
    [HideInInspector] public float delta;
    public float deathDelay = 0.1f;
    [SerializeField] protected Animator anim;


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
        delta = Time.deltaTime;
        _playerDetected = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);
    }

    public virtual void TakeDamage(int dmg)
    {
        health -= dmg;
        anim.SetTrigger("damageTrigger");

        if (health <= 0)
        {
            Destroy(this.gameObject, deathDelay);
            anim.SetTrigger("deathTrigger"); 
        }

    }
}
