using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Transform playerBody;
    public float mouseSensitivity = 100f;

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Fareyi ekranýn ortasýna kilitle
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Yukarý-aþaðý bakmayý sýnýrla

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);       // Kamera yukarý-aþaðý
        playerBody.Rotate(Vector3.up * mouseX);                              // Karakter saða-sola
    }
}
