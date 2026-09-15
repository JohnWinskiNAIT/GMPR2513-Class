using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    [SerializeField] private InputReader2 inputReader;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;

    Rigidbody rb;
    Vector2 moveInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        inputReader.MoveEvent += MovePlayer;
        inputReader.JumpEvent += JumpPlayer;
    }

    private void OnDisable()
    {
        inputReader.MoveEvent -= MovePlayer;
        inputReader.JumpEvent -= JumpPlayer;
    }

    void MovePlayer(Vector2 movement)
    {
        moveInput = movement;
    }

    void JumpPlayer()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        if (moveInput.x != 0 ||  moveInput.y != 0)
        {
            rb.linearVelocity = new Vector3(moveInput.x * moveSpeed * Time.fixedDeltaTime, rb.linearVelocity.y, moveInput.y * moveSpeed * Time.fixedDeltaTime);
        }        
    }
}
