using UnityEngine;
using System.Collections;

public class TargetController : MonoBehaviour {

	public Vector3 random_Speed;
	public float controlSpeed = 10.0f;
	public enum ControlType{
		Automatic = 1,
		Control   = 2,
	}
	public ControlType control_type;
	public int lengthOfLineRenderer = 2;

	private CharacterController characterController;
	private float verticalVelocity = 0;
	
	void Start () {
		Screen.lockCursor = true;
		characterController = GetComponent<CharacterController>();

		random_Speed = new Vector3 (Random.Range (-5.0f, 5.0f), 0.0f, Random.Range (-5.0f, 5.0f));

		LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
		lineRenderer.SetWidth(0.2F, 0.2F);
		lineRenderer.SetVertexCount(lengthOfLineRenderer);
	}

	void Update () {

		switch (control_type) {
		case ControlType.Automatic:{
			Automatic();
			break;
		}
		case ControlType.Control:{
			Control();
			break;
		}
		}
		Render ();
	}
	void Automatic()
	{
		characterController.Move (random_Speed * Time.deltaTime);
	}

	void Control()
	{
		float forwardSpeed = Input.GetAxis("Vertical") * controlSpeed;
		float sideSpeed = Input.GetAxis("Horizontal") * controlSpeed;
		if(Input.GetKey(KeyCode.LeftShift))
		{
			forwardSpeed = Input.GetAxis("Vertical") * controlSpeed *2.4f;
			sideSpeed = Input.GetAxis("Horizontal") * controlSpeed *2.4f;
		}
		
		verticalVelocity += Physics.gravity.y * Time.deltaTime;
		
		Vector3 speed = new Vector3(sideSpeed,verticalVelocity,forwardSpeed);
		speed = transform.rotation * speed;
		
		characterController.Move(speed * Time.deltaTime);
	}

	void Render(){
		this.GetComponent<LineRenderer> ().SetPosition (0, this.transform.position);
		this.GetComponent<LineRenderer> ().SetPosition (1, this.transform.position + random_Speed * 1.0f );
	}

}
