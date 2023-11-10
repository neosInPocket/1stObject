using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
	[SerializeField] private TMP_Text playerCoinsText;
	[SerializeField] private Button lifesUpgradeButton;
	[SerializeField] private Button gravityUpgradeButton;
	[SerializeField] private Image[] lifesStars;
	[SerializeField] private Image[] gravityStars;
	
	private void Awake()
	{
		//SaveLoad.Reset();
	}
	
	private void Start()
	{
		UpdateShop();
	}
	
	private void UpdateShop()
	{
		playerCoinsText.text = SaveLoad.playerCoinsSave.ToString();
		
		if (SaveLoad.playerCoinsSave - 10 < 0 || SaveLoad.maxLifesSave >= 3)
		{
			lifesUpgradeButton.interactable = false;
		}
		else
		{
			lifesUpgradeButton.interactable = true;
		}
		
		if (SaveLoad.playerCoinsSave - 5 < 0 || SaveLoad.gravitySaves >= 3)
		{
			gravityUpgradeButton.interactable = false;
		}
		else
		{
			gravityUpgradeButton.interactable = true;
		}
		
		foreach (var star in lifesStars)
		{
			star.enabled = false;
		}
		
		for (int i = 0; i < SaveLoad.maxLifesSave; i++)
		{
			lifesStars[i].enabled = true;
		}
		
		foreach (var star in gravityStars)
		{
			star.enabled = false;
		}
		
		for (int i = 0; i < SaveLoad.gravitySaves; i++)
		{
			gravityStars[i].enabled = true;
		}
	}
	
	public void BuyLifes()
	{
		SaveLoad.playerCoinsSave -= 10;
		SaveLoad.maxLifesSave++;
		UpdateShop();
	}
	
	public void BuyGravity()
	{
		SaveLoad.playerCoinsSave -= 5;
		SaveLoad.gravitySaves++;
		UpdateShop();
	}
}
