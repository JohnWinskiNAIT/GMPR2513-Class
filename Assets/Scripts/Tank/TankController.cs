using UnityEngine;

public class TankController : MonoBehaviour
{
    [SerializeField] TankInputReader tankInputReader;
    [SerializeField] TankData tankData;
    [SerializeField] GameObject turret, cannon;

    Rigidbody rb;
    Vector2 movementValue, turretMoveValue;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        tankInputReader.MoveEvent += MoveTank;
        tankInputReader.TurretMoveEvent += MoveTurret;
    }

    private void OnDisable()
    {
        tankInputReader.MoveEvent -= MoveTank;
        tankInputReader.TurretMoveEvent -= MoveTurret;
    }

    void MoveTank(Vector2 movement)
    {
        movementValue = movement;
    }

    void MoveTurret(Vector2 movement)
    {
        turretMoveValue = movement;
    }

    void FixedUpdate()
    {
        if (rb != null)
        {
            rb.AddRelativeForce(Vector3.forward * movementValue.y * Time.fixedDeltaTime * tankData.movementSpeed * rb.mass);
            rb.AddRelativeTorque(Vector3.up * movementValue.x * Time.fixedDeltaTime * tankData.rotationSpeed * rb.mass);
        }        

        if (turret != null)
        {
            turret.transform.Rotate(Vector3.up, turretMoveValue.x * Time.fixedDeltaTime * tankData.turretRotationSpeed, Space.World);
        }
        
        if (cannon != null)
        {
            cannon.transform.Rotate(-Vector3.right, turretMoveValue.y * Time.fixedDeltaTime * tankData.turretRotationSpeed, Space.Self);

            Vector3 angles = cannon.transform.localEulerAngles;

            if (angles.x > 10.0f && angles.x < 180.0f)
            {
                cannon.transform.localRotation = Quaternion.Euler(new Vector3(10, 0, 0));
            }
            if (angles.x < 350.0f && angles.x > 180.0f)
            {
                cannon.transform.localRotation = Quaternion.Euler(new Vector3(350, 0, 0));
            }
        }     
    }
}
