using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	public float speed;
	public float damage;

	void Start ()
	{
		GetComponent<Rigidbody2D>().velocity = transform.right * speed;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag == "Player")
		{
			collision.collider.GetComponent<PlayerController>().health -= Mathf.RoundToInt(damage);
		}

		if (!gameObject.name.Contains("laser"))
		{
			Destroy(gameObject);
		}
	}
}
