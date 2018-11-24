﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	public int player;

	public int maxHealth;
	public int health;
	public float speed;
	public float jumpForce;
	public bool canDoubleJump;
	public float mass;
	public float fallMultiplier;

	private Rigidbody2D rigidbody2d;

	private float move = 0f;
	private bool grounded = false;
	private bool doubleJump = false;

    public float dashForce;
    public bool canDash = false;
    public float lastMove = 1f;
	private DashState dashState = DashState.CAN_DASH;
    
	private Vector2 BELOW = new Vector2(0f, -0.01f);

	const int DEFAULT_LAYER = 0;
	const int GHOST_LAYER = 8;

	void Start ()
	{
		rigidbody2d = GetComponent<Rigidbody2D>();

		health = maxHealth;
	}
	
	void Update ()
	{
		move = Input.GetAxis("Horizontal-" + player);
        if (move < 0.01f) lastMove = -1f;
        else if (move > 0.01f )lastMove = 1f;
        

		if(Input.GetButtonDown("Jump-" + player) && (grounded || doubleJump))
		{
			rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, 0f);
			rigidbody2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

			rigidbody2d.gravityScale = mass;

			if (grounded) grounded = false;
			else doubleJump = false;
		}

		if(Input.GetAxis("Vertical-" + player) > 0.5f && grounded)
		{
			gameObject.layer = GHOST_LAYER;
			grounded = false;
			StartCoroutine(StopGhosting());
		}

        if(Input.GetAxis("Dash-" + player) > 0 && canDash && dashState == DashState.CAN_DASH)
        {
            dashState = DashState.DASHING;
            rigidbody2d.AddForce(new Vector2(dashForce * lastMove, 0f), ForceMode2D.Impulse);
            StartCoroutine(StopDashing());
        }

		if(health <= 0 || transform.position.y < -20f)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	void FixedUpdate()
	{

        if (dashState != DashState.DASHING)
        {
		    rigidbody2d.velocity = new Vector2(move * speed, rigidbody2d.velocity.y);
        }

		if(rigidbody2d.velocity.y < 0)
		{
			rigidbody2d.gravityScale = mass * fallMultiplier;
		}

		if (!grounded && Physics2D.Raycast((Vector2)transform.position + BELOW, Vector2.down, 0.01f))
		{
			grounded = true;
			doubleJump = canDoubleJump;

			rigidbody2d.gravityScale = mass;
		}
		if (grounded && !Physics2D.Raycast((Vector2)transform.position + BELOW, Vector2.down, 0.01f))
		{
			grounded = false;
		}
	}

	IEnumerator StopGhosting()
	{
		yield return new WaitForSeconds(0.2f);

		gameObject.layer = DEFAULT_LAYER;
	}

    IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(0.1f);

        dashState = DashState.WAITING;

		yield return new WaitForSeconds(1f);

		dashState = DashState.CAN_DASH;
	}

	enum DashState
	{
		CAN_DASH, DASHING, WAITING
	}
}
