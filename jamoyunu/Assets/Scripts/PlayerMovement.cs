using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.5f;
    public float jumpForce = 7f;
    public Transform groundCheck;
    public float groundDistance = 0.3f;
    public LayerMask groundMask;

    private Rigidbody rb;
    private bool isGrounded;
    private Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Karakterin devrilmesini engelle
        rb.freezeRotation = true;
    }

    void Update()
    {
        // WASD hareket girdileri (YATAY + DİKEY)
        float moveX = Input.GetAxisRaw("Horizontal");  // A/D → -1 / +1
        float moveZ = Input.GetAxisRaw("Vertical");    // W/S → +1 / -1
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 7f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = 5.5f;
        }
      


        moveDirection = new Vector3(moveX, 0f, moveZ).normalized;

        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z); 
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        Vector3 move = transform.TransformDirection(moveDirection) * moveSpeed;
        rb.MovePosition(rb.position + move * Time.fixedDeltaTime);
    }
}
