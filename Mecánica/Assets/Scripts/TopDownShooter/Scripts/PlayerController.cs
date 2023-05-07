using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float Horizontal;
    private float Vertical;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed;
    
    [SerializeField] Transform gun;
    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed;
    private bool canShoot= true;
    [SerializeField] float shootingTime;
    [SerializeField] Weapon weapon;
    [SerializeField] WeaponCollection weapons;
    private int actualIndex;
    [SerializeField] SpriteRenderer gunSprite;
    [SerializeField] Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        NewWeapon(0);
    }

    void NewWeapon(int i)
    {
        if (actualIndex != i)
        {
            StopAllCoroutines();
            canShoot = true;
        }
        actualIndex = i;

        
        weapon = weapons.Weapons[i];
        gunSprite.sprite = weapon.sprite;
       
        shootPoint.localPosition = weapon.ShootPoint;
    }


    // Update is called once per frame
    void Update()
    {
        GetInput();
        Movement();
        MousePosition();
    }


    void GetInput()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");

        if(Horizontal!=0||Vertical!=0)
        {
            anim.SetBool("IsMoving", true);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }
        if (Input.GetMouseButton(0)&&canShoot)
        {
            StartCoroutine(ShootingCoroutine());
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            NewWeapon(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            NewWeapon(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            NewWeapon(2);
        }
    }

    void MousePosition()
    {
        var screenPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float anguloRadianes = Mathf.Atan2(screenPoint.y - transform.position.y, screenPoint.x - transform.position.x);
        float anguloGrados = (180 / Mathf.PI) * anguloRadianes - 90;

        gun.rotation = Quaternion.Euler(0, 0, anguloGrados);

    }



    IEnumerator ShootingCoroutine()
    {
        var actualBullet = Instantiate(bulletPrefab, shootPoint.position, gun.rotation);
        actualBullet.GetComponent<Rigidbody2D>().velocity = actualBullet.transform.up * weapon.bulletSpeed;
        canShoot = false;

        var dir = shootPoint.position - transform.position;
        rb.AddForce( -dir * weapon.knockBack, ForceMode2D.Force);
        yield return new WaitForSeconds(weapon.shootingTime);
        canShoot = true;
    }

    void Movement()
    {
        var dir = new Vector2(Horizontal, Vertical).normalized;

        rb.velocity = dir*speed;

        Debug.Log(rb.velocity);
    }
}
