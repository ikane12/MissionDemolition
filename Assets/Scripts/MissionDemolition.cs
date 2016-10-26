using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MissionDemolition : MonoBehaviour {

	// Use this for initialization
	public enum GameMode{
		idle,
		playing,
		levelEnd
	}

	static public MissionDemolition S;
	static public int level;
	public GameObject[] castles;
	public Text tLevel;
	public Text tScore;
	public Vector3 castlePos;

	public bool _____________________________;


	public int levelMax;
	public int shotsTaken;
	public GameObject castle;
	public GameMode mode = GameMode.idle;
	public string showing = "Slingshot"; 
	void Start () {
		S = this;

		levelMax = castles.Length;
		startLevel ();
	}

	void startLevel(){
		if (castle != null)
			Destroy (castle);

		GameObject[] GOs = GameObject.FindGameObjectsWithTag ("Projectile");
		foreach (GameObject GO in GOs)
			Destroy (GO);

		castle = Instantiate (castles [level]) as GameObject;
		castle.transform.position = castlePos;
		shotsTaken = 0;

		SwitchView ("Both");
		ProjectLine.S.Clear ();
		Goal.goalMet = false;

		ShowText ();

		mode = GameMode.playing;
	}

	void ShowText(){
		tLevel.text = "Level: " + (level + 1) + " of " + levelMax;
		tScore.text = "Shos Taken: " + shotsTaken;
	}

	void Update(){
		ShowText ();

		if (mode == GameMode.playing && Goal.goalMet) {
			mode = GameMode.levelEnd;
			SwitchView ("Both");
			Invoke("NextLevel",2f);
		}
	}

	void NextLevel(){
		level++;
		if (level == levelMax)
			level = 0;

		startLevel ();
	}

	void OnGUI(){
		Rect buttonRect = new Rect ((Screen.width / 2) - 50, Screen.height - 30, 100, 24);
		switch (showing) {
		case "Slingshot":
			if (GUI.Button (buttonRect, "Show Castle"))
				SwitchView ("Castle");
			break;
		case "Castle":
			if (GUI.Button (buttonRect, "Show Both"))
				SwitchView ("Both");
			break;
		case "Both":
			if (GUI.Button (buttonRect, "Show Slingshot"))
				SwitchView ("Slingshot");
			break;
		}
	}

	static public void SwitchView(string eView){
		S.showing = eView;
		switch (S.showing) {
		case "Slingshot":
			FollowCamera.S.poi = null;
			break;
		case "Castle":
			FollowCamera.S.poi = S.castle;
			break;
		case "Both":
			FollowCamera.S.poi = GameObject.Find("ViewBoth");
			break;
		}
	}

	public static void shotFired(){
		S.shotsTaken++;
	}

}
