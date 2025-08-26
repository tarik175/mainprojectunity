using UnityEngine;
using UnityEngine.InputSystem; // NEW input system
// Optional: using UnityEngine.InputSystem.Utilities;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed = 2f;
    [SerializeField] InputActionReference moveAction; // Assign the "Move" action from your Input Actions asset

    Rigidbody2D rb;
    Animator animator;

    Vector2 motionVector;
    public Vector2 lastMotionVector;
    public bool isMoving;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        if (moveAction != null) moveAction.action.Enable();
    }

    void OnDisable()
    {
        if (moveAction != null) moveAction.action.Disable();
    }

    void Update()
    {
        // Read Vector2 from the new Input System
        Vector2 input = moveAction != null ? moveAction.action.ReadValue<Vector2>() : Vector2.zero;

        motionVector = input;
        isMoving = input.sqrMagnitude > 0.0001f;

        // Animator parameters (same names you used)
        if (animator)
        {
            animator.SetFloat("horizontal", input.x);
            animator.SetFloat("vertical",   input.y);
            animator.SetBool("isMoving", isMoving);

            if (isMoving)
            {
                lastMotionVector = input.normalized;
                animator.SetFloat("lastHorizontal", lastMotionVector.x);
                animator.SetFloat("lastVertical",   lastMotionVector.y);
            }
        }
    }

    void FixedUpdate()
    {
        // Unity 6 uses Rigidbody2D.linearVelocity (good), this is frame-rate independent
        rb.linearVelocity = motionVector.normalized * speed;
    }
}
