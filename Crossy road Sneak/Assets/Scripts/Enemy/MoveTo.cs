 using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//example. moving between some points at random
[RequireComponent(typeof(PolyNavAgent))]
public class MoveTo : MonoBehaviour{


	public GameObject redEnemy;
	//Animator otherAnimator;



	public List<Vector2> WPoints = new List<Vector2>();

	private PolyNavAgent _agent;
	public PolyNavAgent agent{
		get
		{
			if (!_agent)
				_agent = GetComponent<PolyNavAgent>();
			return _agent;			
		}
	}

	void Start(){


		//otherAnimator = redEnemy.GetComponent<Animator> ();
		
		if (WPoints.Count != 0)
		{
			agent.SetDestination(WPoints[ Random.Range(0, WPoints.Count) ]);
			//otherAnimator.CrossFade("Walk", 0f);
		}
	}

	//Message from agent
	void OnDestinationReached(){

		//otherAnimator.CrossFade("PlayerIdle", 0f);
	}


	void OnDrawGizmosSelected(){

		for ( int i = 0; i < WPoints.Count; i++)
			Gizmos.DrawSphere(WPoints[i], 0.05f);			
	}
}
