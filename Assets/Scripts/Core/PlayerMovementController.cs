using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.Windows.Speech;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class PlayerMovementController : MonoBehaviour
{
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private float jumpSpeed;
	[SerializeField] private float speed;
	[SerializeField] private PlayerJumpArrow playerJumpArrow;
	[SerializeField] private Transform startPosition;
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private TrailRenderer trail;
	private GravityDirection currentGravityDirection;
	private float screenRatio;
	public Action<ColoredTile> PlayerTileTouched;
	
	private void Awake()
	{
		var screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
		screenRatio = screenSize.y / screenSize.x;
		
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
	}
	
	private void OnPlayerTouchHandler(Finger finger)
	{
		if (Physics2D.gravity.x < 0) rb.velocity = new Vector2(jumpSpeed / screenRatio, rb.velocity.y);
		if (Physics2D.gravity.x > 0) rb.velocity = new Vector2(-jumpSpeed / screenRatio, rb.velocity.y);
		if (Physics2D.gravity.y < 0) rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
		if (Physics2D.gravity.y > 0) rb.velocity = new Vector2(rb.velocity.x, -jumpSpeed);
	}
	
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.TryGetComponent<ColoredTile>(out ColoredTile tile))
		{
			if (currentGravityDirection == GravityDirection.Vertical)
			{
				if (tile.sidePosition == SidePosition.Left && rb.velocity.x > 0) return;
				if (tile.sidePosition == SidePosition.Right && rb.velocity.x < 0) return;
				
				rb.velocity = Vector2.Reflect(rb.velocity, Vector2.right);
			}
			
			if (currentGravityDirection == GravityDirection.Horizontal)
			{
				if (tile.sidePosition == SidePosition.Up && rb.velocity.y < 0) return;
				if (tile.sidePosition == SidePosition.Down && rb.velocity.x > 0) return;
				
				rb.velocity = Vector2.Reflect(rb.velocity, Vector2.up);
			}
			
			PlayerTileTouched?.Invoke(tile);
		}
	}
	
	public void SetGravity(SideDirection direction)
	{
		if (direction == SideDirection.Vertical)
		{
			if (transform.position.x > 0)
			{
				Physics2D.gravity = new Vector2(-9.81f / screenRatio, 0);
				playerJumpArrow.RotateArrow(Vector2.right);
			}
			else
			{
				Physics2D.gravity = new Vector2(9.81f / screenRatio, 0);
				playerJumpArrow.RotateArrow(Vector2.left);
			}
			
			if (transform.position.y > 0)
			{
				rb.velocity = new Vector2(0, -speed);
			}
			else
			{
				rb.velocity = new Vector2(0, speed);
			}
		}
		else
		{
			if (transform.position.y > 0)
			{
				Physics2D.gravity = new Vector2(0, -9.81f);
				playerJumpArrow.RotateArrow(Vector2.up);
			}
			else
			{
				Physics2D.gravity = new Vector2(0, 9.81f);
				playerJumpArrow.RotateArrow(Vector2.down);
			}
			
			if (transform.position.x > 0)
			{
				rb.velocity = new Vector2(-speed, 0);
			}
			else
			{
				rb.velocity = new Vector2(speed, 0);
			}
		}
	}
	
	public void Enable()
	{
		Touch.onFingerDown += OnPlayerTouchHandler;
		rb.constraints = RigidbodyConstraints2D.None;
		transform.position = startPosition.position;
		spriteRenderer.enabled = true;
		trail.enabled = true;
		playerJumpArrow.Image.enabled = true;
		rb.velocity = Vector2.right * speed;
	}
	
	public void Disable()
	{
		Touch.onFingerDown -= OnPlayerTouchHandler;
		rb.constraints = RigidbodyConstraints2D.FreezeAll;
		rb.velocity = Vector2.zero;
		spriteRenderer.enabled = false;
		trail.enabled = false;
		playerJumpArrow.Image.enabled = false;
	}
	
	private void OnDestroy()
	{
		Touch.onFingerDown -= OnPlayerTouchHandler;
	}
}

public enum GravityDirection
{
	Vertical,
	Horizontal
}
