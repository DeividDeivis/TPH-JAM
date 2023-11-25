using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class MovementSystem2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;      // Velocidad de movimiento agachado. 1 = 100%
	[Range(1, 2)] [SerializeField] private float m_RunSpeed = 1.3f;
	[Range(0, .3f)] [SerializeField] private float m_SpeedSmooth = .05f;    // Suavizado del movimiento.
	[SerializeField] public bool m_AirControl = false;
	[SerializeField] private LayerMask m_WhatIsGround;							// Layer del suelo donde el player se apoya.
	[SerializeField] private Transform m_GroundCheck;							// Verifica si esta en contacto con el suelo.
	[SerializeField] private Transform m_CeilingCheck;							// Verifica si esta en contacto con un techo.
	[SerializeField] private Collider m_StandingCollider;						// El collider se deshabilita cuando esta agachado.

	const float k_GroundCheckRadio = .2f; 	// Radio del circulo que detecta el Suelo.
	public bool m_InGround;             	// Esta tocando el Suelo?.
	const float k_CeilingCheckRadio = .2f;    // Radio del circulo que detecta si esta chocando con un techo. No permite salto o pararse.
	private Rigidbody m_Rigidbody;		// Cuerpo rigido del Player.
	public bool m_SpriteRight = true;  	// Esta el sprite en la posicion original?(mirando a la derecha).
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent InGround;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent IsCrounch;
	private bool m_IsCrouch = false;

	private void Awake()
	{
		m_Rigidbody = GetComponent<Rigidbody>();

		if (InGround == null)
			InGround = new UnityEvent();

		if (IsCrounch == null)
			IsCrounch = new BoolEvent();
	}

	private void FixedUpdate()
	{
		bool m_IsGrounded = m_InGround;
		m_InGround = false;

		Collider[] colliders = Physics.OverlapSphere(m_GroundCheck.position, k_GroundCheckRadio, m_WhatIsGround); // Detecta si SueloChack detecta Suelo.
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_InGround = true;
				if (!m_IsGrounded)
					InGround.Invoke();
			}
		}
	}


	public void Move(float move, bool run,bool crouch, bool jump)
	{
		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics.OverlapSphere(m_CeilingCheck.position, k_CeilingCheckRadio, m_WhatIsGround).Length > 0)
			{
				crouch = true;
			}
		}

		//only control the player if grounded or airControl is turned on
		if (m_InGround || m_AirControl)
		{

			// If crouching
			if (crouch && !run)
			{
				if (!m_IsCrouch)
				{
					m_IsCrouch = true;
					IsCrounch.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_StandingCollider != null)
					m_StandingCollider.enabled = false;
			} else
			{
				// Enable the collider when not crouching
				if (m_StandingCollider != null)
					m_StandingCollider.enabled = true;

				if (m_IsCrouch)
				{
					m_IsCrouch = false;
					IsCrounch.Invoke(false);
				}

				if (run) 
				{
					move *= m_RunSpeed;
					m_IsCrouch = false;
					IsCrounch.Invoke(false);
				}
			}

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
