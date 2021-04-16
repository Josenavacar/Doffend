using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    
    [SerializeField]
    public GameObject bullet;
    public GameObject bullet2;

    [SerializeField]
    public Transform bulletDirection;

    private PlayerInput _playerInput;

    public PlayerController playerController;

    private bool _canShoot = true;
    private Camera _mainC;

    public Transform bullets;

    private bool isHeld = false;
    //private static int shotsFired = 0;

    private AudioSource _audioSrc;

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
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        bullets = GameObject.Find("Bullets").transform;


        _mainC = Camera.main;
        _playerInput.Player.Shoot.performed += ctx => FireBullet();
        _playerInput.Player.Secondary.performed += ctx => ShootBarrage();

        _audioSrc = GetComponent<AudioSource>();
    }

    void PlayFireballSound()
    {
        _audioSrc.pitch = Random.Range(0.9f, 1.1f);
        //play oneshot
        _audioSrc.PlayOneShot(_audioSrc.clip);

    }

    IEnumerator CanShoot()
    {
        _canShoot = false;
        yield return new WaitForSeconds(.5f);
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
            playerController.moveAllow = false;
            FireSecondary();
        }
        else
        {
            playerController.moveAllow = true;
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

        PlayFireballSound();

        Vector2 mousePosition = _playerInput.Player.MousePosition.ReadValue<Vector2>();
        mousePosition = _mainC.ScreenToWorldPoint(mousePosition);
        GameObject bulletClone = Instantiate(bullet, bulletDirection.position, bulletDirection.rotation);
        bulletClone.transform.parent = bullets;
        bulletClone.SetActive(true);
        
        StartCoroutine(CanShoot());
    }

    private void FireSecondary()
    {
        if (!_canShoot) return;

        Vector2 mousePosition = _playerInput.Player.MousePosition.ReadValue<Vector2>();
        mousePosition = _mainC.ScreenToWorldPoint(mousePosition);
        GameObject bulletClone = Instantiate(bullet2, bulletDirection.position, bulletDirection.rotation);
        bulletClone.transform.parent = bullets;
        bulletClone.SetActive(true);
        
        if(isHeld)
        {
            StartCoroutine(CanSecondary());
        }
    }

    private void ShootBarrage()
    {
        if(!isHeld)
        {
            isHeld = true;
            playerController.moveAllow = false;
            //playerController.enabled = false;
        }
        else
        {
            playerController.moveAllow = true;
            isHeld = false;
            //playerController.enabled = true;
        }
    }
}
