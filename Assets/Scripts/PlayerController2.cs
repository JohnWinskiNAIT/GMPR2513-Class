using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController2 : MonoBehaviour
{
    [SerializeField] private InputReader2 inputReader;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] GameObject firstSelected;

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
        //inputReader.MoveForward += MoveForward;
    }

    private void OnDisable()
    {
        inputReader.MoveEvent -= MovePlayer;
        inputReader.JumpEvent -= JumpPlayer;
    }

    void MovePlayer(Vector2 movement)
    {
        moveInput = movement;
        //EventSystem.current.SetSelectedGameObject(null);
        //EventSystem.current.SetSelectedGameObject(firstSelected);
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
