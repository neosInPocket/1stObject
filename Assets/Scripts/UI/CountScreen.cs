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
		StartCoroutine(Count());
	}
	
	private IEnumerator Count()
	{
		text.text = "3";
		animator.SetTrigger("Popup");
		yield return new WaitForSeconds(1);
		animator.SetTrigger("Hide");
		
		text.text = "2";
		animator.SetTrigger("Popup");
		yield return new WaitForSeconds(1);
		animator.SetTrigger("Hide");
		
		text.text = "1";
		animator.SetTrigger("Popup");
		yield return new WaitForSeconds(1);
		animator.SetTrigger("Hide");
		
		OnCountEnd?.Invoke();
		text.text = "GO";
		animator.SetTrigger("Popup");
		yield return new WaitForSeconds(1);
		animator.SetTrigger("Hide");
	}
}
