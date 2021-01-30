using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class Sprite_Script : MonoBehaviour
{
	public bool leftfacing;
	public float inversespeed;
	public Sprite[] sprites;
	public Transform relativeform;//Typically, this should be the transform of the camera.
	public Rigidbody rgdbdy;
	[HideInInspector] public float delta = 0;
	[HideInInspector] public int i = 0;
	[HideInInspector] public SpriteRenderer sprite_rNdr;

	public const float v_epsilon = 0.12f;	//I figured since 0.12 was the deadzone for the joystick I'd use the same value here.


	void Start() { sprite_rNdr = GetComponent<SpriteRenderer>(); }

	void Update()
	{	Vector3 velocity = rgdbdy?.velocity ?? Vector3.zero;

		//v Flips the sprite based on the direction it is moving. v
		float horizontal_velocity = relativeform.InverseTransformDirection(velocity).x;
		/**/ if (horizontal_velocity >  v_epsilon) sprite_rNdr.flipX =  leftfacing;
		else if (horizontal_velocity < -v_epsilon) sprite_rNdr.flipX = !leftfacing;
		
		if (velocity.magnitude < v_epsilon)		//If not moving
		{	delta = 0;							//then stop the clock
			sprite_rNdr.sprite = sprites[0];	//and reset the sprite.
		} else									//otherwise
		{	delta += Time.deltaTime;			//count how long you've been running and ...
			if (velocity.magnitude * delta > inversespeed)
			{	NextFrame();
				delta = 0;
			}
		}
	}

	void NextFrame()
	{	i = (i+1) % sprites.Length;
		sprite_rNdr.sprite = sprites[i];
	}
}
