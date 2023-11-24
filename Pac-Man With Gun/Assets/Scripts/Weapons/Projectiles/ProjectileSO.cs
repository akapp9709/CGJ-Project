using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                break;
            case MunitionType.pierce:
                break;
        }
    }

    private void BulletDamage(GameObject target, GameObject obj)
    {
        Debug.Log("Doing Damage");
        // Destroy(target);
        target.transform.parent.GetComponent<PacManEnemyBehavior>().TakeDamage();
        Destroy(obj);
    }
}
