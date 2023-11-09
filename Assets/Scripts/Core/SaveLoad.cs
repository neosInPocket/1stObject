using UnityEngine;
using UnityEngine.Diagnostics;

public static class SaveLoad
{
	public static int currentLevelSave
	{
		get
		{
			 return PlayerPrefs.GetInt("currentLevelSave", 1);
		}
		
		set
		{
			m_currentLevelSave = value;
			PlayerPrefs.SetInt("currentLevelSave", m_currentLevelSave);
		}
	}
	private static int m_currentLevelSave;
	
	public static bool isTutorialPassed
	{
		get
		{
			var tutorInt = PlayerPrefs.GetInt("isTutorialPassed", 0);
			if (tutorInt == 0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		
		set
		{
			if (value)
			{
				m_isTutorialPassed = 1;
			}
			else
			{
				m_isTutorialPassed = 0;
			}
			PlayerPrefs.SetInt("isTutorialPassed", m_isTutorialPassed);
		}
	}
	private static int m_isTutorialPassed;
	
	public static int playerCoinsSave
	{
		get
		{
			return PlayerPrefs.GetInt("playerCoinsSave", 10);
		}
		
		set
		{
			m_playerCoinsSave = value;
			PlayerPrefs.SetInt("playerCoinsSave", m_playerCoinsSave);
		}
	}
	private static int m_playerCoinsSave;
	
	public static int maxLifesSave
	{
		get
		{
			return PlayerPrefs.GetInt("maxLifesSave", 1);
		}
		
		set
		{
			m_maxLifesSave = value;
			PlayerPrefs.SetInt("maxLifesSave", m_maxLifesSave);
		}
	}
	private static int m_maxLifesSave;
	
	public static void Reset()
	{
		playerCoinsSave = 100;
		maxLifesSave = 1;
		currentLevelSave = 1;
		isTutorialPassed = false;
	}
}
