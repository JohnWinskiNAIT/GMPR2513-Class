using UnityEngine;

[CreateAssetMenu(fileName = "TankData", menuName = "Scriptable Objects/TankData")]
public class TankData : ScriptableObject
{
    public float movementSpeed;
    public float rotationSpeed;
    public float turretRotationSpeed;
}
