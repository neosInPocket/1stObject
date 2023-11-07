using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class ColoredSide : MonoBehaviour
{
	private enum SidePosition
	{
		Left,
		Right,
		Up,
		Down
	}
	
	[SerializeField] private SidePosition sidePosition;
	[SerializeField] private int tileCount;
	[SerializeField] private ColoredTile coloredTilePrefab;
	
	private void Start()
	{
		var screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
		
		float tileWidth = 0;
		float tileHeight = 0;
		
		if (sidePosition == SidePosition.Left || sidePosition == SidePosition.Right)
		{
			tileHeight = 2 * screenSize.y / tileCount;
			tileWidth = 2 * screenSize.x / coloredTilePrefab.XSizeMultiplier;
		}
		
		if (sidePosition == SidePosition.Down || sidePosition == SidePosition.Up)
		{
			tileWidth = 2 * screenSize.x / tileCount;
			tileHeight = 2 * screenSize.x / coloredTilePrefab.XSizeMultiplier;
		}
		
		Vector2 firstTilePosition = Vector2.zero;
		
		switch(sidePosition)
		{
			case SidePosition.Left:
				firstTilePosition = new Vector2(- screenSize.x + tileWidth / 2, screenSize.y - tileHeight / 2);
				break;
				
			case SidePosition.Right:
				firstTilePosition = new Vector2(screenSize.x - tileWidth / 2, screenSize.y - tileHeight / 2);
				break;
				
			case SidePosition.Up:
				firstTilePosition = new Vector2(- screenSize.x + tileWidth / 2, screenSize.y - tileHeight / 2);
				break;
				
			case SidePosition.Down:
				firstTilePosition = new Vector2(- screenSize.x + tileWidth / 2, - screenSize.y + tileHeight / 2);
				break;
		}
		
		Vector2 currentPosition = firstTilePosition;
		
		for (int i = 0; i < tileCount; i++)
		{
			var tile = Instantiate(coloredTilePrefab, currentPosition, Quaternion.identity, transform);
			tile.SpriteRenderer.size = new Vector2(tileWidth, tileHeight);
			tile.Collider.size = new Vector2(tileWidth, tileHeight);
			
			if (sidePosition == SidePosition.Left || sidePosition == SidePosition.Right)
			{
				currentPosition.y -= tileHeight;
			}
			
			if (sidePosition == SidePosition.Down || sidePosition == SidePosition.Up)
			{
				currentPosition.x += tileWidth;
			}
		}
	}
}
