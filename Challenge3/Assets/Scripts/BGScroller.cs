using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
	
	public float scrollSpeed;
	public float tileSizeZ;
	
	private int score;
	private bool hasSpedUp;
	private Vector3 startPosition;
	
	Renderer rend;
	
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
		hasSpedUp = false;
		rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float newPosition = Mathf.Repeat (Time.time * scrollSpeed, tileSizeZ);
		transform.position = startPosition + Vector3.forward * newPosition;
		
		GameObject gameControllerObject = GameObject.Find("Game Controller"); //find the Game Controller and yoink the scoink
		GameController gameController = gameControllerObject.GetComponent<GameController>();
		score = gameController.score;
		if (score >= 500 & hasSpedUp == false){ // scroll speed up if win
			scrollSpeed = scrollSpeed * 50;
			hasSpedUp = true;
		}
		
		if (hasSpedUp == true){//change colors back and forth if win, fun times!
			rend.material.color = Color.Lerp(Color.green, Color.magenta, Mathf.PingPong(Time.time, 1));
		}
    }
}
