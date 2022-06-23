using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
	[SerializeField]
	private float smooth;
	[SerializeField]
	private float swayMultiplier;

	public void Sway(Vector2 input)
	{
		var input2 = Vector2.ClampMagnitude(input, 10);
		float mouseX = input2.x * swayMultiplier;
		float mouseY = input2.y * swayMultiplier;

		Quaternion xRotation = Quaternion.AngleAxis(-mouseY, Vector3.right);
		Quaternion yRotation = Quaternion.AngleAxis(mouseX, Vector3.up);

		Quaternion targetRotation = xRotation * yRotation;

		transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
	}
}
