﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	public float speed;
	public int damage;

	void Start ()
	{
		GetComponent<Rigidbody2D>().velocity = transform.right * speed;
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.collider.tag == "Player")
		{
			collision.collider.GetComponent<PlayerController>().health -= damage;
		}

		Destroy(gameObject);
	}
}
