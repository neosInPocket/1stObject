using System;

public class LevelData
{
	private int maxPoints;
	private int reward;
	private int currentPoints;
	public int MaxPoints => maxPoints;
	
	public Action<int> PointsIncreased;
	public Action<int> LevelCompleted;
	
	public LevelData()
	{
		currentPoints = 0;
		maxPoints = GetMaxPoints(SaveLoad.currentLevelSave);
		reward = GetReward(SaveLoad.currentLevelSave);
	}
	
	private int GetMaxPoints(int level)
	{
		return (int)(-1000 / (level + 17.667f) + 60);
	} 
	
	private int GetReward(int level)
	{
		return (int)(-5 / (level + 1) + 7);
	}
	
	public void IncreaseCurrentPoints(int value)
	{
		if (currentPoints + value >= maxPoints)
		{
			currentPoints = maxPoints;
			PointsIncreased?.Invoke(currentPoints);
			LevelCompleted?.Invoke(reward);
			return;
		}
		
		currentPoints += value;
		PointsIncreased?.Invoke(currentPoints);
	}
}
