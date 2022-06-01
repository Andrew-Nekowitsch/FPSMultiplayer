using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
	public Animator animator;
	private bool isRunning = false;
	private bool isCrouching = false;
	private bool isMoving = false;

	public void ToggleRunning() {
		isRunning = !isRunning;
		animator.SetBool("isRunning", isRunning);
	}
	public void SetRunning(bool running)
	{
		isRunning = running;
		animator.SetBool("isRunning", isRunning);
	}

	public void ToggleCrouching()
	{
		isCrouching = !isCrouching;
		animator.SetBool("isCrouching", isCrouching);
	}

	public void ToggleMoving()
	{
		isMoving = !isMoving;
		animator.SetBool("isMoving", isMoving);
	}
	public void SetMoving(bool moving)
	{
		isMoving = moving;
		animator.SetBool("isMoving", isMoving);
	}
}
