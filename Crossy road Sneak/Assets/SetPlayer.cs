using UnityEngine;
using System.Collections;

public class SetPlayer : MonoBehaviour {
	
	public GameObject normalPlayer;
	public  AnimatorOverrideController normalOverrideController;
	public  AnimatorOverrideController shyGuyOverrideController;
	public  AnimatorOverrideController TeederToesOverrideController;
	
	Animator animator;
	
	public void Start()
	{
		animator = normalPlayer.GetComponent<Animator>();
	}
	
	public void setNormalPlayer()
	{
	
	 AnimatorOverrideController overrideController = new AnimatorOverrideController();
 	 animator.runtimeAnimatorController = normalOverrideController;
	}
	public void setShyGuy()
	{
	
	 AnimatorOverrideController overrideController = new AnimatorOverrideController();
 	 animator.runtimeAnimatorController = shyGuyOverrideController;
	}
	public void setTeederToes()
	{
	 AnimatorOverrideController overrideController = new AnimatorOverrideController();
 	 animator.runtimeAnimatorController = TeederToesOverrideController;
	}
}
