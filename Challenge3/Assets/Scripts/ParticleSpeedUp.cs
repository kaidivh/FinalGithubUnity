using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpeedUp : MonoBehaviour
{
	
	private ParticleSystem ps;
	private int score;
	
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
		var main = ps.main; // find current sim speed
		
		GameObject gameControllerObject = GameObject.Find("Game Controller"); //yoink the scoink
		GameController gameController = gameControllerObject.GetComponent<GameController>();
		score = gameController.score;
		
		if (score >= 500){ //you have boost power
			main.simulationSpeed = 10;
		}
    }
}
