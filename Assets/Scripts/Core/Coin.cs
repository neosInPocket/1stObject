using System;
using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private ParticleSystem collectExplosion;
	public SpriteRenderer SpriteRenderer => spriteRenderer;
	public Action Collected;
	private bool dead = false;
	
	public bool Collect()
	{
		if (dead) return false;
		
		dead = true;
		Collected?.Invoke();
		StartCoroutine(CollectRoutine());	
		return true;
	}
	
	private IEnumerator CollectRoutine()
	{
		spriteRenderer.color = new Color(0, 0, 0, 0);
		var explosion = Instantiate(collectExplosion, transform.position, Quaternion.identity, transform);
		
		yield return new WaitForSeconds(explosion.main.duration);
		
		Destroy(gameObject);
	}
}
