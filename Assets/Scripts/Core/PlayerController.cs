using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private TrailRenderer trailRenderer;
	[SerializeField] private ParticleSystem deadParticleSystem;
	
	public Action CoinCollected;
	public Action<int> TakeDamageEvent;
	public Color CurrentColor
	{
		get => spriteRenderer.color;
		set
		{
			 spriteRenderer.color = value;
			 trailRenderer.startColor = new Color(value.r, value.g, value.b, 0.5f);;
			 trailRenderer.endColor = new Color(value.r, value.g, value.b, 0);
		}
	}
	
	private int currentLifes;
	
	public void Initialize()
	{
		currentLifes = 2;//SaveLoad.maxLifesSave;
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.TryGetComponent<Coin>(out Coin coin))
		{
			if (coin.Collect()) CoinCollected?.Invoke();	
		}
	}
	
	public bool TakeDamage()
	{
		if (currentLifes - 1 <= 0)
		{
			currentLifes = 0;
			TakeDamageEvent?.Invoke(currentLifes);
			Debug.Log("Player is dead");
			StartCoroutine(DeadEffect());
			return true;
		}
		else
		{
			currentLifes--;
			TakeDamageEvent?.Invoke(currentLifes);
			//StartCoroutine(TakeDamageEffect());
			return false;
		}
	}
	
	private IEnumerator DeadEffect()
	{
		var effect = Instantiate(deadParticleSystem, transform.position, Quaternion.identity, transform);
		yield return new WaitForSeconds(effect.main.duration);
		Destroy(effect.gameObject);
	}
	
	private IEnumerator TakeDamageEffect()
	{
		for (int i = 0; i < 8; i++)
		{
			spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.3f);
			yield return new WaitForSeconds(0.3f);
			spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
			yield return new WaitForSeconds(0.3f);
		}
	}
}
