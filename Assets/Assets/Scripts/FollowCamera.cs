using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

	// Distance in the x axis the player can move before
	// the camera follows
	public float xMargin = 1.5f;

	// distance in the y axis the player can move before
	// the camera folow
	public float yMargin = 1.5f;

	//How smoothly the camera catches up with its target
	// movement in x axis
	public float xSmooth = 1.5f;

	//How smoothly the camera catches up with its target
	// movement in y axis
	public float ySmooth = 1.5f;

	//tge maximum x and y coordinates the camera have
	private Vector2 maxXAndY;

	//the minimum x and y coordinates the camera can have;
	private Vector2 minXAndY;

	//reference to the player Transform
	public Transform player;


	//awake function
	void Awake(){

		//setting up the reference 
		player = GameObject.Find("Player").transform;
		if(player == null){
			Debug.LogError("Player object not found");
		}

		// get the bounds for the background texture - world size
		var backgroundBounds = GameObject.Find("background").renderer.bounds;

		// get the viewable bounds of the camera in world coordinates
		var camTopLeft = camera.ViewportToWorldPoint(new Vector3(0,0,0));
		var camBottomRight = camera.ViewportToWorldPoint(new Vector3(1,1,0));

		//finally update the min and max value
		minXAndY.x = backgroundBounds.min.x - camTopLeft.x;
		maxXAndY.x = backgroundBounds.max.x - camBottomRight.x;
	}

	// check margin x
	bool CheckXMargin(){
		//return true if the distance between the camera and the
		//player in the x axis is grater than the x margin.

		return Mathf.Abs(transform.position.x - player.position.x) > xMargin;
	}

	// check margin y
	bool CheckYMargin(){
		//return true if the distance between the camera and the
		//player in the y axis is grater than the y margin.
		
		return Mathf.Abs(transform.position.y - player.position.y) > yMargin;
	}

	void FixedUpdate(){
		//by default the target x and y coordinates of the camera
		// are it's current x and y coordinates
		float targetX = transform.position.x;
		float targetY = transform.position.y;

		//if the player has moved beyond the x margin...
		if (CheckXMargin ()){
			// the taget x coordinate should be a lerp betwen 
			// the camera's current x
			// and the player's current x position
			targetX = Mathf.Lerp (transform.position.x, player.position.x, xSmooth * Time.fixedDeltaTime);


		}
		// if the player has moved beyond y margin
		if( CheckYMargin ()){
			targetY = Mathf.Lerp (transform.position.y, player.position.y, ySmooth * Time.fixedDeltaTime);
		}

		// the target x and y should not be larger than the maximum or smaller that the minumum
		targetX =  Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
		targetY =  Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);

		//set the camera's position to the target position with the same z component
		transform.position = new Vector3(targetX, targetY, transform.position.z);
	}



}
