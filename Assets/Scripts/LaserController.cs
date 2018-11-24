using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
	void Start ()
	{
		StartCoroutine(Wait());
	}

	IEnumerator Wait()
	{
		yield return new WaitForSeconds(1f);

		Destroy(gameObject);
	}
}
