using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredSidesController : MonoBehaviour
{
	[SerializeField] private ColoredSide leftSide;
	[SerializeField] private ColoredSide rightSide;
	[SerializeField] private ColoredSide upSide;
	[SerializeField] private ColoredSide downSide;
	
	public void ChangeSides(SideDirection sideDirection)
	{
		if (sideDirection == SideDirection.Vertical)
		{
			leftSide.gameObject.SetActive(false);
			rightSide.gameObject.SetActive(false);
			upSide.gameObject.SetActive(true);
			downSide.gameObject.SetActive(true);
		}
		else
		{
			leftSide.gameObject.SetActive(true);
			rightSide.gameObject.SetActive(true);
			upSide.gameObject.SetActive(false);
			downSide.gameObject.SetActive(false);
		}
	}
}

public enum SideDirection
{
	Vertical,
	Horizontal
}
