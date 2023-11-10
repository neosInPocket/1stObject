using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class CountScreen : MonoBehaviour
{
	[SerializeField] private Animator animator;
	[SerializeField] private TMP_Text text;
	public Action OnCountEnd;
	
	public void PlayCountScreen()
	{
		gameObject.SetActive(true);
		StartCoroutine(Count());
	}
	
	private IEnumerator Count()
	{
		yield return new WaitForSeconds(220f / 60f);
		OnCountEnd?.Invoke();
		gameObject.SetActive(false);
	}
}
