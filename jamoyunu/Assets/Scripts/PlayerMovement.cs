using UnityEngine;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour
{
    public AudioClip[] coinSounds;         // Ses seçenekleri
    public AudioSource audioSource;        // Ses çalıcı

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
        rb.freezeRotation = true;
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.LeftShift)) moveSpeed = 7f;
        if (Input.GetKeyUp(KeyCode.LeftShift)) moveSpeed = 5.5f;

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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            ScoreManager.instance.AddPoint();
            PlayRandomCoinSound();
        }
    }

    void PlayRandomCoinSound()
    {
        if (coinSounds.Length == 0 || audioSource == null) return;

        // Rastgele bir ses seç
        int index = Random.Range(0, coinSounds.Length);

        // Hafifçe rastgele ton (pitch) ve ses seviyesi (volume) ayarla
        audioSource.pitch = Random.Range(0.95f, 1.05f);   // daha ince veya kalın hafifçe
        audioSource.volume = Random.Range(0.9f, 1.0f);    // biraz daha sessiz olabilir

        audioSource.PlayOneShot(coinSounds[index]);
    }
}
