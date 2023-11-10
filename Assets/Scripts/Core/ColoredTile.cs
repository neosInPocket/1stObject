using System;
using UnityEngine;

public class ColoredTile : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private BoxCollider2D boxCollider2D;
	[SerializeField] private float xSizeMultiplier = 15.4f;
	
	
	public SpriteRenderer SpriteRenderer => spriteRenderer;
	public BoxCollider2D Collider => boxCollider2D;
	public float XSizeMultiplier => xSizeMultiplier;
	public SidePosition sidePosition { get; set; }	
	public Color TileColor { get; set; }
	public Action<Color> PlayerTileTouched;
}
