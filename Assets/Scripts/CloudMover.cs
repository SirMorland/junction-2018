using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMover : MonoBehaviour {
    private float speed = 5;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x + 0.01f * speed, gameObject.transform.position.y, 0f);
        if (gameObject.transform.position.x >= 12f)
        {
            gameObject.transform.position = new Vector3(-12f, gameObject.transform.position.y, 0f);
            speed = Random.Range(1f, 5f);
        }
	}
}
