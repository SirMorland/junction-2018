using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
	public GameObject bullet;

	public float fireRate;
	public float accuracy;
	public int bullets;

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

		if(playerTransform.localScale.x > -1)
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
				GameObject newBullet = Instantiate(bullet);
				newBullet.transform.rotation = transform.rotation;
				newBullet.transform.position = transform.position;
				newBullet.transform.Translate(0.75f, 0f, 0f);
				newBullet.transform.Rotate(0f, 0f, Random.Range(-spread, spread));
				lastTime = currentTime;
			}
		}
	}
}
