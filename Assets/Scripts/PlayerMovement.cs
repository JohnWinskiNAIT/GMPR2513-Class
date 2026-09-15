using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    float movementX, movementY;
    [SerializeField] MovementData moveData;
    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Application.Quit();
        }        
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddRelativeForce(movement * moveData.speed);
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void OnLook(InputValue lookValue)
    {
        Vector2 lookVector = lookValue.Get<Vector2>();

        transform.Rotate(Vector3.up, lookVector.x * moveData.rotationSpeed * Time.deltaTime);
    }

    private void OnFire(InputValue fireValue)
    {
        Debug.Log("Fire");
    }

    private void OnFirePrimary(InputValue fireValue)
    {
        Debug.Log("FirePrimary");
    }
}
