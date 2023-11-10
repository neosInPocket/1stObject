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
	
	public static bool tutorial 
	{
		get
		{
			var tutorInt = PlayerPrefs.GetInt("tutorial", 1);
			if (tutorInt == 1)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		
		set
		{
			if (value)
			{
				PlayerPrefs.SetInt("tutorial", 1);
			}
			else
			{
				PlayerPrefs.SetInt("tutorial", 0);
			}
		}
	}
	
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
	
	public static int gravitySaves
	{
		get
		{
			return PlayerPrefs.GetInt("gravitySaves", 1);
		}
		
		set
		{
			m_gravitySave = value;
			PlayerPrefs.SetInt("gravitySaves", m_gravitySave);
		}
	}
	private static int m_gravitySave;
	
	public static float soundVolume
	{
		get
		{
			return PlayerPrefs.GetFloat("soundVolume", 1f);
		}
		
		set
		{
			m_soundVolume = value;
			PlayerPrefs.SetFloat("soundVolume", m_soundVolume);
		}
	}
	private static float m_soundVolume;
	
	
	public static void Reset()
	{
		playerCoinsSave = 10;
		maxLifesSave = 1;
		currentLevelSave = 1;
		gravitySaves = 0;
		tutorial = true;
		soundVolume = 1f;
	}
}
