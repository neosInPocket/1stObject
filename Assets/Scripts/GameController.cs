using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
	[SerializeField] private MechanicController mechanicController;
	[SerializeField] private PlayerController playerController;
	[SerializeField] private PlayerMovementController playerMovementController;
	private LevelData levelData;
	
	private void Start()
	{
		Restart();
	}
	
	public void Restart()
	{
		levelData = new LevelData();
		levelData.PointsIncreased += PointsIncreasedHandler;
		levelData.LevelCompleted += LevelCompleted;
		playerController.coinCollected += PlayerCoinCollectedHandler;
	}
	
	private void PointsIncreasedHandler(int points)
	{
		if (points >= levelData.MaxPoints)
		{
			mechanicController.ToggleSides();
		}
	}
	
	private void LevelCompleted(int reward)
	{
		playerController.coinCollected -= PlayerCoinCollectedHandler;
		playerMovementController.Disable();
		levelData.PointsIncreased -= PointsIncreasedHandler;
		levelData.LevelCompleted -= LevelCompleted;
	}
	
	private void PlayerCoinCollectedHandler()
	{
		
	}
}
