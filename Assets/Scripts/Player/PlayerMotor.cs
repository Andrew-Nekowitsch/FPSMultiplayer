using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
	private CharacterController controller;
	private Vector3 playerVelocity;
	private bool isGrounded;
	private bool isSprinting;
	private bool isCrouching;
	private bool crouchLerp;
	private float crouchTimer = 0f;
	public float speed = 5f;
	public float walkSpeed = 5f;
	public float sprintSpeed = 8f;
	public float crouchSpeed = 3f;
	public float gravity = -9.8f;
	public float jumpHeight = 3f;

	// Start is called before the first frame update
	void Start()
	{
		controller = GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void Update()
	{
		isGrounded = controller.isGrounded;
		HandleCrouch();
	}

	// receive the inputs for our InputManager.cs and apply them to our character controller.
	public void ProcessMove(Vector2 input)
	{
		Vector3 moveDirection = Vector3.zero;
		moveDirection.x = input.x;
		moveDirection.z = input.y;

		controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
		playerVelocity.y += gravity * Time.deltaTime;
		if (isGrounded && playerVelocity.y < 0)
		{
			playerVelocity.y = -2;
		}
		controller.Move(playerVelocity * Time.deltaTime);
	}

	public void Jump()
	{
		if (isGrounded)
		{
			playerVelocity.y = Mathf.Sqrt(jumpHeight * Mathf.Abs(gravity));
		}
	}

	public void Sprint()
	{
		if (isGrounded)
		{
			isSprinting = !isSprinting;
			if(isCrouching)
			{
				Crouch();
			}
			if (isSprinting)
			{
				speed = sprintSpeed;
			}
			else
			{
				speed = walkSpeed;
			}
		}
	}

	public void Crouch()
	{
		isCrouching = !isCrouching;
		isSprinting = false;
		crouchTimer = 0f;
		crouchLerp = true;
		if (isCrouching)
		{
			speed = crouchSpeed;
		}
		else
		{
			speed = walkSpeed;
		}
	}

	private void HandleCrouch()
	{
		if (crouchLerp)
		{
			crouchTimer += Time.deltaTime;
			float p = crouchTimer / 1;
			p *= p;

			if (isCrouching)
			{
				controller.height = Mathf.Lerp(controller.height, 1, p);
			}
			else
			{
				controller.height = Mathf.Lerp(controller.height, 2, p);
			}
			transform.localScale = new Vector3(1, (controller.height / 2), 1);

			if (p > 1)
			{
				crouchLerp = false;
				crouchTimer = 0;
			}
		}
	}
}

