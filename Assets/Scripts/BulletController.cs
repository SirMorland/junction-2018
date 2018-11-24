using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	public float speed;

	void Start ()
	{
		GetComponent<Rigidbody2D>().velocity = transform.right * speed;
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		Destroy(gameObject);
	}
}
