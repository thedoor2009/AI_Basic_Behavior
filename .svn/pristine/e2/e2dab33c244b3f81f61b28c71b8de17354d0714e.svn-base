using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIManager3 : MonoBehaviour {

	public List<GameObject> Heros = new List<GameObject>();
	public List<GameObject> Enemys = new List<GameObject>();
	public Transform Prefab_Hero;
	public Transform Prefab_Enemy;
	public float weight_Alignment = 0.0f;
	public float weight_Cohesion = 0.0f;
	public float weight_Seperation = 0.0f;
	
	private Transform Hero;
	private Transform Enemy;
	private int _findHeroTimer = 0;
	
	void Start () {
		initialization ();
	}
	
	void Update () {
		Find ();
		foreach( GameObject hero in Heros ){
			if (!hero.renderer.isVisible) {
				hero.transform.position = new Vector3( -hero.transform.position.x, 0.0f, -hero.transform.position.z ); 
				//hero.transform.position = new Vector3( 0.0f, 0.0f, 0.0f ); 
			}
		}
	}
	
	void initialization(){
		for (int i = 0; i < Random.Range( 150, 200 ); i ++) {
			Vector3 initlizePosition_Hero = new Vector3( Random.Range( -25.0f, 25.0f ), 1.0f, Random.Range( -25.0f, 25.0f ) );
			Hero = (Transform)Instantiate (Prefab_Hero, initlizePosition_Hero, Quaternion.identity);
		}

		for (int i = 0; i < Random.Range( 4, 6 ); i ++) {
			Vector3 initlizePosition_Enemy = new Vector3( Random.Range( -25.0f, 25.0f ), 1.0f, Random.Range( -25.0f, 25.0f ) );
			Enemy = (Transform)Instantiate (Prefab_Enemy, initlizePosition_Enemy, Quaternion.identity);
			Enemy.transform.localScale = new Vector3( 3.0f,3.0f,3.0f);
		}
	}

	public void Find(){
		if( _findHeroTimer == 0 ){
			GameObject[] HeroArray = GameObject.FindGameObjectsWithTag("Player");
			Heros.AddRange (HeroArray);

			GameObject[] EnemyArray = GameObject.FindGameObjectsWithTag("Enemy");
			Enemys.AddRange (HeroArray);

			_findHeroTimer++; 
		}
	}
	
	public void Add(int i_num ){
		_findHeroTimer = 0;

		for (int i = 0; i < i_num; i ++) {
			Vector3 initlizePosition_Hero = new Vector3( Random.Range( -25.0f, 25.0f ), 1.0f, Random.Range( -25.0f, 25.0f ) );
			Hero = (Transform)Instantiate (Prefab_Hero, initlizePosition_Hero, Quaternion.identity);
		}
	}
}
