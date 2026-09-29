using UnityEngine;

public class TankController : MonoBehaviour
{
    [SerializeField] TankInputReader tankInputReader;
    [SerializeField] TankData tankData;

    Rigidbody rb;
    Vector2 movementValue;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        tankInputReader.MoveEvent += MoveTank;
    }

    private void OnDisable()
    {
        tankInputReader.MoveEvent -= MoveTank;
    }

    void MoveTank(Vector2 movement)
    {
        movementValue = movement;
    }

    void FixedUpdate()
    {
        rb.AddRelativeForce(Vector3.forward * movementValue.y * Time.fixedDeltaTime * tankData.movementSpeed * rb.mass);
        rb.AddRelativeTorque(Vector3.up * movementValue.x * Time.fixedDeltaTime * tankData.rotationSpeed * rb.mass);
    }
}
