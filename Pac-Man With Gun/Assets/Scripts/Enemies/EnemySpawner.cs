using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : PacManEnemyBehavior
{
    [Serializable]
    private struct Enemy
    {
        public GameObject prefab;
        public float bias;
    }
    [SerializeField] private bool isWinSpwaner;
    [SerializeField] private float enemyTimeDelay = 2f;
    [SerializeField] private List<Enemy> enemies;

    public string rewardName = "Pistol";

    private float biasLineLength;
    private Timer _spawnTimer;
    // Start is called before the first frame update
    protected override void Start()
    {
        _spawnTimer = new Timer(enemyTimeDelay, SpawnEnemy);
        biasLineLength = GetTotalBias();
    }

    // Update is called once per frame
    protected override void Update()
    {
        _spawnTimer.Tick(Time.deltaTime);
    }

    private void SpawnEnemy()
    {
        _spawnTimer = new Timer(enemyTimeDelay, SpawnEnemy);

        var rand = Random.Range(0, biasLineLength);
        float runningTotal = 0f;

        foreach (var obj in enemies)
        {
            if (rand < obj.bias + runningTotal)
            {
                Instantiate(obj.prefab, this.transform);
                return;
            }
        }
    }

    private float GetTotalBias()
    {
        float result = 0;

        foreach (var bias in enemies)
        {
            result += bias.bias;
        }

        return result;
    }

    public override void TakeDamage(int dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            if (isWinSpwaner)
            {
                /*Win Logic*/
                Destroy(this.gameObject, deathDelay);
                return;
            }
            FindObjectOfType<Shooting>().GetNewWeapon(rewardName);
            Destroy(this.gameObject, deathDelay);
        }
    }
}
