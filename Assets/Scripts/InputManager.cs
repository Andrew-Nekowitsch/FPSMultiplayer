using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
	private PlayerInput playerInput;
	public PlayerInput.OnFootActions onFoot;

	private PlayerMotor playerMotor;
	private PlayerLook playerLook;

	// Start is called before the first frame update
	void Awake()
	{
		playerInput = new PlayerInput();
		playerLook = GetComponent<PlayerLook>();
		playerMotor = GetComponent<PlayerMotor>();
		onFoot = playerInput.OnFoot;
		onFoot.Jump.performed += ctx => playerMotor.Jump();
		onFoot.Sprint.performed += ctx => playerMotor.Sprint();
		onFoot.Crouch.performed += ctx => playerMotor.Crouch();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		playerMotor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
	}
	private void LateUpdate()
	{
		playerLook.ProcessLook(onFoot.Look.ReadValue<Vector2>());
	}

	private void OnEnable()
	{
		onFoot.Enable();
	}
	private void OnDisable()
	{
		onFoot.Disable();
	}
}
