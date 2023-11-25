using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Shooting : MonoBehaviour
{
    [Header("Player Stuff")]
    public GameObject playerSprite;
    public GameObject playerSpriteEyes;
    public GameObject crosshairs;
    public Animator playerPistolAnimator;
    public Animator playerRifleAnimator;
    public Animator playerShotgunAnimator;

    [Header("Weapon Stuff")]
    public WeaponSO _weapon;
    public GameObject weaponObject;
    public GameObject weaponMuzzle;
    private Weapon _currentWeapon;
    [SerializeField] private List<Weapon> arsenal;
    public int ammoCount;
    private Vector3 refVelocity = Vector3.zero;

    [Header("Camera Stuff")]
    [SerializeField] private Camera mainCamera;

    private Timer _weaponTimer, _reloadTimer;
    private bool _weaponCool = true, _loaded = true;
    private int _shotCounter;

    private PacManWithGun controls;
    private Vector3 playerLocalScale, weaponScale;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;

        controls = new PacManWithGun();
        controls.Player.Enable();
        controls.Player.Fire.started += Fire;

        SceneManager.sceneUnloaded += OnSceneUnload;

        _currentWeapon = arsenal[0];
        SwitchToWeapon(0);

        playerLocalScale = playerSprite.transform.localScale;
        weaponScale = weaponObject.transform.localScale;
    }

    private void OnSceneUnload(Scene current)
    {
        controls.Player.Fire.started -= Fire;
        controls.Player.Disable();
        SceneManager.sceneUnloaded -= OnSceneUnload;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Aiming();
        float delta = Time.deltaTime;
        TickTimer(_weaponTimer, delta);
        TickTimer(_reloadTimer, delta);
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
        weaponObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        // playerSpriteEyes.transform.eulerAngles = new Vector3(0, 0, angle);

        Vector3 aimLocalScale = Vector3.one;
        Vector3 playerScale = playerLocalScale;
        if (angle > 90 || angle < -90)
        {
            aimLocalScale.y = -1f;
            // playerLocalScale.y = -1f;
            playerScale.x *= 1f;
            // playerEyesLocalScale.y = -1f;
        }
        else
        {
            aimLocalScale.y = 1f;
            // playerLocalScale.y = +1f;
            playerScale.x *= -1f;
            // playerEyesLocalScale.y = +1f;
            // weapon.transform.eulerAngles *=
        }

        weaponObject.transform.localScale = new Vector3(weaponScale.x, weaponScale.y * aimLocalScale.y, weaponScale.z);
        playerSprite.transform.localScale = playerScale;
    }

    private void Fire(InputAction.CallbackContext context)
    {
        if (!_weaponCool)
            return;

        if (!_loaded && _weaponCool)
            return;

        StartCoroutine(FireRoutine());
        _weaponCool = false;
        _weaponTimer = new Timer(_weapon.weaponCoolDown, () => _weaponCool = true);

        _shotCounter++;
        if (_shotCounter >= _weapon.magazineSize)
        {
            Debug.Log(_shotCounter);
            _loaded = false;
            _reloadTimer = new Timer(_weapon.reloadTime, Reload);
        }
    }

    private void Reload()
    {
        _shotCounter = 0;
        _loaded = true;
    }

    private IEnumerator FireRoutine()
    {
        float startingAngle = -(_weapon.projectileAngleOffset * _weapon.numberOfProjectiles) / 2f;

        for (int i = 0; i < _weapon.numberOfProjectiles; i++)
        {
            //Yes yes this is dumb, but it will work. Leave me alone.
            /*noted, I wasnt gonna touch it XD*/
            playerPistolAnimator.SetTrigger("Fire");
            playerRifleAnimator.SetTrigger("Fire");
            playerShotgunAnimator.SetTrigger("Fire");


            float angle = startingAngle + ((i + 0.5f) * _weapon.projectileAngleOffset);
            var right = weaponMuzzle.transform.right;
            GameObject projectileGO = Instantiate(_weapon.projectile.projectilePrefab, weaponMuzzle.transform.position,
                    Quaternion.Euler(0f, 0f, angle) * weaponMuzzle.transform.rotation);

            Vector2 direction = Quaternion.Euler(0f, 0f, angle) * new Vector2(right.x, right.y);

            Rigidbody2D projectileRb = projectileGO.GetComponent<Rigidbody2D>();
            projectileRb.AddForce(_weapon.weaponForce * direction.normalized);
            yield return new WaitForSeconds(_weapon.projectileTimeDelay);
        }
    }

    public void GetNewWeapon(string weaponName, WeaponSO weapon)
    {
        _weapon = weapon;
        for (int i = 0; i < arsenal.Count; i++)
        {
            if (arsenal[i].weaponName == weaponName)
            {
                SwitchToWeapon(i);
                return;
            }
        }
    }

    private void SwitchToWeapon(int index)
    {
        foreach (var w in arsenal)
        {
            w.weapon.SetActive(false);
        }

        arsenal[index].weapon.SetActive(true);
        _weapon = arsenal[index].stats;
    }

    [Serializable]
    private struct Weapon
    {
        public GameObject weapon;
        public string weaponName;
        public WeaponSO stats;
    }
}
