using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Coin : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private ParticleSystem collectExplosion;
	private bool dead = false;
	
	public bool Collect()
	{
		if (dead) return false;
		
		dead = true;
		StartCoroutine(CollectRoutine());	
		return true;
	}
	
	private IEnumerator CollectRoutine()
	{
		spriteRenderer.color = new Color(0, 0, 0, 1);
		var explosion = Instantiate(collectExplosion, transform.position, Quaternion.identity, transform);
		
		yield return new WaitForSeconds(explosion.main.duration);
		
		Destroy(gameObject);
	}
}
