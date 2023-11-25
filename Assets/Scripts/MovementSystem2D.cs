using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementSystem2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;
	[Range(0, .3f)] [SerializeField] private float m_SpeedSmooth = .05f;    // Suavizado del movimiento.
	[SerializeField] public bool m_AirControl = false;
	[SerializeField] private LayerMask m_WhatIsGround;						// Layer del suelo donde el player se apoya.
	[SerializeField] private Transform m_GroundCheck;                       // Verifica si esta en contacto con el suelo.

    [SerializeField] private float k_GroundCheckRadio = .2f; 	// Radio del circulo que detecta el Suelo.
	public bool m_InGround;             	// Esta tocando el Suelo?.
	private Rigidbody2D m_Rigidbody;			// Cuerpo rigido del Player.
	public bool m_SpriteRight = true;  		// Esta el sprite en la posicion original?(mirando a la derecha).
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent InGround;


	private void Awake()
	{
		m_Rigidbody = GetComponent<Rigidbody2D>();

		if (InGround == null)
			InGround = new UnityEvent();
	}

	private void FixedUpdate()
	{
		bool m_IsGrounded = m_InGround;
		m_InGround = false;

		Collider2D collider = Physics2D.OverlapCircle(m_GroundCheck.position, k_GroundCheckRadio, m_WhatIsGround); // Detecta si SueloChack detecta Suelo.

		if (collider != null && collider.gameObject.tag == "Ground")
		{
			m_InGround = true;
			if (!m_IsGrounded)
				InGround.Invoke();
		}		
	}


	public void Move(float move, bool jump)
	{

		//only control the player if grounded or airControl is turned on
		if (m_InGround || m_AirControl)
		{
			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody.velocity = Vector3.SmoothDamp(m_Rigidbody.velocity, targetVelocity, ref m_Velocity, m_SpeedSmooth);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_SpriteRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_SpriteRight)
			{
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump...
		if (m_InGround && jump)
		{
			// Add a vertical force to the player.
			m_InGround = false;
			m_Rigidbody.AddForce(new Vector2(0f, m_JumpForce));
		}
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_SpriteRight = !m_SpriteRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
