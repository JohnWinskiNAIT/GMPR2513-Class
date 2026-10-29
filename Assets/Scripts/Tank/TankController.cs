using System;
using Unity.Cinemachine;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankController : MonoBehaviour, IDamageable
{
    [SerializeField] TankInputReader tankInputReader;
    [SerializeField] TankData tankData;
    [SerializeField] GameObject turret, cannon;
    [SerializeField] float raycastDistance = 3.0f, upperRotation = 350, lowerRotation = 10;

    [SerializeField] CinemachineCamera followCam;

    IInteractable interactableObject;

    LayerMask mask;
    
    public enum MovementState
    {
        None,
        Player,
        Tank
    }

    Rigidbody rb;
    Vector2 movementValue, turretMoveValue;

    public MovementState movementState;

    [SerializeField] GameObject tankDriver;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mask = LayerMask.GetMask("Interactable") | LayerMask.GetMask("Tank");
    }

    private void Start()
    {
        tankData.currentHealth = tankData.maxHealth;
    }

    private void OnEnable()
    {
        tankInputReader.MoveEvent += MoveTank;
        tankInputReader.TurretMoveEvent += MoveTurret;
        tankInputReader.FireEvent += FireWeapon;
    }

    private void OnDisable()
    {
        tankInputReader.MoveEvent -= MoveTank;
        tankInputReader.TurretMoveEvent -= MoveTurret;
        tankInputReader.FireEvent -= FireWeapon;
    }

    void MoveTank(Vector2 movement)
    {
        movementValue = movement;
    }

    void MoveTurret(Vector2 movement)
    {
        turretMoveValue = movement;
    }

    void FireWeapon()
    {
        BroadcastMessage("Fire", SendMessageOptions.DontRequireReceiver);
    }

    private void Update()
    {
        switch (movementState)
        {
            case MovementState.Player:
                if (Physics.Raycast(cannon.transform.position, cannon.transform.forward, out RaycastHit hitInfo, raycastDistance, mask))
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

                    if (interactableObject == null)
                    {
                        if (hitInfo.transform.TryGetComponent(out interactableObject))
                        {
                            Debug.Log("I see a interactable object");
                            
                        }
                    }
                    else
                    {
                        if (Keyboard.current.eKey.wasPressedThisFrame)
                        {
                            interactableObject.Interact();
                        }
                    }
                }
                else
                {
                    interactableObject = null;
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

            if (cannon != null)
            {
                cannon.transform.Rotate(-Vector3.right, turretMoveValue.y * Time.fixedDeltaTime * tankData.turretRotationSpeed, Space.Self);

                Vector3 angles = cannon.transform.localEulerAngles;

                if (angles.x > upperRotation && angles.x < 180.0f)
                {
                    cannon.transform.localRotation = Quaternion.Euler(new Vector3(upperRotation, 0, 0));
                }
                if (angles.x < lowerRotation && angles.x > 180.0f)
                {
                    cannon.transform.localRotation = Quaternion.Euler(new Vector3(lowerRotation, 0, 0));
                }
            }

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

    public void Damage(int damage)
    {
        tankData.currentHealth -= damage;
    }

    public void Heal(int damage)
    {

    }
    public int GetHealth()
    {
        return tankData.currentHealth;
    }

    public int GetMaxHealth()
    {
        return tankData.maxHealth;
    }
}
