using System.Collections;
using TMPro;
using UnityEngine;

public class PopupText : MonoBehaviour
{
	[SerializeField] private Animator animator;
	[SerializeField] private TMP_Text text;
	
	public void Show(string caption)
	{
		gameObject.SetActive(true);
		StartCoroutine(Popup(caption));
	}
	
	private IEnumerator Popup(string caption)
	{
		var defaultDeltaTime = Time.fixedDeltaTime;
		
		Time.timeScale = 0.4f;
		Time.fixedDeltaTime = 0.001f;
		text.text = caption;
		yield return new WaitForSeconds(2);
		Time.timeScale = 1f;
		Time.fixedDeltaTime = defaultDeltaTime;
		gameObject.SetActive(false);
	}
}
