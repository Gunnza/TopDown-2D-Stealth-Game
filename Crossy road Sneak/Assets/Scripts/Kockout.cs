using UnityEngine;
using System.Collections;

public class Kockout : MonoBehaviour {

	public GameObject playerObject;
	public GameObject ParentPlayer;
	public GameObject Score;
	public GameObject highScore;

	ClickToMove clickToMove;
	UpdateScore updateScore;
	Animator otherAnimator;
	AudioSource audio;
	public AudioClip chokeSfx;




	// Use this for initialization
	void Start () {

		audio = GetComponent<AudioSource>();
		updateScore = Score.GetComponent<UpdateScore>(); 
		updateScore.guardsKo = 0;
		ES2.Save(updateScore.guardsKo,  "savefile.txt?tag=workersKo");
		clickToMove = ParentPlayer.GetComponent<ClickToMove>();
		otherAnimator = playerObject.GetComponent<Animator> ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == ("EnemyKO"))
		{
			if(clickToMove.caught !=true)
			{
				Destroy(other.gameObject.transform.parent.gameObject);
				clickToMove.choking = true;
				audio.PlayOneShot(chokeSfx, 0.7F);
				updateScore.guardsKo += 1;
				if (updateScore.guardsKo > updateScore.highGuardsKo)
				{
					updateScore.highGuardsKo = updateScore.guardsKo;
					ES2.Save(updateScore.highGuardsKo,  "savefile.txt?tag=highWorkersKo");
				}
				ES2.Save(updateScore.guardsKo,  "savefile.txt?tag=workersKo");
				
				StartCoroutine(Knockout());
			}
		}
	}

	IEnumerator Knockout()
	{
		yield return new WaitForSeconds(2);
		clickToMove.choking = false;
	}
}


