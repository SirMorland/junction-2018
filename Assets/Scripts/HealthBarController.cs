using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
	public PlayerController playerController;
	public GameObject heart;
	public Sprite[] sprites;

	private SpriteRenderer[] hearts;

	void Start()
	{
		int heartAmount = playerController.maxHealth;
		hearts = new SpriteRenderer[heartAmount];

		int x = 0;
		int y = 0;
		for (int i = 0; i < hearts.Length; i++)
		{
			GameObject newHeart = Instantiate(heart, transform);
			newHeart.transform.localPosition = new Vector3(x, y, 0f);
			hearts[i] = newHeart.GetComponent<SpriteRenderer>();

			x++;
			if(x >= 10)
			{
				x = 0;
				y--;
			}
		}
	}
	
	void Update ()
	{
		for(int i = 0; i < Mathf.Max(Mathf.Min(playerController.health, hearts.Length), 0); i++)
		{
			hearts[i].sprite = sprites[0];
		}
		for(int i = Mathf.Max(playerController.health, 0); i < hearts.Length; i++)
		{
			hearts[i].sprite = sprites[1];
		}
	}
}
