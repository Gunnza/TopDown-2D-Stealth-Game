using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour
{
	private Color myColor;
	private float alpha = 1f;
	private float speed = 1;
	
	private float targetAlpha = 1;
	
	void Update()
	{
		if( Input.GetKeyDown( KeyCode.RightArrow ) )
		{
			Debug.Log( "Fading Out" );
			targetAlpha = 0;
		}
		
		if( Input.GetKeyDown( KeyCode.LeftArrow ) )
		{
			Debug.Log( "Fading In" );
			targetAlpha = 1;
		}
		
		if( !Mathf.Approximately( alpha, targetAlpha ) )
		{
			alpha = Mathf.MoveTowards( alpha, targetAlpha, speed * Time.deltaTime );
		}
	}
	
	void OnGUI ()
	{
		GUI.Window(0, new Rect( 10, 10, 300, 300 ), pauseWindow, "");
	}
	
	void pauseWindow (int windowID) 
	{
		myColor = GUI.color;
		myColor.a = alpha;
		GUI.color = myColor;
		GUI.Label(new Rect(100,100,100,100), "Fading Text");
		myColor.a = 1f;
		GUI.color = myColor;
		GUI.Label(new Rect (100,200,100,100), "Simple Text");
	}
}