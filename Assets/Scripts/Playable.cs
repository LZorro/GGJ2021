﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Playable : Moveable
{
	public new GameObject camera;
	public Rigidbody body;
	public float speed;
	public float rotation_speed;
	public float jump_power;

	private Collider floor_collider;
	private float horz, latr, angu;

    public GameObject nearbyCharacter = null;

	void Start ()
	{	Init();
	}
	
	void FixedUpdate ()
	{	var direction = camera.transform.TransformDirection(V(horz, 0, latr));
		var vector = speed * V(direction.x, 0, direction.z).normalized;
		body.velocity = V(vector.x, body.velocity.y, vector.z);
		if (horz != 0 || angu != 0)
		{	transform.Rotate( V(0, 0, horz*rotation_speed) );
			transform.Rotate( V(0, 0, angu*rotation_speed) );	}
	}

	void Update ()
	{	horz = Input.GetAxis("Horizontal");
		latr = Input.GetAxis( "Vertical" );
		angu = Input.GetAxis("RS_h");//Horizontal Right Stick
		if (Input.GetButtonDown("Jump") && On()) Jump();
        if (Input.GetButtonDown("Fire1") && nearbyCharacter != null)
        {
            nearbyCharacter.GetComponent<CharacterDialog>().advanceDialog();
        }
    }

	
	private void Jump()
		=> phys.AddForce(transform.forward * jump_power);


	private IEnumerator OnCollisionEnter(Collision collision)
	{	GameObject hit = collision.gameObject;
		yield return null;
	}

	private IEnumerator OnTriggerEnter(Collider collider)
	{	GameObject hit = collider.gameObject;
        if (hit.tag.Equals("Character"))
        {
            nearbyCharacter = hit;
            hit.GetComponent<CharacterDialog>().enablePrompt(true);
        }
		yield return null;
	}

    private void OnTriggerExit(Collider other)
    {
        GameObject hit = other.gameObject;
        if (hit.tag.Equals("Character"))
        {
            hit.GetComponent<CharacterDialog>().enablePrompt(false);
            nearbyCharacter = null;
        }
    }
}
