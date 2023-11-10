using UnityEngine;

public class GameController : MonoBehaviour
{
	[SerializeField] private MechanicController mechanicController;
	[SerializeField] private PlayerController playerController;
	[SerializeField] private PlayerMovementController playerMovementController;
	[SerializeField] private Tutorial tutorial;
	[SerializeField] private CountScreen countScreen;
	[SerializeField] private DeathScreen deathScreen;
	[SerializeField] private ProgressBar progressBar;
	[SerializeField] private PopupText popupText;
	private LevelData levelData;
	private bool isHardMode;
	private float defaultDeltaTime;
	
	private void Start()
	{
		defaultDeltaTime = Time.fixedDeltaTime;
		Restart();
	}
	
	public void Restart()
	{
		Time.fixedDeltaTime = defaultDeltaTime;
		Time.timeScale = 1;
		
		levelData = new LevelData();
		levelData.PointsIncreased += PointsIncreasedHandler;
		levelData.LevelCompleted += LevelCompleted;
		playerController.CoinCollected += PlayerCoinCollectedHandler;
		playerController.TakeDamageEvent += PlayerTakeDamageHandler;
		playerController.Initialize();
		progressBar.Fill(0);
		mechanicController.SetDirection(SideDirection.Horizontal);
		playerMovementController.EnableVisuals();
		mechanicController.Initialize();
		progressBar.RefreshHearts(playerController.CurrentLifes);
		progressBar.UpdateText(SaveLoad.currentLevelSave);
		isHardMode = false;
		
		Debug.Log(SaveLoad.tutorial);
		if (SaveLoad.tutorial)
		{
			SaveLoad.tutorial = false;
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
		Debug.Log(points);
		
		if (points >= levelData.MaxPoints * 3 / 4 && !isHardMode)
		{
			isHardMode = true;
			mechanicController.currentXHardness = 6;
			mechanicController.currentYHardness = 4;
			popupText.Show("HARD MODE!");
		}
		
		progressBar.Fill((float)points / (float)levelData.MaxPoints);
	}
	
	private void LevelCompleted(int reward)
	{
		playerController.CoinCollected -= PlayerCoinCollectedHandler;
		levelData.PointsIncreased -= PointsIncreasedHandler;
		levelData.LevelCompleted -= LevelCompleted;
		playerMovementController.Disable();
		
		deathScreen.Show(true, reward);
		SaveLoad.currentLevelSave += 1;
		SaveLoad.playerCoinsSave += reward;
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
			playerController.CoinCollected -= PlayerCoinCollectedHandler;
			levelData.PointsIncreased -= PointsIncreasedHandler;
			levelData.LevelCompleted -= LevelCompleted;
		}
		
		progressBar.RefreshHearts(lifes);
	}
}
