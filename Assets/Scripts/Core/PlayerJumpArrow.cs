using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class PlayerJumpArrow : MonoBehaviour
{
	[SerializeField] private Image image;
	[SerializeField] private float rotateSpeed;
	[SerializeField] private float angleThreshold;
	private int rotationMultiplier;
	public Image Image => image;
	
	public void RotateArrow(Vector2 direction)
	{
		StopAllCoroutines();
		StartCoroutine(Rotate(direction));
	}
	
	private IEnumerator Rotate(Vector2 direction)
	{
		float angle = 100;
		Vector3 normalizedDirection = Vector3.zero;
		float deltaPhi = 0;
		float crossProduct = 0;
		
		while (angle > angleThreshold)
		{
			normalizedDirection = new Vector3(direction.x, direction.y, 0);
			angle = Vector2.Angle(normalizedDirection, transform.right);
			deltaPhi = rotateSpeed * (angle + 1f);
			
			crossProduct = Vector3.Cross(transform.right, direction).z;
			rotationMultiplier = (int)(crossProduct / Mathf.Abs(crossProduct));
			deltaPhi *= rotationMultiplier;
			
			var a11 = Mathf.Cos(deltaPhi);
			var a12 = -Mathf.Sin(deltaPhi);
			var a21 = Mathf.Sin(deltaPhi);
			var a22 = Mathf.Cos(deltaPhi);
			
			transform.right = new Vector2(a11 * transform.right.x + a12 * transform.right.y, a21 * transform.right.x + a22 * transform.right.y);
			yield return new WaitForSecondsRealtime(0.01f);
		}
		
		transform.right = direction;
	}
}
