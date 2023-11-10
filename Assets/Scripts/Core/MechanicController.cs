using UnityEngine;

public class MechanicController : MonoBehaviour
{
	[SerializeField] private ColoredSidesController sidesController;
	[SerializeField] private PlayerMovementController playerMovementController;
	[SerializeField] private PlayerController playerController;
	[SerializeField] private float gravityChangeChance;
	private SideDirection currentSideDirection;
	private ColoredTile lastTile;
	public int currentXHardness { get; set; }
	public int currentYHardness { get; set; }
	
	private void Start()
	{
		playerMovementController.PlayerTileTouched += PlayerTileTouched;
		sidesController.Initialize();
	}
	
	public void Initialize()
	{
		lastTile = null;
		currentXHardness = 3;
		currentYHardness = 2;
		playerController.CurrentColor = sidesController.SetTilesColor(currentXHardness, currentYHardness);
		
		currentSideDirection = SideDirection.Horizontal;
		SetDirection(SideDirection.Horizontal);
	}
	
	private void PlayerTileTouched(ColoredTile tile)
	{
		if (lastTile != null)
		{
			if (lastTile.sidePosition == tile.sidePosition)
			{
				return;
			}
		}
		
		if (tile.TileColor == playerController.CurrentColor && tile.SpriteRenderer.enabled)
		{
			lastTile = tile;
			playerController.CurrentColor = sidesController.SetTilesColor(currentXHardness, currentYHardness);
			if (Random.Range(0, 1f) < gravityChangeChance)
			{
				ToggleSides();
			}
			
			return;
		}
		
		if (!tile.SpriteRenderer.enabled || (tile.TileColor != playerController.CurrentColor && tile.SpriteRenderer.enabled))
		{
			lastTile = tile;
			if (playerController.TakeDamage())
			{
				 playerMovementController.Disable();
			}
			else
			{
				playerController.CurrentColor = sidesController.SetTilesColor(currentXHardness, currentYHardness);
				if (Random.Range(0, 1) < gravityChangeChance)
				{
					ToggleSides();
				}
			}
			return;
		}
	}
	
	public void ToggleSides()
	{
		if (currentSideDirection == SideDirection.Vertical)
		{
			SetDirection(SideDirection.Horizontal);
		}
		else
		{
			SetDirection(SideDirection.Vertical);
		}
	}
	
	public void SetDirection(SideDirection direction)
	{
		sidesController.SetSides(direction);
		playerMovementController.SetGravity(direction);
		currentSideDirection = direction;
	}
}
