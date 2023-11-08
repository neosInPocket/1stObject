using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public Action coinCollected;

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.TryGetComponent<Coin>(out Coin coin))
		{
			if (coin.Collect()) coinCollected?.Invoke();	
		}
	}
}
