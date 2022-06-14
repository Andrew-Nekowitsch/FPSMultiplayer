using CMF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
	private PlayerInput playerInput;
	public PlayerInput.OnFootActions onFoot;

	[SerializeField]
	private AdvancedWalkerController advancedWalkerController;
	[SerializeField]
	private CameraController cameraController;
	[SerializeField]
	private PlayerAnimator playerAnimator;

	// Start is called before the first frame update
	void Awake()
	{
		playerInput = new PlayerInput();
		if (!cameraController)
			cameraController = GetComponent<CameraController>();
		if (!advancedWalkerController)
			advancedWalkerController = GetComponent<AdvancedWalkerController>();
		if (!playerAnimator)
			playerAnimator = GetComponent<PlayerAnimator>();

		onFoot = playerInput.OnFoot;
		//onFoot.Jump.performed += ctx => playerMotor.Jump();
		//onFoot.Sprint.performed += ctx => playerMotor.Run();
		//onFoot.Crouch.performed += ctx => playerMotor.Crouch();
		onFoot.FreeLook.performed += ctx => cameraController.FreeLook(ctx.ReadValueAsButton());
		onFoot.ADS.performed += ctx => playerAnimator.SetAiming(ctx.ReadValueAsButton());
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		advancedWalkerController.CalculateMovementDirection(onFoot.Movement.ReadValue<Vector2>());
	}
	private void LateUpdate()
	{
		cameraController.HandleCameraRotation(onFoot.Look.ReadValue<Vector2>());
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
