using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
	public Sprite[] guns;

	public GameObject bullet;
	public GameObject laser;

	public float fireRate;
	public float accuracy;
	public int bullets;
	public float damage;
	public float bulletSpeed;
	public bool railgun;

	private SpriteRenderer spriteRenderer;
	private Transform playerTransform;

	private int player;
	private float lastTime;

	void Start ()
	{
		player = transform.parent.GetComponent<PlayerController>().player;
		playerTransform = transform.parent;
		spriteRenderer = GetComponent<SpriteRenderer>();
		lastTime = Time.time;

		SetStats();
	}
	
	void Update ()
	{
		float horizontal = Input.GetAxis("Look-Horizontal-" + player);
		float vertical = -Input.GetAxis("Look-Vertical-" + player);

		if (horizontal != 0 || vertical != 0)
		{
			float angle = Mathf.Round(Mathf.Rad2Deg * Mathf.Atan2(vertical, horizontal) / 45f) * 45f;
			transform.rotation = Quaternion.Euler(0f, 0f, angle);
			
			if(angle <= 90 && angle > -90)
			{
				spriteRenderer.flipY = false;
			}
			else
			{
				spriteRenderer.flipY = true;
			}
		}

		if(playerTransform.localScale.x > 0)
		{
			spriteRenderer.flipX = false;
		}
		else
		{
			spriteRenderer.flipX = true;
		}

		float currentTime = Time.time;
		if(Input.GetButton("Shoot-" + player) && currentTime - lastTime > fireRate)
		{
            float spread = 20f - accuracy;
            if (spread < 0f) spread = 0f;
			for (int i = 0; i < bullets; i++)
			{
				GameObject newBullet;
				if (!railgun) newBullet = Instantiate(bullet);
				else newBullet = Instantiate(laser);
				newBullet.transform.rotation = transform.rotation;
				newBullet.transform.position = transform.position;
				newBullet.transform.Translate(0.75f * playerTransform.localScale.y, 0f, 0f);
				newBullet.transform.Rotate(0f, 0f, Random.Range(-spread, spread));

				BulletController bc = newBullet.GetComponent<BulletController>();
				bc.damage = damage;
				bc.speed = bulletSpeed;

				lastTime = currentTime;
			}
		}
	}

	void SetStats()
	{
		GameObject statObject = GameObject.Find("Stats-" + player);

		if (statObject != null)
		{
			PlayerStats playerStats = statObject.GetComponent<PlayerStats>();

			switch (playerStats.weapon)
			{
				case "Sniper rifle":
					fireRate = 1.5f;
					accuracy = 20;
					bullets = 1;
					damage = 5;
					bulletSpeed = 50;
					spriteRenderer.sprite = guns[0];
					break;
				case "Uzi":
					fireRate = 0.2f;
					accuracy = 5;
					bullets = 1;
					damage = 2;
					bulletSpeed = 35;
					spriteRenderer.sprite = guns[1];
					break;
				case "Shotgun":
					fireRate = 1.3f;
					accuracy = 0;
					bullets = 5;
					damage = 2;
					bulletSpeed = 35;
					spriteRenderer.sprite = guns[2];
					break;
				case "Assault rifle":
					fireRate = 0.3f;
					accuracy = 10;
					bullets = 1;
					damage = 3;
					bulletSpeed = 35;
					spriteRenderer.sprite = guns[3];
					break;
				case "Railgun":
					fireRate = 1.7f;
					accuracy = 15;
					bullets = 1;
					damage = 5;
					bulletSpeed = 0;
					railgun = true;
					spriteRenderer.sprite = guns[4];
					break;
				case "Rifle":
					fireRate = 1f;
					accuracy = 15;
					bullets = 1;
					damage = 3;
					bulletSpeed = 45;
					spriteRenderer.sprite = guns[5];
					break;
				default:
					break;
			}

			damage *= playerStats.damageMod;
			fireRate *= playerStats.fireRateMod;
			accuracy *= playerStats.accuracyMod;
		}
	}
}
