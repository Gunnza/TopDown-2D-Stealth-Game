using UnityEngine;
using System.Collections;

public class SwipeDetector : MonoBehaviour {
	
	// Values to set:
	public float comfortZone = 70.0f;
	public float minSwipeDist = 14.0f;
	public float maxSwipeTime = 0.5f;
	
	private float startTime;
	private Vector2 startPos;
	private bool couldBeSwipey;
	private bool couldBeSwipex;

	public static  bool upAvailable;
	public static bool downAvailable;
	public static bool rightAvailable;
	public static bool leftAvailable;
	
	Rigidbody2D thePlayer;
	public Vector2 velocity;

	GameObject thePlayerChild;
	Rigidbody2D thePlayerChildRigidbody;
	
	public enum SwipeDirection {
		None,
		Up,
		Down,
		Left,
		Right
	}
	
	public SwipeDirection lastSwipe = SwipeDetector.SwipeDirection.None;
	public float lastSwipeTime;

	void Start()
	{
		thePlayer = GetComponent<Rigidbody2D>();
		thePlayerChildRigidbody = GetComponent<Rigidbody2D>();
	}
	
	void Update()
	{
		if (Input.touchCount > 0)
		{
			Touch touch = Input.touches[0];
			
			switch (touch.phase)
			{
			case TouchPhase.Began:
				lastSwipe = SwipeDetector.SwipeDirection.None;
				lastSwipeTime = 0;
				couldBeSwipey = true;
				couldBeSwipex = true;
				startPos = touch.position;
				startTime = Time.time;
				break;
				
			case TouchPhase.Moved:
				if (Mathf.Abs(touch.position.x - startPos.x) > comfortZone)
				{
				//	Debug.Log("Not a swipe. Swipe strayed " + (int)Mathf.Abs(touch.position.x - startPos.x) +
				//	          "px which is " + (int)(Mathf.Abs(touch.position.x - startPos.x) - comfortZone) +
				//	          "px outside the comfort zone.");
					couldBeSwipey = false;
				}
				else if (Mathf.Abs(touch.position.y - startPos.y) > comfortZone)
				{
					//Debug.Log("Not a swipe. Swipe strayed " + (int)Mathf.Abs(touch.position.x - startPos.x) +
					 //         "px which is " + (int)(Mathf.Abs(touch.position.x - startPos.x) - comfortZone) +
					 //         "px outside the comfort zone.");
					couldBeSwipex = false;
				}
				break;
			case TouchPhase.Ended:
				if (couldBeSwipey)
				{
					float swipeTime = Time.time - startTime;
					float swipeDist = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;

					if ((swipeTime < maxSwipeTime) && (swipeDist > minSwipeDist))
					{
						// It's a swiiiiiiiiiiiipe!
						float swipeValue = Mathf.Sign(touch.position.y - startPos.y);
						
						// If the swipe direction is positive, it was an upward swipe.
						// If the swipe direction is negative, it was a downward swipe.
						if (swipeValue > 0)
						{
							lastSwipe = SwipeDetector.SwipeDirection.Up;
							//thePlayer.transform.position(.5f,0,0);
							thePlayer.MovePosition(thePlayer.position + velocity * Time.fixedDeltaTime);
							thePlayerChildRigidbody.MovePosition(thePlayer.position + velocity * Time.fixedDeltaTime);
							//gameObject.transform.Translate(-.5f,0,0);
						}
						else if (swipeValue < 0)
						{
							lastSwipe = SwipeDetector.SwipeDirection.Down;
							gameObject.transform.Translate(.5f,0,0);
						}
						
						// Set the time the last swipe occured, useful for other scripts to check:
						lastSwipeTime = Time.time;
						Debug.Log("Found a swipe!  Direction: " + lastSwipe);
					}
					else
					{
						Debug.Log ("Just Tapped in x");
						gameObject.transform.Translate(0,.5f,0);
					}
				}
				else if (couldBeSwipex)
				{
					float swipeTime = Time.time - startTime;
					float backSwipeDist = (new Vector3(touch.position.x,0, 0) - new Vector3(startPos.x,0, 0)).magnitude;
					
					if ((swipeTime < maxSwipeTime) && (backSwipeDist > minSwipeDist))
					{
						
						
						// It's a swiiiiiiiiiiiipe!
						float backSwipeValue = Mathf.Sign(touch.position.x - startPos.x);
						
						// If the swipe direction is positive, it was an upward swipe.
						// If the swipe direction is negative, it was a downward swipe.
						if (backSwipeValue > 0)
						{
							lastSwipe = SwipeDetector.SwipeDirection.Right;
							gameObject.transform.Translate(0,.5f,0);
						}
						else if (backSwipeValue < 0)
						{
							lastSwipe = SwipeDetector.SwipeDirection.Left;
							gameObject.transform.Translate(0,-.5f,0);
						}
						
						// Set the time the last swipe occured, useful for other scripts to check:
						lastSwipeTime = Time.time;
						Debug.Log("Found a swipe!  Direction: " + lastSwipe);
					}
					else
					{
						//Tap forward
						Debug.Log ("Just Tapped in x");
						gameObject.transform.Translate(0,.5f,0);
					}
				}
				break;
			}
		}
	}
}


/*
// It's a backkkkk swiiiiiiiiiiiipe!
float backSwipeValue = Mathf.Sign(touch.position.x - startPos.x);
float dontDiagonal = Mathf.Sign(touch.position.y - startPos.y);

if (dontDiagonal > 0 && dontDiagonal < 0)
{
	Debug.Log("cant swipe back cause you swiped up aswel ");
}
else
{
	if (backSwipeValue < 0)
	{
		//lastSwipe = SwipeDetector.SwipeDirection.Down;
		gameObject.transform.Translate(0,-.5f,0);
	}
}

*/



