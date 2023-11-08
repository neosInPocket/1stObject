using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class PlayerMovementController : MonoBehaviour
{
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private float jumpSpeed;
	[SerializeField] private float speed;
	private GravityDirection currentGravityDirection;
	private float screenRatio;
	private int currentSpeedMultiplier;
	
	private void Start()
	{
		var screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
		screenRatio = screenSize.y / screenSize.x;
		
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
		Enable();
	}
	
	public void Initialize()
	{
		currentGravityDirection = GravityDirection.Vertical;
		ChangeGravity(GravityDirection.Horizontal);
	}
	
	private void OnPlayerTouchHandler(Finger finger)
	{
		if (currentGravityDirection == GravityDirection.Vertical)
		{
			rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
		}
		else
		{
			rb.velocity = new Vector2(jumpSpeed / screenRatio, rb.velocity.y);
		}
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
		}
	}
	
	public void ChangeGravity(GravityDirection direction)
	{
		if (direction == GravityDirection.Vertical)
		{
			Physics2D.gravity = new Vector2(0, -9.81f);
		}
		else
		{
			Physics2D.gravity = new Vector2(9.81f / screenRatio, 0);
		}
	}
	
	public void Enable()
	{
		Touch.onFingerDown += OnPlayerTouchHandler;
		rb.velocity = Vector2.right * speed;
		currentSpeedMultiplier = 1;
	}
	
	public void Disable() => Touch.onFingerDown -= OnPlayerTouchHandler;
}

public enum GravityDirection
{
	Vertical,
	Horizontal
}
