using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Objects/Ammo Type")]
public class ProjectileSO : ScriptableObject
{
    [HideInInspector] public GameObject gameObject;
    public string targetTag = "Enemy";
    public GameObject projectilePrefab;
    public MunitionType type;
    public int damage = 1;
    public float damageRadius = 0;
    public enum MunitionType
    {
        bullet,
        explosive,
        pierce
    }

    public void DoDamage(GameObject target, string tag, GameObject obj)
    {
        switch (type)
        {
            case MunitionType.bullet:
                Debug.Log("Bullet Damag: " + tag);
                if (tag == targetTag)
                    BulletDamage(target, obj);
                break;
            case MunitionType.explosive:
                ExplosiveDamage(obj);
                break;
            case MunitionType.pierce:
                break;
        }
    }

    private void BulletDamage(GameObject target, GameObject obj)
    {
        if (targetTag == "Player")
        {
            target.GetComponentInParent<PlayerController>().Die();
            Destroy(obj);
        }

        Debug.Log("Doing Damage");
        // Destroy(target);
        target.transform.parent.GetComponent<PacManEnemyBehavior>().TakeDamage(damage);
        Destroy(obj);
    }

    private void ExplosiveDamage(GameObject obj)
    {
        var col = Physics2D.OverlapCircleAll(obj.transform.position, damageRadius);
        foreach (var collider in col)
        {
            if (collider.CompareTag(targetTag))
            {
                Debug.Log("Dealing Damage");
                collider.GetComponentInParent<PacManEnemyBehavior>().TakeDamage(damage);
            }
        }
        Destroy(obj);
    }
}

