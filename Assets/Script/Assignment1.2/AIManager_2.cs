using UnityEngine;
using System.Collections;

public class AIManager_2 : MonoBehaviour {

	public Transform Prefab_Hero;
	public Transform Prefab_Obstacle;

	private Transform Hero;

	void Start () {
		initialization ();
	}

	void Update () {

		//if( Vector3.Distance (Hero.transform.position, new Vector3( 0.0f, 0.0f, 0.0f ) ) > 30.0f ){
			//Destroy( Hero.gameObject );
			//Reset();
		//}
		if (!Hero.gameObject.renderer.isVisible) {
			Hero.transform.position = new Vector3( -Hero.transform.position.x, 0.0f, -Hero.transform.position.z ); 
		}
	}

	void initialization(){
		Vector3 initlizePosition_Hero = new Vector3( Random.Range( -5.0f, 5.0f ), 1.0f, Random.Range( -5.0f, 5.0f ) );
		
		Hero = (Transform)Instantiate (Prefab_Hero, initlizePosition_Hero, Quaternion.identity);
		
		for (int i = 0; i < Random.Range( 18, 24 ); i ++) {
			Vector3 initlizePosition_Obstacle = new Vector3( Random.Range( -27.0f, 27.0f ), 0.0f, Random.Range( -10.0f, 10.0f ) );
			Transform Obstacle = (Transform)Instantiate (Prefab_Obstacle, initlizePosition_Obstacle, Quaternion.identity);
			Obstacle.transform.eulerAngles = new Vector3(347, 112, 94);
			float scale = Random.Range( 1f, 6.0f );
			Obstacle.transform.localScale = new Vector3( scale,scale,scale);
			Obstacle.renderer.material.color = Color.white;
			Obstacle.GetComponent<ObstacleTrigger>().radius = scale * 0.7f;
			Obstacle.transform.tag = "Obstacle";
		}
	}

	void Reset(){
		Vector3 initlizePosition_Hero = new Vector3( Random.Range( -5.0f, 5.0f ), 1.0f, Random.Range( -5.0f, 5.0f ) );
		
		Hero = (Transform)Instantiate (Prefab_Hero, initlizePosition_Hero, Quaternion.identity);
	}
}
