using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AvaliableColors", menuName = "Avaliable colors")]
public class AvaliableColorsSO : ScriptableObject
{
	[SerializeField] private List<Color> colors;
	[SerializeField] private int initialColorIndex;
	public List<Color> Colors => colors;
	public Color InitialColor => colors[initialColorIndex];
}
