using UnityEngine;

[RequireComponent (typeof(InputReader))]
public class PlayerController : MonoBehaviour
{
    private InputReader input;
    [SerializeField] float moveSpeed, jumpForce;
    private Rigidbody rb;

    private void Awake()
    {
        input = GetComponent<InputReader>();
        rb = GetComponent<Rigidbody>();
    }
    
    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(input.MoveInput.x, 0, input.MoveInput.y);
        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);

        if (input.JumpPressed && Mathf.Abs(rb.linearVelocity.y) < 0.01f)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
