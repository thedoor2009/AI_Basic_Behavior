using UnityEngine;
using System.Collections;

public class FlockBehavior : MonoBehaviour {

	public GameObject ai_Manager;

	private AIManager3 ai;
	private float w_a = 0.0f;
	private float w_c = 0.0f;
	private float w_s = 0.0f;
	private float num = 0.0f;
	void Start () {
		ai_Manager = GameObject.Find ("AIManager");
		ai = ai_Manager.GetComponent<AIManager3> ();
	}

	void Update () {
		ai.weight_Alignment = w_a;
		ai.weight_Cohesion = w_c;
		ai.weight_Seperation = w_s;
	}

	void OnGUI () {
		w_a = GUI.HorizontalSlider (new Rect (25, 25, 100, 30), w_a, 0.0f, 1.0f);
		GUI.Label(new Rect( 125, 20, 100, 30),"  Aligement: " + w_a);

		w_c = GUI.HorizontalSlider (new Rect (25, 50, 100, 30), w_c, 0.0f, 1.0f);
		GUI.Label(new Rect( 125, 50, 100, 30),"  Cohesion: " + w_c);

		w_s = GUI.HorizontalSlider (new Rect (25, 75, 100, 30), w_s, 0.0f, 1.0f);
		GUI.Label(new Rect( 125, 75, 100, 30),"  Seperation: " + w_s);

		num = GUI.HorizontalSlider (new Rect (25, 100, 100, 30), num, 0.0f, 50.0f);
		GUI.Label(new Rect( 125, 100, 100, 30),"  Number: " + num);

		if (GUI.Button (new Rect (125, 125, 100, 30), "Add")) {
			ai.Add( (int)num );
		}
	}
}
