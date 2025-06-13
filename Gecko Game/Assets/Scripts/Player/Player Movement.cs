using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] [Range(0.1f,8f)] float m_walkSpeed;



    Rigidbody2D m_rigidBody;
    Animator m_animator;
    float m_horizontalMoveValue;
    bool m_isWalking;
    E_PlayerMoveStates m_currentMoveState;
    SpriteRenderer m_spriteRenderer;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        m_rigidBody = this.GetComponent<Rigidbody2D>(); 
        m_animator = this.GetComponent<Animator>();
        m_spriteRenderer = this.GetComponent<SpriteRenderer>();
        Physics2D.gravity = new Vector2(0, -9.81f);
      
    }

    // Update is called once per frame
    void Update()
    {
        // move states for animation
        switch(m_currentMoveState)
        {
            case E_PlayerMoveStates.IDLE:
                m_animator.SetBool("Idle", true);
                break;
            case E_PlayerMoveStates.WALKING:
                m_animator.SetBool("Idle", false);
                break;
        }
        
        

    }

    private void FixedUpdate()
    {
        float _moveForce = m_horizontalMoveValue * m_walkSpeed;
        Vector2 _moveForceVector = new Vector2(_moveForce, 0);
        m_rigidBody.linearVelocity += _moveForceVector;

    }


    public void OnMovementHorizontal(InputAction.CallbackContext _context)
    {
        float _value = _context.ReadValue<float>();


        if(_context.started)
        {

            m_currentMoveState = E_PlayerMoveStates.WALKING;
            
            if(_value < 0)
            {
                m_spriteRenderer.flipX = true;
            }
            else
            {
                m_spriteRenderer.flipX = false;
            }
            m_horizontalMoveValue = _value;
        }

        if(_context.canceled)
        {
            m_currentMoveState = E_PlayerMoveStates.IDLE;
            m_rigidBody.linearVelocityX = m_rigidBody.linearVelocityX / 12;
            m_horizontalMoveValue = 0f;
        }

       
    }
}


public enum E_PlayerMoveStates
{
    IDLE,

    WALKING,

    JUMPING,

}
