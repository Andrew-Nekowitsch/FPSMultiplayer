using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
	public Animator[] movementAnimators;
	public Animator[] aimingAnimators;
	private bool isRunning = false;
	private bool isCrouching = false;
	private bool isMoving = false;
	private bool isAiming = false;

	public void SetRunning(bool running)
	{
		isRunning = running;
		SetMovementBool("isRunning", isRunning);
	}
	public void SetMoving(bool moving)
	{
		isMoving = moving;
		SetMovementBool("isMoving", isMoving);
	}
	public void SetCrouching(bool moving)
	{
		isCrouching = moving;
		SetMovementBool("isMoving", isCrouching);
	}
	public void SetAiming(bool aiming)
	{
		isAiming = aiming;
		SetAimingBool("isAiming", isAiming);
	}

	void SetAimingBool(string name, bool state)
	{
		foreach (var animator in aimingAnimators)
		{
			animator.SetBool(name, state);
		}
	}
	void SetMovementBool(string name, bool state)
	{
		foreach (var animator in movementAnimators)
		{
			animator.SetBool(name, state);
		}
	}
}
