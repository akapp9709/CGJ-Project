using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    [Header("Player Stuff")]
    public GameObject playerSprite;
    public GameObject playerSpriteEyes;
    public GameObject crosshairs;

    [Header("Weapon Stuff")]
    public WeaponSO _weapon;
    public GameObject weapon;
    public GameObject weaponMuzzle;
    public GameObject projectile_pistol;
    public float pistoleForce;

    public int ammoCount;
    private Vector3 refVelocity = Vector3.zero;

    [Header("Camera Stuff")]
    [SerializeField] private Camera mainCamera;

    private Timer _weaponTimer;
    private bool _weaponCool = true;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;

        var controls = new PacManWithGun();
        controls.Player.Enable();
        controls.Player.Fire.started += Fire;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Aiming();
        TickTimer(_weaponTimer, Time.deltaTime);
    }

    private void TickTimer(Timer timer, float deltaTime)
    {
        if (timer != null)
        {
            timer.Tick(deltaTime);
        }
    }

    public void Aiming()
    {
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        var currentPos = crosshairs.transform.position;

        crosshairs.transform.position = Vector3.SmoothDamp(currentPos, mousePos, ref refVelocity, 0.01f);

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

    private void Fire(InputAction.CallbackContext context)
    {
        if (!_weaponCool)
            return;

        StartCoroutine(FireRoutine());
        _weaponCool = false;
        _weaponTimer = new Timer(_weapon.weaponCoolDown, () => _weaponCool = true);
    }

    private IEnumerator FireRoutine()
    {
        for (int i = 0; i < _weapon.numberOfProjectiles; i++)
        {
            GameObject projectileGO = Instantiate(_weapon.projectilePrefab, weaponMuzzle.transform.position,
                    weaponMuzzle.transform.rotation);

            Vector2 direction = weaponMuzzle.transform.right;

            Rigidbody2D projectileRb = projectileGO.GetComponent<Rigidbody2D>();
            projectileRb.AddForce(_weapon.weaponForce * direction);
            yield return new WaitForSeconds(_weapon.projectileTimeDelay);
        }
    }

    public void Firing()
    {
        if (Input.GetMouseButtonDown(0) /*&& pistolEqipped*/)
        {

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
