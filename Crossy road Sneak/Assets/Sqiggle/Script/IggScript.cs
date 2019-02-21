using UnityEngine;
using System.Collections;

public class IggScript : MonoBehaviour,ParleyEnviromentInfo {
	
	private bool broadcastjumps=false;
	public float waterlevel=.7f;
	private Vector3 restartLocation;
	public string toonName="Igg";
	private float gold=0f;
	private int mushrooms=0;
	private bool showgold=false;
	private bool showmushrooms=false;
	public GUISkin iggSkin;
	private GameObject fall;
	private float respawntimer = -1;
	private bool splashCreated = false;
	
	void Start () {
		restartLocation=transform.localPosition;
		// Register this instance and the ParleyEnviromentInfo provider
		Parley.GetInstance().SetParleyEnviromentInfo(this);
		//GameObject.Find("camera").SendMessage("SetParleyEnviromentInfo", this);
	}
	
	// Update is called once per frame
	void Update () {
		if (respawntimer>0){
			if (respawntimer<Time.time){
				Respawn();
			}
		}else{
			if (transform.localPosition.y<=waterlevel){
				Die();
			}
		}
	}
	
	void Die(){
		DialogStarted(null);
		// Die and respawn
		if(splashCreated == false){
			splashCreated = true;
			fall = Instantiate(Resources.Load("IggFallInWater"),transform.position,Quaternion.identity) as GameObject;
		}
		respawntimer = Time.time+1f;
	}
	
	void Respawn(){
		DialogEnded(null);
		Destroy(fall);
		splashCreated = false;
		respawntimer = -1;
		transform.localPosition=restartLocation;
	}
	
	/** 
	 * DidJump is fired from ThirdPersonControler when the player pressed Jump
	 * 
	 * We want to fire an event "Jump" onto the quests each time the player jumps, but only when that quest is enabled for performance reasons.
	 * 
	 */
	void DidJump(){
		if (broadcastjumps){
			// The code below fires a Quest Event "Jump" into Parley
			Parley.GetInstance().TriggerQuestEvent("Jump");
		}
	}

	/**
	 * Player eveents are text string that are called into the player object as a SendMessage at certain times within the processing
	 * of the dialogs and quests.
	 * 
	 * We defined tow such Player events 
	 * 
	 * BroadcastJumps was defined in the Dialog where Igg is told more about jumping. This triggers the Quest option to count
	 * jumps and as such we want to start braodcasting jumps into the quests. The queszt would still work if we always send
	 * Jump into the system but its messy to send messages we know will be ignored if we can avoid them.
	 * 
	 * StopBroadcastingJumps is sent when the Try Jumping quest objective is completed. Shutting off teh flow of Jump message to the quest engine.
	 * 
	 * NOTE: Player events associated with Quest Objectives will be sent to the player each time the Quest Objective is effected. So if the 
	 * Objective must be executed 5 times to complete teh Player event will call each time.
	 * 
	 */
	void BroadcastJumps(){
		broadcastjumps=true;
	}
	
	void StopBroadcastingJumps(){
		broadcastjumps=false;
	}
	
	/**
	 * The 4 methods defined below are all called to notify the player that a dialog has been entered into or exited.
	 * 
	 * This is managed by the GUI script so your own implementations might need to achieve a similar thing. Essentiall in
	 * Iggs grand adventure we are freeze Igg untill the player leaves the Dialog or Quests gui's
	 * 
	 */
	
	// List of player events
	public void DialogStarted(Dialog dialog){
		MonoBehaviour tpc=(MonoBehaviour)GetComponent("ThirdPersonController");
		tpc.enabled=false;
	}

	public void DialogEnded(Dialog dialog){
		MonoBehaviour tpc=(MonoBehaviour)GetComponent("ThirdPersonController");
		tpc.enabled=true;
	}
	
	// List of player events
	public void QuestsStarted(){
		MonoBehaviour tpc=(MonoBehaviour)GetComponent("ThirdPersonController");
		tpc.enabled=false;
	}

	public void QuestsEnded(){
		MonoBehaviour tpc=(MonoBehaviour)GetComponent("ThirdPersonController");
		tpc.enabled=true;
	}
	
	public void GetGold(int howmuch){
		showgold=true;
		gold+=howmuch;
	}
	
	public void GetMushrooms(int howmuch){
		showmushrooms=true;
		mushrooms+=howmuch;
	}
	/**
	 * Below are some of the other funcitons that will be called by the Parley engine on events.
	 * 
	 * 
	 */
	
	
	// ExperianceParamater Quest and Quest events (per event effect) can call add XP this will only be called if the XP is not 0
	public void AddXp(int xp){
	}
	
	// ExtraParamater Quest and Quest events (per event effect) can call add EP this will only be called if the EP is not 0
	public void AddEp(int ep){
	}
	
	// When a quest is completed this will be called. This allows for some notifications to the player
	public void FinishedQuest(Quest quest){
	}

	// When a quest is started this will be called. This allows for some notifications to the player
	public void StartedQuest(Quest quest){
	}
	
	public void ShowGold(){
		showgold=true;
	}
	
	public void SetGold(float gold){
		this.gold=gold;
	}
	
	public void ShowMushrooms(){
		showmushrooms=true;
	}
	
	public void SetMushrooms(float mushrooms){
		this.mushrooms=(int)mushrooms;
	}

	/** This method will return the environmental data. For now we are simply 
	 * returning the name, gold and mushrooms.
	 * 
	 * If a dialog string has <name> in it that string will be replaced with the 
	 * name as configured in the 
	 * Object in Unity. This could be used with far more versatility
	 */
	public object GetEnviromentInfo(string key){
		Debug.Log("Igg: EnviromentalInformation Requested <"+key+">");
		
		if (key.Equals("name")){
			return toonName;
		}else if (key.Equals("gold")){
			return gold;
		}else if (key.Equals("mushrooms")){
			return mushrooms;
		}
		return null;
	}
	
	public void SetEnviromentInfo(string key,object v){
		if (key.Equals("name")){
			toonName=(string)v;
		}else if (key.Equals("gold")){
			gold=(float)v;
		}else if (key.Equals("mushrooms")){
			mushrooms=(int)v;
		}
	}
	
	public void OnGUI(){
		if (iggSkin!=null){
			GUI.skin=iggSkin;
		}
		
		string label="";
		if (showgold){
			label=" Gold "+gold+ " ";
		}
		if (showmushrooms){
			label=label+" Wildflowers "+mushrooms+" " ;
		}
		
		GUI.Label(new Rect(0,0,Screen.width,40),label);
	}
}
