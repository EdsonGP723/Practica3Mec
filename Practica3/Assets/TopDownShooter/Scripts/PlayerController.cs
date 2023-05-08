using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float Horizontal;
    private float Vertical;
	public Rigidbody2D rb;
    public float speed;   
    public Transform gun;
    public Transform shootPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    private bool canShoot= true;
    public float shootingTime;
    public Weapon weapon;
    public WeaponCollection weapons;
    private int actualIndex;
    public SpriteRenderer gunSprite;
  

    // Start is called before the first frame update
    void Start()
    {
        Weapon(0);
    }



    void Update()
    {
        Inputs();
	    Movimiento();
        MousePosition();
    }
    
	void Movimiento()
	{
		var dir = new Vector2(Horizontal, Vertical).normalized;

		rb.velocity = dir*speed;

		Debug.Log(rb.velocity);
	}
	
	void Weapon(int i)
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
    
	void MousePosition()
	{
		var screenPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		float anguloRadianes = Mathf.Atan2(screenPoint.y - transform.position.y, screenPoint.x - transform.position.x);
		float anguloGrados = (180 / Mathf.PI) * anguloRadianes - 90;

		gun.rotation = Quaternion.Euler(0, 0, anguloGrados);

	}



	IEnumerator Shoot()
	{
		var actualBullet = Instantiate(bulletPrefab, shootPoint.position, gun.rotation);
		actualBullet.GetComponent<Rigidbody2D>().velocity = actualBullet.transform.up * weapon.bulletSpeed;
		canShoot = false;

		var dir = shootPoint.position - transform.position;
		rb.AddForce( -dir * weapon.knockBack, ForceMode2D.Force);
		yield return new WaitForSeconds(weapon.shootingTime);
		canShoot = true;
	}


 
	
	void Inputs()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");

        
        if (Input.GetMouseButton(0)&&canShoot)
        {
            StartCoroutine(Shoot());
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Weapon(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Weapon(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Weapon(2);
        }
    }

   
}
