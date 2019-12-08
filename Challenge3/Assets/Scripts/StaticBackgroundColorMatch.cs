using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticBackgroundColorMatch : MonoBehaviour
{
	
	private int score;
	private bool hasSpedUp;
	
	
	Renderer rend;
	
    // Start is called before the first frame update
    void Start()
    {        
		hasSpedUp = false;
		rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update() // This Script is BackgroundScroller without Scrolling the Background
    {	
		GameObject gameControllerObject = GameObject.Find("Game Controller"); //find the Game Controller and yoink the scoink
		GameController gameController = gameControllerObject.GetComponent<GameController>();
		score = gameController.score;
		
		if (score >= 500 & hasSpedUp == false){ // scroll speed up if win
			hasSpedUp = true;
		}
		
		if (hasSpedUp == true){//change colors back and forth if win, fun times!
			rend.material.color = Color.Lerp(Color.green, Color.magenta, Mathf.PingPong(Time.time, 1));
		}
    }
}
