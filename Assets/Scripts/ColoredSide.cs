using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.SearchService;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class ColoredSide : MonoBehaviour
{
	[SerializeField] private SidePosition sidePosition;
	[SerializeField] private ColoredTile coloredTilePrefab;
	private List<ColoredTile> tiles = new List<ColoredTile>();
	
	
	public void SetTilesColor(List<Color> colors, int colorCount)
	{
		int loopCount = colorCount;
		int tilesColorCount = tiles.Count / colorCount;
		
		int pointer = 0;
		
		for (int i = 0; i < loopCount; i++)
		{
			for (int j = 0; j < tilesColorCount; j++)
			{
				tiles[pointer + j].SpriteRenderer.color = colors[i];
			}
			
			pointer += tilesColorCount;
		}
	}
	
	public void SpawnTiles(int horizontalTileCount, int verticalTileCount)
	{
		var screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
		
		float tileWidth = 0;
		float tileHeight = 0;
		
		if (sidePosition == SidePosition.Left || sidePosition == SidePosition.Right)
		{
			tileHeight = 2 * screenSize.y / horizontalTileCount;
			tileWidth = 2 * screenSize.x / coloredTilePrefab.XSizeMultiplier;
		}
		
		if (sidePosition == SidePosition.Down || sidePosition == SidePosition.Up)
		{
			tileWidth = 2 * screenSize.x / verticalTileCount;
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
		
		if (sidePosition == SidePosition.Left || sidePosition == SidePosition.Right)
		{
			for (int i = 0; i < horizontalTileCount; i++)
			{
				var tile = Instantiate(coloredTilePrefab, currentPosition, Quaternion.identity, transform);
				tile.SpriteRenderer.size = new Vector2(tileWidth, tileHeight);
				tile.Collider.size = new Vector2(tileWidth, tileHeight);
				currentPosition.y -= tileHeight;
				tile.sidePosition = sidePosition;
				
				tiles.Add(tile);
			}
		}
		
		if (sidePosition == SidePosition.Down || sidePosition == SidePosition.Up)
		{
			for (int i = 0; i < verticalTileCount; i++)
			{
				var tile = Instantiate(coloredTilePrefab, currentPosition, Quaternion.identity, transform);
				tile.SpriteRenderer.size = new Vector2(tileWidth, tileHeight);
				tile.Collider.size = new Vector2(tileWidth, tileHeight);
				currentPosition.x += tileWidth;
				tile.sidePosition = sidePosition;
				
				tiles.Add(tile);
			}
		}
	}
}

public enum SidePosition
	{
		Left,
		Right,
		Up,
		Down
	}
