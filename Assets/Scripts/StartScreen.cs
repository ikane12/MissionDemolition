using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour {

	int level = 1;
	public void StartGame()
	{
		Application.LoadLevel ("Scene_0");
		MissionDemolition.level = level - 1;

	}

	public void ChangeLevel(int level)
	{
		this.level = level;
	}
	public void ExitGame(){
		Application.Quit ();
	}

}
