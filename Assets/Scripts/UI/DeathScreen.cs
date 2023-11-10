using TMPro;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
	[SerializeField] private TMP_Text deathText;
	[SerializeField] private TMP_Text coinsText;
	[SerializeField] private TMP_Text restartButtonText;
	
	public void Show(bool isWin, int reward = 0)
	{
		gameObject.SetActive(true);
		
		if (isWin)
		{
			deathText.text = "YOU WIN!";
			restartButtonText.text = "NEXT LEVEL";
		}
		else
		{
			deathText.text = "YOU LOSE!";
			restartButtonText.text = "TRY AGAIN";
		}
		
		coinsText.text = "+" + reward.ToString();
	}
	
	public void Hide()
	{
		gameObject.SetActive(false);
	}
}
