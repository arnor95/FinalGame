using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

// [RequireComponent(typeof(CharacterController))]
public class player2 : MonoBehaviour {

	public int playerId = 1; // The Rewired player id of this character
	public float bulletSpeed = 15.0f;
	public GameObject bulletPrefab;

	private Player player; // The Rewired Player
	private CharacterController cc;
    private Animator anim;
	private Vector3 moveVector;
	private bool fire;

	public float rotateSpeed;
	public float moveSpeed; 
	public float strafeSpeed;

	private Rigidbody myRigidBody;
	private Vector3 moveInput;
	private Vector3 moveVelocity;
	private Vector3 camForward;
	Transform cam;

	float forwardAmount;
	float turnAmount;

	void Awake() {
		// Get the Rewired Player object for this player and keep it for 
		// the duration of the character's lifetime
		player = ReInput.players.GetPlayer(playerId);

		// Get the character controller
		// cc = GetComponent<CharacterController>();

	    anim = GetComponent<Animator>();
		myRigidBody = GetComponent<Rigidbody>();
	}

	void Start () {

		cam = Camera.main.transform;

		rotateSpeed = 150;
		moveSpeed = 3;
		strafeSpeed = 3;
	}
	
	// Update is called once per frame
	void Update () {
		GetInput();
		// ProcessInput();

		// var rotate = player.GetAxis("Rotate Player") * Time.deltaTime * rotateSpeed;
		// var move = player.GetAxis("Move Horizontal") * Time.deltaTime * moveSpeed;
		// var strafe = player.GetAxis("Strafe") * Time.deltaTime * strafeSpeed;

		// transform.Rotate(0, rotate, 0);
		// transform.Translate(strafe, 0, move);

		// moveInput = new Vector3(player.GetAxis("MHorizontal"), 0f, player.GetAxis("MVertical"));
		// moveVelocity = moveInput * moveSpeed;

		Vector3 playerDirection = Vector3.right * player.GetAxisRaw("RHorizontal") + Vector3.forward * player.GetAxisRaw("RVertical");
		if(playerDirection.sqrMagnitude > 0.3f)
		{
			transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
		}

	    // float animMove = moveInput.x * 20;
	    // float animStrafe = moveInput.z * 20;

	    // anim.SetFloat("Forward", animMove);
	    // anim.SetFloat("Turn", animStrafe);

		if(fire)
		{
			Debug.Log("fire!");
		}
	}

	private void FixedUpdate()
	{
		myRigidBody.velocity = moveVelocity;

		float horizontal = player.GetAxisRaw("MHorizontal");
		float vertical = player.GetAxisRaw("MVertical");

		if(cam != null)
		{
			camForward = Vector3.Scale(cam.up, new Vector3(1,0,1)).normalized;
			moveVelocity = vertical * camForward + horizontal * cam.right;
		}
		else
		{
			moveVelocity = vertical * Vector3.forward + horizontal * Vector3.right;
		}

		if(moveInput.magnitude > 1)
		{
			moveInput.Normalize();
		}
		
		Move(moveVelocity);

		Vector3 movement;
		// if(turnAmount > 0.3 || turnAmount < -0.3)
		// {
		// 	movement = new Vector3(horizontal / 2, 0, vertical);	
		// }
		// else if(forwardAmount < -0.3)
		// {
		// 	movement = new Vector3(horizontal, 0, vertical / 2);
		// }
//		else
//		{
			bool backwards = false;

			

			movement = new Vector3(horizontal, 0, vertical);
			float angle = Vector3.Angle(transform.forward, movement);
			
			if(angle > 85)
			{
				backwards = true;
			}

			if(backwards)
			{
				movement = movement * 0.1f;
			}
//		}

		

		myRigidBody.AddForce(movement * moveSpeed / Time.deltaTime);
		
	}

	private void Move(Vector3 moveVelocity)
	{
		if(moveVelocity.magnitude > 1)
		{
			moveVelocity.Normalize();
		}

		this.moveInput = moveVelocity;

		ConvertMoveInput();
		UpdateAnimator();
	}

	private void ConvertMoveInput()
	{
		Vector3 localMove = transform.InverseTransformDirection(moveInput);
		turnAmount = localMove.x;

		forwardAmount = localMove.z;
	}

	private void UpdateAnimator()
	{
		anim.SetFloat("Forward", forwardAmount, 0.1f, Time.deltaTime);
		anim.SetFloat("Turn", turnAmount, 0.1f, Time.deltaTime);
	}

	private void GetInput() 
	{
		// Get the input from the Rewired Player. 
		// All controllers that the Player owns will contribute, so it doesn't matter
		// whether the input is coming from a joystick, the keyboard, mouse, or a custom 
		// controller.

		// smoveVector.x = player.GetAxis("Move Horizontal"); // get input by name or action id
		// moveVector.y = player.GetAxis("Rotate Player");
		fire = player.GetButtonDown("Fire");
	}

	private void ProcessInput() 
	{
		// Process movement
		if(moveVector.x != 0.0f || moveVector.y != 0.0f) {
			cc.Move(moveVector * moveSpeed * Time.deltaTime);
		}

		// Process fire
		if(fire) {
//			GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position + transform.right, transform.rotation);
//			bullet.rigidbody.AddForce(transform.right * bulletSpeed, ForceMode.VelocityChange);
			
		}
	}
}
		// if(turnAmount > 0.3 || turnAmount < -0.3)
		// {
		// 	movement = new Vector3(horizontal / 2, 0, vertical);	
		// }
		// else if(forwardAmount < -0.3)
		// {
		// 	movement = new Vector3(horizontal, 0, vertical / 2);
		// }
		// else
		// {
		// 	movement = new Vector3(horizontal, 0, vertical);
		// }
