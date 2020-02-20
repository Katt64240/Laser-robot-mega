using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour {

	public float walkSpeed = 4.0f;
	public float mouseSpeed = 3.0f;
	public float verticalRange = 90f;
	public float gravity = -60f;
	public float jumpSpeed = 10.0f;
	public GameObject cameraCenter;


	float verticalRotation;
	Vector3 moveDirection;
	float forwardSpeed;
	float sideSpeed;
	float moveSpeed;
	float ySpeed;

	CharacterController cc;

	// Use this for initialization
	void Start () {
		Screen.lockCursor = true;
		cc = GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void Update () {

		ySpeed += gravity * Time.deltaTime;
		if (cc.isGrounded) {
			ySpeed = -1;
			if (Input.GetKey (KeyCode.Space)) {
				ySpeed = jumpSpeed;
			}
		}

		//Rotaion
		transform.Rotate(Vector3.up * Input.GetAxis ("Mouse X") * mouseSpeed);
		verticalRotation += Input.GetAxis("Mouse Y") * mouseSpeed;
		verticalRotation = Mathf.Clamp(verticalRotation, -verticalRange, verticalRange);
		cameraCenter.transform.localEulerAngles = Vector3.left * verticalRotation;


		moveSpeed = walkSpeed;
		forwardSpeed = Input.GetAxis ("Vertical") * moveSpeed;
		sideSpeed = Input.GetAxis ("Horizontal") * moveSpeed;

		moveDirection = new Vector3 (sideSpeed, ySpeed, forwardSpeed);
		moveDirection = transform.rotation * moveDirection;

        cc.Move(moveDirection * Time.deltaTime);
	}
}

