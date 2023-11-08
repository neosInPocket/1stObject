using UnityEngine;

public class MechanicController : MonoBehaviour
{
	[SerializeField] private ColoredSidesController sidesController;
	[SerializeField] private PlayerMovementController playerMovementController;
	private SideDirection currentSideDirection;
	
	private void Start()
	{
		currentSideDirection = SideDirection.Vertical;
		SetDirection(SideDirection.Vertical);
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
