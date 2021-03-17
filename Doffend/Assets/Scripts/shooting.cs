using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    [SerializeField]
    public GameObject bullet;

    [SerializeField]
    public Transform bulletDirection;

    private PlayerInput _playerInput;

    private bool _canShoot = true;
    private Camera _mainC;

    private bool isHeld = false;
    private static int shotsFired = 0;

    private void Awake()
    {
        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }


    void Start()
    {
        _mainC = Camera.main;
        _playerInput.Player.Shoot.performed += ctx => FireBullet();
        _playerInput.Player.Secondary.performed += ctx => ShootBarrage();
    }

    IEnumerator CanShoot()
    {
        _canShoot = false;
        yield return new WaitForSeconds(.2f);
        _canShoot = true;
    }

    IEnumerator CanSecondary()
    {
        _canShoot = false;
        yield return new WaitForSeconds(.1f);
        _canShoot = true;
    }

    void Update()
    {
        if(isHeld)
        {
            FireBullet();
        }

        // Rotation
        Vector2 mouseScreenPosition = _playerInput.Player.MousePosition.ReadValue<Vector2>();
        Vector3 mouseWorldPosition = _mainC.ScreenToWorldPoint(mouseScreenPosition);
        Vector3 targetDirection = mouseWorldPosition - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        angle = angle - 90f;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

    }

    private void FireBullet()
    {
        if (!_canShoot) return;

        Vector2 mousePosition = _playerInput.Player.MousePosition.ReadValue<Vector2>();
        mousePosition = _mainC.ScreenToWorldPoint(mousePosition);
        GameObject bulletClone = Instantiate(bullet, bulletDirection.position, bulletDirection.rotation);
        bulletClone.SetActive(true);
        if(isHeld)
        {
            StartCoroutine(CanSecondary());
        }
        else
        {
            StartCoroutine(CanShoot());
        }
    }

    private void ShootBarrage()
    {
        if(!isHeld)
        {
            isHeld = true;
        }
        else
        {
            isHeld = false;
        }
    }
}
