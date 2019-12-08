using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Boundary
{
     public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
     public float speed;
     public float tilt;
     public Boundary boundary;
	 
	 public GameObject shot;
	 public Transform shotSpawn;
	 public Transform shotSpawn2;
	 public Transform shotSpawn3;
	 public float fireRate;
	 
	 public bool hasPickup;
	 
	 public Text chainGunText;
	 
	 public AudioSource audioSource;
	 public AudioClip weaponFire;
	 
	 private float nextFire;
	 private int chainGunCount;

     private Rigidbody rb;

     private void Start()
     {
          rb = GetComponent<Rigidbody>();
		  audioSource.clip = weaponFire;
		  hasPickup = false;
		  chainGunText.text = "Press H for Hard Mode! Yellow Box is Chain Gun!";
     }
	 
	 void Update()
	 {
		 if (Input.GetButton("Fire1") && Time.time > nextFire && hasPickup == false) // make pickup do fan style attack
		 {			 
			 nextFire = Time.time + fireRate; //do an if no pickup statement here
			 Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			 audioSource.Play();
		 }
		 
		 if (Input.GetButton("Fire1") && Time.time > nextFire && hasPickup == true) // make pickup do chain gun attack
		 {
			 if (fireRate >= .105f){
				 fireRate -= .005f;
			}
			
			 chainGunCount = chainGunCount + 1;
			 
			 if (chainGunCount >= 10 || chainGunCount == -1){//Make Chain Gun Text Disappear
				chainGunText.text = "";
			}
			 
			 if (chainGunCount > 75 && chainGunCount < 100){
				chainGunText.text = "Chain Gun Almost Used Up!";
			 }
			 
			 if (chainGunCount >= 100){//chain gun burns out
				 hasPickup = false;
				 fireRate = .25f;
			 }
			 nextFire = Time.time + fireRate; //do an if no pickup statement here
			 Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			 //Instantiate(shot, shotSpawn2.position, shotSpawn2.rotation); hitbox issues
			 //Instantiate(shot, shotSpawn3.position, shotSpawn3.rotation);
			 audioSource.Play();
		 }
	 }

     void FixedUpdate()
     {
          float moveHorizontal = Input.GetAxis("Horizontal");
          float moveVertical = Input.GetAxis("Vertical");

          Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
          rb.velocity = movement * speed;

          rb.position = new Vector3
          (
               Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
               0.0f,
               Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
          );

          rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
     }
	 
	 void OnTriggerEnter(Collider other){
		if (other.tag == "Boundary" || other.tag == "Enemy"){
			return;
		}
				
		if (other.tag == "Pickup"){
			hasPickup = true;
			chainGunText.text = "Chain Gun Power!";			
		}
		
	}
	 
}