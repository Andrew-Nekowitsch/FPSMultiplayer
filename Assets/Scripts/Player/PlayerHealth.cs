using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
	private float health;
	private float lerpTimer;

	[Header("Health Bar")]
	public float maxHealth = 100f;
	public float chipSpeed = 2f;
	public Image frontHealthBar;
	public Image backHealthBar;

	[Header("Damage Overlay")]
	public Image damageOverlay;
	public Image damageOverlayStrong;
	public float duration;
	public float fadeSpeed;
	private float durationTimer;
	private bool rising = true;


	void Start()
	{
		health = maxHealth;
		damageOverlay.color = new Color(damageOverlay.color.r, damageOverlay.color.g, damageOverlay.color.b, 0);
		damageOverlayStrong.color = new Color(damageOverlayStrong.color.r, damageOverlayStrong.color.g, damageOverlayStrong.color.b, 0);
	}

	void Update()
	{
		health = Mathf.Clamp(health, 0, maxHealth);
		UpdateHealthUI();
		HandleDamageOverlay();
	}

	private void HandleDamageOverlay()
	{
		if (damageOverlay.color.a > 0)
		{
			if (health <= 30)
			{
				PulseOverlay();
				return;
			}
			// reset strong overlay because health is > 30
			else if (damageOverlayStrong.color.a > 0)
			{
				damageOverlayStrong.color = new Color(damageOverlayStrong.color.r, damageOverlayStrong.color.g, damageOverlayStrong.color.b, 0);
			}

			durationTimer += Time.deltaTime;
			if (durationTimer >= duration)
			{
				ImageFadeOut(damageOverlay);
			}
		}
	}

	public void UpdateHealthUI()
	{
		float fillFront = frontHealthBar.fillAmount;
		float fillBack = backHealthBar.fillAmount;
		float healthPercent = health / maxHealth;

		if (fillBack > healthPercent)
		{
			backHealthBar.color = Color.red;
			frontHealthBar.fillAmount = healthPercent;
			float percentComplete = CalculatePercentComplete();
			backHealthBar.fillAmount = Mathf.Lerp(fillBack, healthPercent, percentComplete);
		}
		else if (fillFront < healthPercent)
		{
			backHealthBar.color = Color.green;
			backHealthBar.fillAmount = healthPercent;
			float percentComplete = CalculatePercentComplete();
			frontHealthBar.fillAmount = Mathf.Lerp(fillFront, healthPercent, percentComplete);
		}

		float CalculatePercentComplete()
		{
			lerpTimer += Time.deltaTime;
			float percentComplete = lerpTimer / chipSpeed;
			percentComplete = percentComplete * percentComplete;
			return percentComplete;
		}
	}

	public void TakeDamage(float amount)
	{
		health -= amount;
		lerpTimer = 0f;

		durationTimer = 0f;
		float tempAlpha = Mathf.Clamp(.9f - (health / maxHealth), .1f, .8f);
		damageOverlay.color = new Color(damageOverlay.color.r, damageOverlay.color.g, damageOverlay.color.b, tempAlpha);
	}

	public void RestoreHealth(float amount)
	{
		health += amount;
		lerpTimer = 0f;
	}

	private void PulseOverlay()
	{
		if (rising)
		{
			ImageFadeIn(damageOverlay);
			ImageFadeOut(damageOverlayStrong);
		}
		else
		{
			ImageFadeIn(damageOverlayStrong);
			ImageFadeOut(damageOverlay);
		}
		if (damageOverlay.color.a <= .15f)
		{
			rising = true;
		}
		else if (damageOverlay.color.a >= .75f)
		{
			rising = false;
		}
	}

	private float ClampOverlay(float alpha)
	{
		return Mathf.Clamp(alpha, 0, .8f);
	}

	private void ImageFadeOut(Image i)
	{
		float tempAlpha = i.color.a;
		tempAlpha -= Time.deltaTime * fadeSpeed / 2;
		tempAlpha = ClampOverlay(tempAlpha);
		i.color = new Color(i.color.r, i.color.g, i.color.b, tempAlpha);
	}

	private void ImageFadeIn(Image i)
	{
		float tempAlpha = i.color.a;
		tempAlpha += Time.deltaTime * fadeSpeed / 2;
		tempAlpha = ClampOverlay(tempAlpha);
		i.color = new Color(i.color.r, i.color.g, i.color.b, tempAlpha);
	}
}
