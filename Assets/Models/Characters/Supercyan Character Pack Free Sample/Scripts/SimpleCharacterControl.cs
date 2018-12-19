using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;

public class SimpleCharacterControl : MonoBehaviour {

    private enum ControlMode
    {
        Tank,
        Direct
    }

    [SerializeField] private string m_locomotionState = "Movement";
    [SerializeField] private float m_moveSpeed = 2;
    [SerializeField] private float m_turnSpeed = 200;
    [SerializeField] private float m_jumpForce = 4;
    [SerializeField] private float m_dodgeForceAngle = 30.0f;
    [SerializeField] private float m_dodgeCooldown = 1.0f;
    [SerializeField] private float m_dodgeForce = 3;
    [SerializeField] private Animator m_animator;
    [SerializeField] private Rigidbody m_rigidBody;

    [SerializeField] private ControlMode m_controlMode = ControlMode.Direct;

    private float m_currentV = 0;
    private float m_currentH = 0;

    private readonly float m_interpolation = 10;
    private readonly float m_walkScale = 0.33f;
    private readonly float m_backwardsWalkScale = 0.16f;
    private readonly float m_backwardRunScale = 0.66f;

    private bool m_wasGrounded;
    private Vector3 m_currentDirection = Vector3.zero;

    private float m_jumpTimeStamp = 0;
    private float m_minJumpInterval = 0.25f;

    private bool m_disabledMovement;
    private bool m_isWalking;
    private bool m_isGrounded;
    private float m_nextDodgeAvailable;
    private Vector3 m_walkSide;
    private List<Collider> m_collisions = new List<Collider>();

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == GameDefs.PLAYER_LAYER)
        {
            return;
        }

        ContactPoint[] contactPoints = collision.contacts;
        for(int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                if (!m_collisions.Contains(collision.collider)) {
                    m_collisions.Add(collision.collider);
                }
                m_isGrounded = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == GameDefs.PLAYER_LAYER)
        {
            return;
        }

        ContactPoint[] contactPoints = collision.contacts;
        bool validSurfaceNormal = false;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                validSurfaceNormal = true; break;
            }
        }

        if(validSurfaceNormal)
        {
            m_isGrounded = true;
            if (!m_collisions.Contains(collision.collider))
            {
                m_collisions.Add(collision.collider);
            }
        } else
        {
            if (m_collisions.Contains(collision.collider))
            {
                m_collisions.Remove(collision.collider);
            }
            if (m_collisions.Count == 0) { m_isGrounded = false; }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == GameDefs.PLAYER_LAYER)
        {
            return;
        }

        if (m_collisions.Contains(collision.collider))
        {
            m_collisions.Remove(collision.collider);
        }
        if (m_collisions.Count == 0) { m_isGrounded = false; }
    }

    private void PerformJump()
    {
        m_jumpTimeStamp = Time.time;

        Player player = GetComponent<Player>();
        if (player && player.IsRevealed())
        {
            if (Time.time < m_nextDodgeAvailable)
            {
                return;
            }

            m_nextDodgeAvailable = Time.time + m_dodgeCooldown;
            Vector3 dodgeVector = Vector3.RotateTowards(m_walkSide, Vector3.up, m_dodgeForceAngle * Mathf.Deg2Rad, 0.0f);
            m_rigidBody.AddForce(dodgeVector * m_dodgeForce, ForceMode.Impulse);
            return;
        }
        m_rigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
    }

    private void Start()
    {
        m_nextDodgeAvailable = 0.0f;
    }

    void Update () {
        m_animator.SetBool("Grounded", m_isGrounded);

        if (!m_disabledMovement)
        {         
            switch (m_controlMode)
            {
                case ControlMode.Direct:
                    DirectUpdate();
                    break;

                case ControlMode.Tank:
                    TankUpdate();
                    break;

                default:
                    Debug.LogError("Unsupported state");
                    break;
            }
        }

        m_wasGrounded = m_isGrounded;
    }

    private void TankUpdate()
    {
        UpdateLookAt();

        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        if (v == 0.0f && h == 0.0f)
        {
            m_animator.SetFloat("MoveSpeed", 0.0f);
            return;
        }

        m_isWalking = Input.GetKey(KeyCode.LeftShift);

        if (v < 0) {
            if (m_isWalking) { v *= m_backwardsWalkScale; }
            else { v *= m_backwardRunScale; }
        } else if(m_isWalking)
        {
            v *= m_walkScale;
            h *= m_walkScale;
        }

        m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
        m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

        float sumDirectionValue = (Mathf.Abs(m_currentV) + Mathf.Abs(m_currentH));
        float currentSpeedValue = Mathf.Sqrt(m_moveSpeed * m_moveSpeed / sumDirectionValue);

        transform.position += Vector3.forward * m_currentV * currentSpeedValue * Time.deltaTime;
        transform.position += Vector3.right * m_currentH * currentSpeedValue * Time.deltaTime;

        m_walkSide = Vector3.forward * m_currentV + Vector3.right * m_currentH;
        m_walkSide.Normalize();
        float angleToPlayer = Vector3.Angle(m_walkSide, transform.forward); 

        float moveSide = Mathf.Max(Mathf.Abs(m_currentV), Mathf.Abs(m_currentH));
        if (angleToPlayer > 90.0f)
        {
            moveSide *= -1;
        }

        m_animator.SetFloat("MoveSpeed", moveSide);

        JumpingAndLanding();
    }

    public void SetDisabledMovement(bool disable)
    {
        m_disabledMovement = disable;
        if (disable)
        {
            m_animator.SetFloat("MoveSpeed", 0.0f);
        }
    }

    public bool IsGrounded()
    {
        return m_isGrounded;
    }

    public bool IsWalking()
    {
        return m_isWalking;
    }

    private void DirectUpdate()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        Transform camera = Camera.main.transform;

        m_isWalking = Input.GetKey(KeyCode.LeftShift);
        if (m_isWalking)
        {
            v *= m_walkScale;
            h *= m_walkScale;
        }

        m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
        m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

        Vector3 direction = camera.forward * m_currentV + camera.right * m_currentH;

        float directionLength = direction.magnitude;
        direction.y = 0;
        direction = direction.normalized * directionLength;

        if(direction != Vector3.zero)
        {
            m_currentDirection = Vector3.Slerp(m_currentDirection, direction, Time.deltaTime * m_interpolation);

            transform.rotation = Quaternion.LookRotation(m_currentDirection);
            transform.position += m_currentDirection * m_moveSpeed * Time.deltaTime;

            m_animator.SetFloat("MoveSpeed", direction.magnitude);
        }

        JumpingAndLanding();
    }

    public void PickUp()
    {
       // m_animator.SetBool("pickup", true);
        m_animator.SetTrigger("Pickup");
    }

    private void UpdateLookAt()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -(transform.position.x - Camera.main.transform.position.x);
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
        float angle = -Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg + 90.0f;
        transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
    }

    private void JumpingAndLanding()
    {
        bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval;

        if (jumpCooldownOver && m_isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            PerformJump();
        }

        if (!m_wasGrounded && m_isGrounded)
        {
            m_animator.SetTrigger("Land");
        }

        if (!m_isGrounded && m_wasGrounded)
        {
            m_animator.SetTrigger("Jump");
        }
    }
}
