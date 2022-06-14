using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
	private CharacterController controller;
	private PlayerAnimator playerAnimator;
	private Vector3 playerVelocity;
	private bool isGrounded;
	private bool isRunning;
	private bool isCrouching;
	private bool crouchLerp;
	private float crouchTimer = 0f;
	public float speed = 5f;
	public float walkSpeed = 5f;
	public float runSpeed = 8f;
	public float crouchSpeed = 3f;
	public float gravity = -9.8f;
	public float jumpHeight = 3f;

	// Start is called before the first frame update
	void Start()
	{
		controller = GetComponent<CharacterController>();
		playerAnimator = GetComponent<PlayerAnimator>();
	}

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

		if (input != Vector2.zero)
			playerAnimator.SetMoving(true);
		else
		{
			playerAnimator.SetRunning(false);
			playerAnimator.SetMoving(false);
		}

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

	public void Run()
	{
		if (isGrounded)
		{
			if (isCrouching)
				Crouch();

			isRunning = !isRunning;
			playerAnimator.SetRunning(isRunning);
			speed = isRunning ? runSpeed : walkSpeed;
		}
	}

	public void Crouch()
	{
		isCrouching = !isCrouching;
		playerAnimator.SetCrouching(isCrouching);
		crouchTimer = 0f;
		crouchLerp = true;
		speed = isCrouching ? crouchSpeed : walkSpeed;
		if (isRunning)
		{
			isRunning = false;
			playerAnimator.SetRunning(isRunning);
		}
	}

	private void HandleCrouch()
	{
		if (crouchLerp)
		{
			crouchTimer += Time.deltaTime;
			float p = crouchTimer / 1;
			p *= p;

			controller.height =
				isCrouching ? Mathf.Lerp(controller.height, 1, p) : Mathf.Lerp(controller.height, 2, p);
			//transform.localScale = new Vector3(1, (controller.height / 2), 1);

			if (p > 1)
			{
				crouchLerp = false;
				crouchTimer = 0;
			}
		}
	}
}

