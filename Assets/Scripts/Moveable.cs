using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(Collider))]
public class Moveable : MonoBehaviour
{	
	protected Rigidbody phys;
	public new Collider collider;

	[HideInInspector] public const float epsilon = 0.001f;	//Miniscule amount

	/// Transform Shortcuts
	#region Rotation
	//WARNING: EULER ROTATIONS SUFFER FROM GIMBAL LOCK
	protected float Ex		{ get {return transform.eulerAngles.x;} set {transform.rotation = Quaternion.Euler(value, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);} }//Roll  (not necessarilly)
	protected float Ey		{ get {return transform.eulerAngles.y;} set {transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, value, transform.rotation.eulerAngles.z);} }//Pitch (not necessarilly)
	protected float Ez		{ get {return transform.eulerAngles.z;} set {transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, value);} }//Yaw   (not necessarilly)
	protected Vector3 DRot	{ get {return phys.angularVelocity;}	set { phys.angularVelocity = value; } }
	#endregion
	#region Scale
	protected float Cx { get {return transform.localScale.x;} set {transform.localScale = new Vector3(value, transform.localScale.y, transform.localScale.z);} }
	protected float Cy { get {return transform.localScale.y;} set {transform.localScale = new Vector3(transform.localScale.x, value, transform.localScale.z);} }
	protected float CZ { get {return transform.localScale.z;} set {transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, value);} }
	#endregion

	// Use this for initialization
	protected virtual void Init ()
	{	phys	= gameObject.GetComponent<Rigidbody>();
		collider = transform.GetComponent<BoxCollider>();
	}

	protected static bool Suspended { get{ return Public_Const.Suspended; } }

	//Function to check whether or not the player is on solid ground.
	public Collider On()
	{	float offset = Public_Const.Contactoffset;
		Bounds bowns = collider.bounds;
		Collider[] colliders = Physics.OverlapBox
		(	new Vector3(bowns.center.x			, bowns.min.y	, bowns.center.z		),
			new Vector3(bowns.extents.x-offset	, offset		, bowns.extents.z-offset),
			Quaternion.identity//transform.rotation,
			/*ContactFilter,*/
			/*results,*/ //This is where the results go on some functions, can be ignored since I only need to know that there was a collision.
			//Public_Const.Solid
		);
		if (colliders.Length > 1) return colliders[0];
		else return null;
	}


	public static Vector2 V(float x, float y)			=> new Vector2(x, y);
	public static Vector3 V(float x, float y, float z)	=> new Vector3(x, y, z);

	public static Vector2 VectorFormat(float magnitude, float angle)
		=> new Vector2(magnitude*Mathf.Cos(angle*Mathf.Deg2Rad), magnitude*Mathf.Sin(angle*Mathf.Deg2Rad));

	public Vector3 Local2World(Vector3 vector3) => transform.TransformDirection(vector3);
	public Vector3 World2Local(Vector3 vector3) => transform.InverseTransformDirection(vector3);
}
