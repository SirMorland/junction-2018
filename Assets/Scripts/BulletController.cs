using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	public float speed;

	void Start ()
	{
		
	}
	
	void Update ()
	{
		transform.Translate(Time.deltaTime * speed, 0f, 0f);
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		Destroy(gameObject);
	}
}
