using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look_At_Player : MonoBehaviour
{	
	public float speed;
	public Moveable player;
	public Transform camera_point;
	private Vector3 initial_location;//Thus far unused, as camera point doesn't change.
	[HideInInspector]
	public float pitch;
	private Transform player_transform;

	void Start()
	{	player_transform = player.transform;
		initial_location = camera_point.localPosition;
		pitch = 0;
	}

	void FixedUpdate()
		{ pitch = Mathf.Clamp(pitch - Input.GetAxis("RS_v"), -90, 90); }

	void Update()
	{	transform.position = Vector3.Lerp(transform.position, camera_point.position, speed);
		if (Input.GetButtonDown("Reset Camera")) ResetCamera();
		transform.LookAt(player_transform, Vector3.up);
		transform.Rotate(Vector3.right, pitch);
	}

	void ResetCamera()
	{	camera_point.localPosition = initial_location;//Not currently needed.
		transform.position = camera_point.position;
		pitch = 0;
	}
}
