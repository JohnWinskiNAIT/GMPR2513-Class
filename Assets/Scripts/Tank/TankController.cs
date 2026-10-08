using System;
using Unity.Cinemachine;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankController : MonoBehaviour
{
    [SerializeField] TankInputReader tankInputReader;
    [SerializeField] TankData tankData;
    [SerializeField] GameObject turret, cannon;

    [SerializeField] CinemachineCamera followCam;
    
    public enum MovementState
    {
        None,
        Player,
        Tank
    }

    Rigidbody rb;
    Vector2 movementValue, turretMoveValue;

    public MovementState movementState;

    float raycastDistance = 3.0f;

    [SerializeField] GameObject tankDriver;

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

    private void Update()
    {
        switch (movementState)
        {
            case MovementState.Player:
                if (Physics.Raycast(transform.position + new Vector3(0,0.5f,0), transform.forward, out RaycastHit hitInfo, raycastDistance))
                {
                    if (hitInfo.transform.tag == "Vehicle")
                    {
                        Debug.Log("I see a vehicle");
                        if (Keyboard.current.eKey.wasPressedThisFrame)
                        {
                            followCam.Follow = hitInfo.transform;
                            hitInfo.transform.GetComponent<TankController>().DriveTank(this.gameObject);
                        }
                    }
                }
                break;

            case MovementState.Tank:
                if (Keyboard.current.fKey.wasPressedThisFrame)
                {
                    LeaveTank();
                }
                    break;

            default:
                break;
        }        
    }

    void FixedUpdate()
    {
         switch (movementState)
        {
            case MovementState.Player:
                MovePlayer();
                break;
                
            case MovementState.Tank:
                MoveTank();
                break;

            default:
                break;
        }
    }

    void MoveTank()
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

    void MovePlayer()
    {
        if (rb != null)
        {
            rb.AddRelativeForce(new Vector3(movementValue.x, 0, movementValue.y) * Time.fixedDeltaTime * tankData.movementSpeed * rb.mass);

            rb.AddRelativeTorque(Vector3.up * turretMoveValue.x * Time.fixedDeltaTime * tankData.turretRotationSpeed * rb.mass);
        }
    }

    public void DriveTank(GameObject driver)
    {
        movementState = MovementState.Tank;
        tankDriver = driver;
        driver.SetActive(false);
    }

    void LeaveTank()
    {
        movementState = MovementState.None;
        tankDriver.transform.position = transform.position + (transform.right * 3.0f);
        tankDriver.SetActive(true);
        followCam.Follow = tankDriver.transform;
        tankDriver = null;
    }
}
