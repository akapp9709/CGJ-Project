using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Player Stuff")]
    public GameObject playerSprite;
    public GameObject playerSpriteEyes;
    public GameObject crosshairs;

    [Header("Weapon Stuff")]
    public GameObject weapon;
    public GameObject weaponMuzzle;
    public GameObject projectile_pistol;
    public float pistoleForce;

    public int ammoCount;

    [Header("Camera Stuff")]
    [SerializeField] private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        Aiming();
        Firing();
    }

    public void Aiming()
    {
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        crosshairs.transform.position = mousePos;

        Vector3 aimDirection = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        weapon.transform.eulerAngles = new Vector3(0, 0, angle);
        playerSpriteEyes.transform.eulerAngles = new Vector3(0, 0, angle);

        Vector3 aimLocalScale = Vector3.one;
        Vector3 playerLocalScale = Vector3.one;
        Vector3 playerEyesLocalScale = Vector3.one;
        if (angle > 90 || angle < -90)
        {
            aimLocalScale.y = -1f;
            playerLocalScale.y = -1f;
            playerLocalScale.x = -1f;

            playerEyesLocalScale.y = -1f;
        }
        else
        {
            aimLocalScale.y = +1f;
            playerLocalScale.y = +1f;
            playerLocalScale.x = +1f;

            playerEyesLocalScale.y = +1f;
        }
        weapon.transform.localScale = aimLocalScale;
        playerSprite.transform.localScale = playerLocalScale;
    }

    public void Firing()
    {
        if(Input.GetMouseButtonDown(0) /*&& pistolEqipped*/)
        {
            GameObject projectileGO = Instantiate(projectile_pistol, weaponMuzzle.transform.position,
            weaponMuzzle.transform.rotation);

            Vector2 direction =  weaponMuzzle.transform.position - transform.position;

            Rigidbody2D projectileRb = projectileGO.GetComponent<Rigidbody2D>();
            projectileRb.AddForce(pistoleForce * direction);
        }
        else if (Input.GetMouseButtonDown(0) /*&& rifleEqipped*/)
        {
            GameObject projectileGO = Instantiate(projectile_pistol, weaponMuzzle.transform.position,
            weaponMuzzle.transform.rotation);

            Vector2 direction = weaponMuzzle.transform.position - transform.position;

            Rigidbody2D projectileRb = projectileGO.GetComponent<Rigidbody2D>();
            projectileRb.AddForce(pistoleForce * direction);
        }
    }
}
