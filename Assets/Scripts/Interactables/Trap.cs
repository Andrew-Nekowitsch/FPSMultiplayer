using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : Interactable
{
	[SerializeField]
	private GameObject trap;
	private bool triggered;
	public PlayerHealth playerHealth;
	public float damageAmount = 10f;

	protected override void Interact()
	{
		TriggerTrap();
		trap.GetComponent<Animator>().SetBool("triggered", triggered);
	}

	public void TriggerTrap()
	{
		if (playerHealth == null) return;

		triggered = !triggered;
		if (triggered == true)
		{
			playerHealth.TakeDamage(damageAmount);
		}
	}
}
