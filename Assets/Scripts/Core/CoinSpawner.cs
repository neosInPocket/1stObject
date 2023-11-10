using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
	[SerializeField] private Coin coinPrefab;
	[SerializeField] private Coin initialCoin;
	[SerializeField] private ColoredSide coloredSide;
	private Vector2 screenSize;
	private Coin lastCoin;
	
	private void Start()
	{
		screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
		lastCoin = initialCoin;
		lastCoin.Collected += OnPlayerCoinCollected;
	}
	
	private void OnPlayerCoinCollected()
	{
		lastCoin.Collected -= OnPlayerCoinCollected;
		var rndX = Random.Range(-screenSize.x + coloredSide.offset + coinPrefab.SpriteRenderer.size.x / 2, screenSize.x - coloredSide.offset - coinPrefab.SpriteRenderer.size.x / 2);
		var rndY = Random.Range(-screenSize.y + coloredSide.offset + coinPrefab.SpriteRenderer.size.y / 2, screenSize.y - coloredSide.offset - coloredSide.offsetTop - coinPrefab.SpriteRenderer.size.y /2);
		var newCoinPosition = new Vector2(rndX, rndY);
		
		lastCoin = Instantiate(coinPrefab, newCoinPosition, Quaternion.identity, transform);
		lastCoin.Collected += OnPlayerCoinCollected;
	}
	
}
