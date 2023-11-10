using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
	[SerializeField] private Image fill;
	[SerializeField] private TMP_Text levelText;
	[SerializeField] private Image[] hearts;
	
	public void Fill(float value)
	{
		fill.fillAmount = value;
	}
	
	public void UpdateText(int level)
	{
		levelText.text = "LEVEL " + level.ToString();
	}
	
	public void RefreshHearts(int heartCount)
	{
		foreach (var heart in hearts)
		{
			heart.color = new Color(0, 0, 0, 1);
		}
		
		for (int n = 0; n < heartCount; n++)
		{
			hearts[n].color = new Color(1, 1, 1, 1);
		}
	}
}
