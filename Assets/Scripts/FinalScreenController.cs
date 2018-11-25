using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalScreenController : MonoBehaviour
{	
	void Update ()
	{
		if(Input.GetButtonDown("SelectCard-1"))
		{
			SceneManager.LoadScene(0);
		}
	}
}
