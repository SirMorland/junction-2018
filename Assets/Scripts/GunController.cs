using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
	public GameObject bullet;

	public float fireRate;

	private int player;
	private float lastTime;

	void Start ()
	{
		player = transform.parent.GetComponent<PlayerController>().player;
		lastTime = Time.time;
	}
	
	void Update ()
	{
		float horizontal = Input.GetAxis("Look-Horizontal-" + player);
		float vertical = -Input.GetAxis("Look-Vertical-" + player);

		if (horizontal != 0 || vertical != 0)
		{
			transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Round(Mathf.Rad2Deg * Mathf.Atan2(vertical, horizontal)/45f)* 45f);
		}

		float currentTime = Time.time;
		if(Input.GetButton("Shoot-" + player) && currentTime - lastTime > fireRate)
		{
			GameObject newBullet = Instantiate(bullet);
			newBullet.transform.rotation = transform.rotation;
			newBullet.transform.position = transform.position;
			newBullet.transform.Translate(1.1f, 0f, 0f);
			lastTime = currentTime;
		}
	}
}
