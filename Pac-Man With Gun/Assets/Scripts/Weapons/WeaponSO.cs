using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Objects/Weapon")]
public class WeaponSO : ScriptableObject
{
    public float weaponForce = 100;
    public int numberOfProjectiles = 1;
    public float projectileAngleOffset = 0;
    public float projectileTimeDelay;
    public float weaponCoolDown = 0.5f;
    public int magazineSize = 10;
    public float reloadTime = 2f;
    public ProjectileSO projectile;
    public GameObject particleSystem;
}
