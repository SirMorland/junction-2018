using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
	public SpriteRenderer spriteRenderer;
	Text text;

	PlayerController[] players;
	bool gameOver = false;

	void Start ()
	{
		text = transform.GetChild(0).GetComponent<Text>();

		GameObject[] po = GameObject.FindGameObjectsWithTag("Player");
		players = new PlayerController[po.Length];

		for(int i = 0; i < po.Length; i++)
		{
			players[i] = po[i].GetComponent<PlayerController>();
		}
	}
	
	void Update ()
	{
		if(!gameOver)
		{
			for(int i = 0; i < players.Length; i++)
			{
				if(players[i].health <= 0)
				{
					text.text = "Player " + players[(i + 1) % 2].player + "\nwins!";

					StartCoroutine(Animtion());
					gameOver = true;
					break;
				}
			}
		}
	}

	IEnumerator Animtion()
	{
		while(true)
		{
			spriteRenderer.color = Color.Lerp(spriteRenderer.color, new Color(1f, 1f, 1f, 0.8f), Time.deltaTime);

			yield return null;
		}
	}
}
