using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage : MonoBehaviour
{
	public GameObject afterImage;

	void Start ()
	{
		
	}
	
	void Update ()
	{
		Instantiate(afterImage, transform.position, transform.rotation);
	}
}
