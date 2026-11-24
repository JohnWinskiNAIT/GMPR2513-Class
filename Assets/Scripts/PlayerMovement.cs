using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    float movementX, movementY;
    [SerializeField] MovementData moveData;
    Rigidbody rb;

    [SerializeField] AudioMixer myMixer;

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
        
        if (Keyboard.current.equalsKey.wasPressedThisFrame)
        {
            Debug.Log("increase volume");
            float currentVolume;
            myMixer.GetFloat("AmbientVolume", out currentVolume);
            Debug.Log(currentVolume);
            myMixer.SetFloat("AmbientVolume", ++currentVolume);
        }

        if (Keyboard.current.minusKey.wasPressedThisFrame)
        {
            float currentVolume;
            myMixer.GetFloat("AmbientVolume", out currentVolume);
            myMixer.SetFloat("AmbientVolume", --currentVolume);
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
