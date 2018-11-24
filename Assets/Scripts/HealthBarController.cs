using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
	public PlayerController playerController;
	public GameObject heart;
	public Sprite[] sprites;

	public int direction;

	private SpriteRenderer[] hearts;

	void Start()
	{
		int heartAmount = playerController.maxHealth;
		hearts = new SpriteRenderer[heartAmount];

		float x = 0;
		float y = 0;
		for (int i = 0; i < hearts.Length; i++)
		{
			GameObject newHeart = Instantiate(heart, transform);
			newHeart.transform.localPosition = new Vector3(x * direction, y, 0f);
			hearts[i] = newHeart.GetComponent<SpriteRenderer>();

			x += 0.5f;
			if(x >= 5)
			{
				x = 0;
				y -= 0.5f;
			}
		}

		transform.Translate(0f, 0.25f * Mathf.Floor((heartAmount - 1) / 10f), 0f);
	}
	
	void Update ()
	{
		for (int i = 0; i < Mathf.Max(Mathf.Min(playerController.health, hearts.Length), 0); i++)
		{
			hearts[i].sprite = sprites[0];
		}
		for(int i = Mathf.Max(playerController.health, 0); i < hearts.Length; i++)
		{
			hearts[i].sprite = sprites[1];
		}
	}
}
