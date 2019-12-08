using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
     public float speed;

     private Rigidbody rb;
	 
	 private bool hardMode;
	 private bool hasSpedUp;

     void Start()
     {
          rb = GetComponent<Rigidbody>();
          rb.velocity = transform.forward * speed;
     }
	 void Update()
	 {
		 GameObject gameControllerObject = GameObject.Find("Game Controller"); //yoink the hardBoink
		 GameController gameController = gameControllerObject.GetComponent<GameController>();
		 hardMode = gameController.hardMode;
		 
		 if (hardMode == true && hasSpedUp == false){
			 rb.velocity = rb.velocity*2;
			 hasSpedUp = true;
		 }
	 }
}