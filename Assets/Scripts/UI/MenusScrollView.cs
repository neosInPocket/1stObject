using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using Screen = UnityEngine.Screen;

public class MenusScrollView : MonoBehaviour
{
	[SerializeField] private Transform content;
	[SerializeField] private float speed;
	private Vector2 screenSize;
	
	private void Start()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
		Touch.onFingerUp += OnFingerUpHandler;
		Touch.onFingerDown += OnFingerDownHandler;
		screenSize = new Vector2(Screen.width, Screen.height);
	}
	
	private void OnFingerUpHandler(Finger finger)
	{
		StopAllCoroutines();
		
		if (content.position.x > screenSize.x)
		{
			float destination = screenSize.x * 3 / 2;
			StartCoroutine(MoveToDestination(destination));
			return;
		}
		
		if (content.position.x < 0)
		{
			StartCoroutine(MoveToDestination(-screenSize.x / 2));
			return;
		}
		
		if (content.position.x > 0 || content.position.x < screenSize.x)
		{
			StartCoroutine(MoveToDestination(screenSize.x / 2));
			return;
		}
	}
	
	private void OnFingerDownHandler(Finger finger)
	{
		StopAllCoroutines();
	}
	
	private IEnumerator MoveToDestination(float destination)
	{
		var direction = (destination - content.position.x) / Mathf.Abs(destination - content.position.x);
		float distance = 0;
		
		while (Mathf.Abs(content.position.x - destination) > 1)
		{
			distance = Mathf.Abs(destination - content.position.x);
			content.position = new Vector2(content.position.x + speed * direction * (distance + 10) * Time.deltaTime, content.position.y);
			yield return new WaitForEndOfFrame();
		}
		
		content.position = new Vector2(destination, content.position.y);
	}
	
	private void OnDisable()
	{
		Touch.onFingerUp -= OnFingerUpHandler;
		Touch.onFingerDown -= OnFingerDownHandler;
	}
}
