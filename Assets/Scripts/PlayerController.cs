using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	public int player;

	public int maxHealth;
	public int health;
	public float speed;
	public float jumpForce;
	public bool canJump;
	public bool canDoubleJump;
	public float mass;
	public float fallMultiplier;
	public float sizeMultiplier;
	public bool invertVertical;
	public bool invertHorizontal;

	private Rigidbody2D rigidbody2d;
	private Animator animator;

	private float move = 0f;
	private bool grounded = false;
	private bool doubleJump = false;

    public float dashForce;
    public bool canDash = false;
	private DashState dashState = DashState.CAN_DASH;

	public Text score;
	private PlayerStats enemyStats;


	private Vector2 BELOW = new Vector2(0f, -0.01f);

	const int DEFAULT_LAYER = 0;
	const int GHOST_LAYER = 8;

	void Start ()
	{
		rigidbody2d = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		GameObject scoreObject = GameObject.Find("Score-" + player);
		if(scoreObject != null) score = scoreObject.GetComponent<Text>();

		GameObject enemyStatObject = GameObject.Find("Stats-" + ((player % 2) + 1));
		if(enemyStatObject != null)
		{
			enemyStats = enemyStatObject.GetComponent<PlayerStats>();
		}
		else
		{
			enemyStats = gameObject.AddComponent<PlayerStats>();
		}

		SetStats();

		transform.localScale = new Vector3(sizeMultiplier, sizeMultiplier, 1f);
		health = maxHealth;
	}
	
	void Update ()
	{
		move = Input.GetAxis("Horizontal-" + player);

		if (invertHorizontal) move *= -1;

		if (move < -0.1f)
		{
			animator.SetBool("Running", true);
			transform.localScale = new Vector3(-sizeMultiplier, sizeMultiplier, 1f);
		}
		else if (move > 0.1f)
		{
			animator.SetBool("Running", true);
			transform.localScale = new Vector3(sizeMultiplier, sizeMultiplier, 1f);
		}
		else
		{
			animator.SetBool("Running", false);
		}

		if(((Input.GetButtonDown("Jump-" + player) && !invertVertical) || (Input.GetAxis("Vertical-" + player) > 0.5f && invertVertical)) &&
			(canJump && (grounded || doubleJump)))
		{
			rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, 0f);
			rigidbody2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

			rigidbody2d.gravityScale = mass;

			if (grounded) grounded = false;
			else doubleJump = false;
		}

		if(((Input.GetAxis("Vertical-" + player) > 0.5f && !invertVertical) || (Input.GetButtonDown("Jump-" + player) && invertVertical)) &&
			grounded)
		{
			gameObject.layer = GHOST_LAYER;
			grounded = false;
			StartCoroutine(StopGhosting());
		}

        if(Input.GetAxis("Dash-" + player) != 0 && canDash && dashState == DashState.CAN_DASH)
        {
            dashState = DashState.DASHING;
            rigidbody2d.AddForce(
				new Vector2(dashForce * -Mathf.Sign(Input.GetAxis("Dash-" + player)), 0f),
				ForceMode2D.Impulse
			);
			animator.SetBool("Dashing", true);
			StartCoroutine(StopDashing());
        }

		if(health <= 0 || transform.position.y < -20f)
		{
			health = 0;

			Destroy(transform.GetChild(0).gameObject);
			animator.SetTrigger("Die");

			StartCoroutine(EndRound());

			rigidbody2d.bodyType = RigidbodyType2D.Static;
			enabled = false;
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

		if (!grounded && Physics2D.Raycast((Vector2)transform.position + BELOW, Vector2.down, 0.03f))
		{
			grounded = true;
			doubleJump = canDoubleJump;

			rigidbody2d.gravityScale = mass;
			animator.SetBool("Jumping", false);
		}
		if (grounded && !Physics2D.Raycast((Vector2)transform.position + BELOW, Vector2.down, 0.03f))
		{
			grounded = false;
			animator.SetBool("Jumping", true);
		}
	}

	void SetStats()
	{
		GameObject statObject = GameObject.Find("Stats-" + player);
		if (statObject != null)
		{
			PlayerStats playerStats = statObject.GetComponent<PlayerStats>();

			sizeMultiplier = playerStats.sizeMod;
			speed = 4f * playerStats.speedMod;
			fallMultiplier = playerStats.fallMult;
			maxHealth = Mathf.RoundToInt(10 * playerStats.hpMod);
			canDash = playerStats.hasDash;
			canDoubleJump = playerStats.hasDoubleJump;
			canJump = !playerStats.hasNoJump;
			invertVertical = playerStats.invertVertical;
			invertHorizontal = playerStats.invertHorizontal;

			score.text = playerStats.score.ToString();
		}
	}

	IEnumerator StopGhosting()
	{
		yield return new WaitForSeconds(0.1f);

		gameObject.layer = DEFAULT_LAYER;
	}

    IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(0.1f);

        dashState = DashState.WAITING;
		animator.SetBool("Dashing", false);

		yield return new WaitForSeconds(1f);

		dashState = DashState.CAN_DASH;
	}

	IEnumerator EndRound()
	{
		yield return new WaitForSeconds(4f);

		enemyStats.score++;

		Debug.Log(enemyStats.score);

		if (enemyStats.score < 2)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
		else
		{
			Destroy(GameObject.Find("Stats-1"));
			Destroy(GameObject.Find("Stats-2"));
			
			SceneManager.LoadScene("Player" + ((player % 2) + 1));
		}
	}

	enum DashState
	{
		CAN_DASH, DASHING, WAITING
	}
}
