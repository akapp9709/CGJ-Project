using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
        float startingAngle = -(_weapon.projectileAngleOffset * _weapon.numberOfProjectiles) / 2f;

        for (int i = 0; i < _weapon.numberOfProjectiles; i++)
        {
            float angle = startingAngle + ((i + 0.5f) * _weapon.projectileAngleOffset);
            var right = weaponMuzzle.transform.right;
            GameObject projectileGO = Instantiate(_weapon.projectilePrefab, weaponMuzzle.transform.position,
                    Quaternion.Euler(0f, 0f, angle) * weaponMuzzle.transform.rotation);



            Vector2 direction = Quaternion.Euler(0f, 0f, angle) * new Vector2(right.x, right.y);
            Debug.Log(weaponMuzzle.transform.right);

            Rigidbody2D projectileRb = projectileGO.GetComponent<Rigidbody2D>();
            projectileRb.AddForce(_weapon.weaponForce * direction.normalized);
            yield return new WaitForSeconds(_weapon.projectileTimeDelay);
        }
    }
}
