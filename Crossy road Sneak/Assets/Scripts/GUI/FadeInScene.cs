using UnityEngine;
using System.Collections;

public class FadeInScene : MonoBehaviour {

	
  //====================================================================================================
  // Member Variables
  //====================================================================================================
  public float StartAlpha = 1.0f; // The transparency value to start fading from
  public float EndAlpha = 0.0f; // The transparency value to end fading at
  public float FadingSpeed = 1.0f; // The speed of the effect
 
  private float Timer = 0.0f; // The time passed since fading was enabled
  private bool FadingOn = true; // Controls whether to fade or not
 
  //====================================================================================================
  // Custom Functions
  //====================================================================================================
 
  // Use this function to control fading using another script
  // i.e.
  // Fading fadingScript = fadingObject.GetComponent<Fading>();
  // fadingScript.Fade(true);
  public void Fade(bool fade)
  {
  FadingOn = fade;
  }
	
void Awake()
	{
	 Timer = 0.0f;
	 FadingOn = true;
	}
 
 
  //====================================================================================================
  // Unity Functions
  //====================================================================================================
  void LateUpdate()
  {
  // Don't do anything if we are not fading
  if (!FadingOn)
  return;
 
  // Increase the timer by the amount of time passed since the last frame
  Timer += Time.deltaTime;
 
 
  // Change the material's color, keeping RGB intact and interpolating alpha between
  // StartAlpha and EndAlpha
  GetComponent<Renderer>().material.color = new Color
  (
  GetComponent<Renderer>().material.color.r,
  GetComponent<Renderer>().material.color.g,
  GetComponent<Renderer>().material.color.b,
  Mathf.Lerp(StartAlpha, EndAlpha, Timer * FadingSpeed)
  );
 
  // Done fading
  if (GetComponent<Renderer>().material.color.a == EndAlpha)
  {
  // Stop fading and reset timer
  Timer = 0.0f;
  FadingOn = false;
 
  // Do whatever you need to do
  // i.e.: transition or destroy this object
  }
  }
 }


