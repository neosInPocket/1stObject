using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.iOS;
using UnityEngine;
using Random = System.Random;

public class ColoredSidesController : MonoBehaviour
{
	[SerializeField] private ColoredSide leftSide;
	[SerializeField] private ColoredSide rightSide;
	[SerializeField] private ColoredSide upSide;
	[SerializeField] private ColoredSide downSide;
	[SerializeField] private AvaliableColorsSO avaliableColors;
	private int horizontalTileCount;
	private int verticalTileCount = 2;
	private List<ColoredSide> coloredSides;
	
	private void Start()
	{
		coloredSides = new List<ColoredSide>() { leftSide, rightSide, upSide, downSide };
		
		horizontalTileCount = avaliableColors.Colors.Count;
		
		leftSide.SpawnTiles(horizontalTileCount, verticalTileCount);
		rightSide.SpawnTiles(horizontalTileCount, verticalTileCount);
		upSide.SpawnTiles(horizontalTileCount, verticalTileCount);
		downSide.SpawnTiles(horizontalTileCount, verticalTileCount);
		
		SetTilesColor(avaliableColors.Colors[1], avaliableColors.Colors, 3, 2);
	}
	
	public void ChangeSides(SideDirection sideDirection)
	{
		if (sideDirection == SideDirection.Vertical)
		{
			leftSide.gameObject.SetActive(false);
			rightSide.gameObject.SetActive(false);
			upSide.gameObject.SetActive(true);
			downSide.gameObject.SetActive(true);
		}
		else
		{
			leftSide.gameObject.SetActive(true);
			rightSide.gameObject.SetActive(true);
			upSide.gameObject.SetActive(false);
			downSide.gameObject.SetActive(false);
		}
	}
	
	private void SetTilesColor(Color preDefinedColor, List<Color> colors, int xColorCount, int yColorCount)
	{
		leftSide.SetTilesColor(ShuffleColorsList(preDefinedColor, colors, xColorCount), xColorCount);
		rightSide.SetTilesColor(ShuffleColorsList(preDefinedColor, colors, xColorCount), xColorCount);
		upSide.SetTilesColor(ShuffleColorsList(preDefinedColor, colors, yColorCount), yColorCount);
		downSide.SetTilesColor(ShuffleColorsList(preDefinedColor, colors, yColorCount), yColorCount);
	}
	
	private List<Color> ShuffleColorsList(Color preDefinedColor, List<Color> colors, int colorCount)
	{
		var random = new Random();
		random.Shuffle<Color>(colors);
		
		int preDefinedColorIndex = colors.IndexOf(preDefinedColor);
		var rndIndex = UnityEngine.Random.Range(0, colorCount);
		colors.RemoveAt(preDefinedColorIndex);
		colors.Insert(rndIndex, preDefinedColor);
		
		return colors;
	}
}



public enum SideDirection
{
	Vertical,
	Horizontal
}
