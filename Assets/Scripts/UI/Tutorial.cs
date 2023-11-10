using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class Tutorial : MonoBehaviour
{
	[SerializeField] private TMP_Text characterText;
	public Action TutorialEnded;
	
	private void Start()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
		
		Touch.onFingerDown += Character1;
		characterText.text = "Welcome to Fortune Tiger: Fairy Quest!";
	}
	
	public void PlayTutorial()
	{
		gameObject.SetActive(true);
	}
	
	private void Character1(Finger finger)
	{
		Touch.onFingerDown -= Character1;
		Touch.onFingerDown += Character2;
		
		characterText.text = "Here is your ball! Control it by tapping the screen";
	}
	
	private void Character2(Finger finger)
	{
		Touch.onFingerDown -= Character2;
		Touch.onFingerDown += Character3;
		
		characterText.text = "Your goal is to touch side tile that matches your ball color";
	}
	
	private void Character3(Finger finger)
	{
		Touch.onFingerDown -= Character3;
		Touch.onFingerDown += Character4;
		
		characterText.text = "Be aware of spontaneous changes in gravity: it can change in any moment!";
	}
	
	private void Character4(Finger finger)
	{
		Touch.onFingerDown -= Character4;
		Touch.onFingerDown += Character5;
		
		characterText.text = "Collect coins, complete levels and buy different upgrades for your ball";
	}
	
	private void Character5(Finger finger)
	{
		Touch.onFingerDown -= Character5;
		Touch.onFingerDown += Character6;
		
		characterText.text = "Good luck!";
	}
	
	private void Character6(Finger finger)
	{
		Touch.onFingerDown -= Character6;
		TutorialEnded?.Invoke();
		gameObject.SetActive(false);
	}
}
