using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look_At_Player : MonoBehaviour
{	
	public float speed;
	public Transform player_transform;
	public Transform camera_point;
	private Vector3 initial_location;
	private Quaternion initial_orientation;
	[HideInInspector]
	public float pitch;

	#region Rotation
	//WARNING: EULER ROTATIONS SUFFER FROM GIMBAL LOCK
	protected float Ex		{ get {return transform.eulerAngles.x;} set {transform.rotation = Quaternion.Euler(value, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);} }//Pitch(not necessarilly)
	protected float Ey		{ get {return transform.eulerAngles.y;} set {transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, value, transform.rotation.eulerAngles.z);} }//Yaw	(not necessarilly)
	protected float Ez		{ get {return transform.eulerAngles.z;} set {transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, value);} }//Roll	(not necessarilly)
	#endregion

#if EXTERNALCAMERA	//If the camera should not be a child of the player.

	void Start()
	{	initial_location = camera_point.localPosition;
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

#else
	//Changes an external camera setup to an internal one.
	void SetLocation()
	{	transform.parent = player_transform;
		transform.localPosition = camera_point.localPosition;
		transform.localRotation = camera_point.localRotation;
		Destroy(camera_point.gameObject);
	}

	void Start()
	{	SetLocation();
		
		initial_location = transform.localPosition;
		initial_orientation = transform.localRotation;
		pitch = 0;
	}

	void FixedUpdate() { pitch = -Input.GetAxis("RS_v"); }

	void Update()
	{	if (Input.GetButtonDown("Reset Camera")) ResetCamera();
		transform.Rotate(Vector3.right, pitch);

		float angle = Vector3.Angle(player_transform.up, transform.forward);//While this is not the most optimized way
		if (Mathf.Abs(angle) > 90) transform.Rotate(Vector3.right, -pitch);//to clamp an angle, the others didn't work.
		
		pitch = 0;
	}

	void ResetCamera()
	{	transform.localPosition = initial_location;
		transform.localRotation = initial_orientation;
		pitch = 0;
	}

#endif
}
