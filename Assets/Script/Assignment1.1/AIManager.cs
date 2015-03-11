using UnityEngine;
using System.Collections;

public class AIManager : MonoBehaviour {

	public Transform Prefab_Predator;
	public Transform Prefab_Target;
	public Transform Prefab_Prey;
	public Transform Prefab_Estimate;

	public PredatorController.AIType Predator_AI_Type;
	public PreyController.AIType Prey_AI_Type;
	public TargetController.ControlType Target_Control_Type;

	private Transform Target;
	private Transform Predator;
	private Transform Prey;
	private Transform Estimate;

	private TargetController Target_Controller;
	private PredatorController Predator_Controller;
	private PreyController Prey_Controller;

	void Start () {
		Vector3 initlizePosition_Predator = new Vector3( Random.Range( -5.0f, 5.0f ), 1.0f, Random.Range( -5.0f, 5.0f ) );
		Vector3 initlizePosition_Prey = new Vector3( -Random.Range( -6.0f, 6.0f ), 1.0f, -Random.Range( -6.0f, 6.0f ) );
		Vector3 initlizePosition_Target = new Vector3( -Random.Range( -6.0f, 6.0f ), 1.0f, -Random.Range( -6.0f, 6.0f ) );

		Target = (Transform)Instantiate (Prefab_Target, initlizePosition_Target, Quaternion.identity);
		Predator = (Transform)Instantiate (Prefab_Predator, initlizePosition_Predator, Quaternion.identity); 
		Prey = (Transform)Instantiate (Prefab_Prey, initlizePosition_Prey, Quaternion.identity);
		Estimate = (Transform)Instantiate (Prefab_Estimate, Target.transform.position, Quaternion.identity);

		Target.gameObject.tag = "Target";
		Predator.gameObject.tag = "Predator";
		Prey.gameObject.tag = "Prey";

		Target_Controller = Target.GetComponent<TargetController> ();
		Target_Controller.control_type = Target_Control_Type;

		Predator_Controller = Predator.GetComponent<PredatorController> ();
		Predator_Controller.ai_type = Predator_AI_Type;
		Predator_Controller.Target = Target.gameObject;

		Prey_Controller = Prey.GetComponent<PreyController> ();
		Prey_Controller.ai_type = Prey_AI_Type;
		Prey_Controller.Target = Target.gameObject;
	}

	void Update () {

		Estimate.transform.position = Predator_Controller.future_Positon;

		if( Vector3.Distance (Prey.transform.position, new Vector3( 0.0f, 0.0f, 0.0f ) ) > 20.0f ||
		    Vector3.Distance (Predator.transform.position, new Vector3( 0.0f, 0.0f, 0.0f ) ) > 20.0f){
			Destroy( Target.gameObject );
			Destroy( Predator.gameObject );
			Destroy( Prey.gameObject );
			Destroy( Estimate.gameObject );
			Reset();
		}
	}

	void Reset(){
		Vector3 initlizePosition_Predator = new Vector3( Random.Range( -5.0f, 5.0f ), 1.0f, Random.Range( -5.0f, 5.0f ) );
		Vector3 initlizePosition_Prey = new Vector3( -Random.Range( -6.0f, 6.0f ), 1.0f, -Random.Range( -6.0f, 6.0f ) );
		Vector3 initlizePosition_Target = new Vector3( -Random.Range( -6.0f, 6.0f ), 1.0f, -Random.Range( -6.0f, 6.0f ) );
		
		Target = (Transform)Instantiate (Prefab_Target, initlizePosition_Target, Quaternion.identity);
		Predator = (Transform)Instantiate (Prefab_Predator, initlizePosition_Predator, Quaternion.identity); 
		Prey = (Transform)Instantiate (Prefab_Prey, initlizePosition_Prey, Quaternion.identity);
		Estimate = (Transform)Instantiate (Prefab_Estimate, Target.transform.position, Quaternion.identity);

		Target.gameObject.tag = "Target";
		Predator.gameObject.tag = "Predator";
		Prey.gameObject.tag = "Prey";

		Target_Controller = Target.GetComponent<TargetController> ();
		Target_Controller.control_type = Target_Control_Type;
		
		Predator_Controller = Predator.GetComponent<PredatorController> ();
		Predator_Controller.ai_type = Predator_AI_Type;
		Predator_Controller.Target = Target.gameObject;
		
		Prey_Controller = Prey.GetComponent<PreyController> ();
		Prey_Controller.ai_type = Prey_AI_Type;
		Prey_Controller.Target = Target.gameObject;
	}
}
