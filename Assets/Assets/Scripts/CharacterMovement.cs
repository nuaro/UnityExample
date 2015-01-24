using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
	//rigibody for the player character
	private Rigidbody2D playerRigibody2d;

	//variable to track how much movement is needed from input
	private float movePlayerVector;

	//for determining which way the player are facing 
	private bool facingRight;

	//reference to the animator component
	private Animator anim;

	//reference to spriteplayer gameobject
	private GameObject playerSprite;

	//speed modifier for player movement
	public float speed = 4.0f;

	void Awake (){
		playerRigibody2d = (Rigidbody2D)GetComponent(typeof(Rigidbody2D));

		playerSprite = transform.Find("PlayerSprite").gameObject;

		anim = (Animator)playerSprite.GetComponent(typeof(Animator));

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//get the orizontal input
		movePlayerVector = Input.GetAxis("Horizontal");
		anim.SetFloat("speed",Mathf.Abs(movePlayerVector));
		playerRigibody2d.velocity = new Vector2(movePlayerVector * speed, playerRigibody2d.velocity.y);

		if(movePlayerVector > 0 && !facingRight)
		{
			Flip();
		}
		else if(movePlayerVector < 0 && facingRight){
			Flip();
		}
	
	}

	void Flip(){
		//switch the way the player is labeled as facing
		facingRight = !facingRight;
		// multiply the player local scale x by -1
		Vector3 theScale = playerSprite.transform.localScale;
		theScale.x *= -1;
		playerSprite.transform.localScale = theScale;
	}
}
