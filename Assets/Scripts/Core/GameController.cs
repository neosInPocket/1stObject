using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
	[SerializeField] private MechanicController mechanicController;
	[SerializeField] private PlayerController playerController;
	[SerializeField] private PlayerMovementController playerMovementController;
	[SerializeField] private Tutorial tutorial;
	[SerializeField] private CountScreen countScreen;
	[SerializeField] private DeathScreen deathScreen;
	private LevelData levelData;
	
	private void Start()
	{
		Restart();
		SaveLoad.isTutorialPassed = false;
	}
	
	public void Restart()
	{
		levelData = new LevelData();
		levelData.PointsIncreased += PointsIncreasedHandler;
		levelData.LevelCompleted += LevelCompleted;
		playerController.CoinCollected += PlayerCoinCollectedHandler;
		playerController.TakeDamageEvent += PlayerTakeDamageHandler;
		playerController.Initialize();
		
		if (!SaveLoad.isTutorialPassed)
		{
			tutorial.TutorialEnded += OnTutorialEndHandler;
			tutorial.PlayTutorial();
		}
		else
		{
			countScreen.OnCountEnd += OnCountEndHandler;
			countScreen.PlayCountScreen();
		}
	}
	
	private void OnTutorialEndHandler()
	{
		tutorial.TutorialEnded -= OnTutorialEndHandler;
		countScreen.OnCountEnd += OnCountEndHandler;
		countScreen.PlayCountScreen();
	}
	
	private void OnCountEndHandler()
	{
		playerMovementController.Enable();
	}
	
	private void PointsIncreasedHandler(int points)
	{
		
	}
	
	private void LevelCompleted(int reward)
	{
		playerController.CoinCollected -= PlayerCoinCollectedHandler;
		levelData.PointsIncreased -= PointsIncreasedHandler;
		levelData.LevelCompleted -= LevelCompleted;
		playerMovementController.Disable();
		
		deathScreen.Show(true, reward);
	}
	
	private void PlayerCoinCollectedHandler()
	{
		levelData.IncreaseCurrentPoints(2);
	}
	
	private void PlayerTakeDamageHandler(int lifes)
	{
		if (lifes == 0)
		{
			deathScreen.Show(false);
		}
	}
}
