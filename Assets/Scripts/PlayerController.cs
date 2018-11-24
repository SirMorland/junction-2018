using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	public int player;

	public float speed;
	public float jumpForce;
	public bool canDoubleJump;
	public float mass;
	public float fallMultiplier;

	private Rigidbody2D rigidbody2d;

	private float move = 0f;
	private bool grounded = false;
	private bool doubleJump = false;

	private Vector2 BELOW = new Vector2(0f, -0.01f);

	const int DEFAULT_LAYER = 0;
	const int GHOST_LAYER = 8;

	void Start ()
	{
		rigidbody2d = GetComponent<Rigidbody2D>();
	}
	
	void Update ()
	{
		move = Input.GetAxis("Horizontal-" + player);

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

		if(transform.position.y < -20f)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	void FixedUpdate()
	{
		rigidbody2d.velocity = new Vector2(move * speed, rigidbody2d.velocity.y);

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
}
