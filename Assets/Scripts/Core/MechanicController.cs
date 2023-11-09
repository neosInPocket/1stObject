using UnityEngine;

public class MechanicController : MonoBehaviour
{
	[SerializeField] private ColoredSidesController sidesController;
	[SerializeField] private PlayerMovementController playerMovementController;
	[SerializeField] private PlayerController playerController;
	[SerializeField] private float gravityChangeChance;
	private SideDirection currentSideDirection;
	
	private void Start()
	{
		playerMovementController.PlayerTileTouched += PlayerTileTouched;
		sidesController.Initialize();
		playerController.CurrentColor = sidesController.SetTilesColor(3, 2);
		
		currentSideDirection = SideDirection.Horizontal;
		SetDirection(SideDirection.Horizontal);
	}
	
	private void PlayerTileTouched(ColoredTile tile)
	{
		if (tile.TileColor == playerController.CurrentColor && tile.SpriteRenderer.enabled)
		{
			playerController.CurrentColor = sidesController.SetTilesColor(3, 2);
			if (Random.Range(0, 1) < gravityChangeChance)
			{
				ToggleSides();
			}
			
			return;
		}
		
		if (!tile.SpriteRenderer.enabled || (tile.TileColor != playerController.CurrentColor && tile.SpriteRenderer.enabled))
		{
			if (playerController.TakeDamage())
			{
				 playerMovementController.Disable();
			}
			else
			{
				playerController.CurrentColor = sidesController.SetTilesColor(3, 2);
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
	
	private void SetDirection(SideDirection direction)
	{
		sidesController.SetSides(direction);
		playerMovementController.SetGravity(direction);
		currentSideDirection = direction;
	}
}
