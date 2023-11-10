using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
	public void Load()
	{
		SceneManager.LoadScene("GameScene");
	}
	
	public void LoadMenu()
	{
		SceneManager.LoadScene("MenuScene");
	}
	
	public void Quit()
	{
		Application.Quit();
	}
}
