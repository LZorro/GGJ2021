using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]
public class Sprite_Script : MonoBehaviour
{
	public bool leftfacing;
	public float inversespeed;
	public float jump_framedelay;
	public Sprite[] sprites;
	public Sprite[] jump_sprites;
	public Transform relativeform;//Typically, this should be the transform of the camera.
	public Rigidbody rgdbdy;
	[HideInInspector] public float delta = 0;
	[HideInInspector] public int i = 0, j = nill;
	[HideInInspector] public SpriteRenderer sprite_rNdr;

	public const float v_epsilon = 0.12f;	//I figured since 0.12 was the deadzone for the joystick I'd use the same value here.
	public const int nill = int.MinValue;

	private Moveable moveable;

	void Start()
	{	sprite_rNdr = GetComponent<SpriteRenderer>();
		moveable = GetComponentInParent<Playable >(); 
	}

	void Update()
	{	Vector3 velocity = rgdbdy?.velocity ?? Vector3.zero;

		//v Flips the sprite based on the direction it is moving. v
		float horizontal_velocity = relativeform.InverseTransformDirection(velocity).x;
		/**/ if (horizontal_velocity >  v_epsilon) sprite_rNdr.flipX =  leftfacing;
		else if (horizontal_velocity < -v_epsilon) sprite_rNdr.flipX = !leftfacing;

		if (j != nill)									//Test if jumping
		{	delta += Time.deltaTime;					//Count how long you've been jumping
			if (delta >= jump_framedelay)				//if it is time to move to the next frame
			{	sprite_rNdr.sprite = jump_sprites[j++];	//set the sprite to the next one
				delta = 0;								//then reset the clock
				if (j >= jump_sprites.Length) j = nill;	//And if at the end of the gif j = nill
			}
		} else
		{	if (velocity.magnitude < v_epsilon)			//If not moving
			{	delta = 0;								//then stop the clock
				sprite_rNdr.sprite = sprites[0];		//and reset the sprite.
			} else										//otherwise
			{	delta += Time.deltaTime;				//count how long you've been running and ...
				if (velocity.magnitude * delta > inversespeed)
				{	i = (i+1) % sprites.Length;
					sprite_rNdr.sprite = sprites[i];
					delta = 0;
				}
			}
		}
	}

	void NextFrame(Sprite[] sheet)
	{	i = (i+1) % sheet.Length;
		sprite_rNdr.sprite = sheet[i];
		delta = 0;
	}

	public void Jump()
	{	sprite_rNdr.sprite = jump_sprites[0];
		delta = 0;
		j = 12;
	}
}
