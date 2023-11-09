using System;
using System.Collections.Generic;
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
	public Color CurrentColor;
	public AvaliableColorsSO AvaliableColors { get; set; }
	
	public void Initialize()
	{
		horizontalTileCount = avaliableColors.Colors.Count;
		
		leftSide.SpawnTiles(horizontalTileCount, verticalTileCount);
		rightSide.SpawnTiles(horizontalTileCount, verticalTileCount);
		upSide.SpawnTiles(horizontalTileCount, verticalTileCount);
		downSide.SpawnTiles(horizontalTileCount, verticalTileCount);
	}
	
	public void SetSides(SideDirection sideDirection)
	{
		if (sideDirection == SideDirection.Vertical)
		{
			leftSide.Disable();
			rightSide.Disable();
			upSide.Enable();
			downSide.Enable();;
		}
		else
		{
			leftSide.Enable();
			rightSide.Enable();
			upSide.Disable();
			downSide.Disable();
		}
	}
	
	public Color SetTilesColor(int xColorCount, int yColorCount)
	{
		var colors = avaliableColors.Colors;
		var preDefinedColor = GetRandomColor();
		
		leftSide.SetTilesColor(ShuffleColorsList(preDefinedColor, colors, xColorCount), xColorCount);
		rightSide.SetTilesColor(ShuffleColorsList(preDefinedColor, colors, xColorCount), xColorCount);
		upSide.SetTilesColor(ShuffleColorsList(preDefinedColor, colors, yColorCount), yColorCount);
		downSide.SetTilesColor(ShuffleColorsList(preDefinedColor, colors, yColorCount), yColorCount);
		CurrentColor = preDefinedColor;
		
		return preDefinedColor;
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
	
	public Color GetRandomColor()
	{
		return avaliableColors.Colors[UnityEngine.Random.Range(0, avaliableColors.Colors.Count)];
	}
}



public enum SideDirection
{
	Vertical,
	Horizontal
}
