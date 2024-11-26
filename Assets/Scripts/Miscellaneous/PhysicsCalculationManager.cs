using UnityEngine;

public class PhysicsCalculationManager : MonoBehaviour
{

    public static PhysicsCalculationManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public Vector3 GetDirection(Vector3 origin, Vector3 target)
    {
        return target - origin;
    }

    public float GetMagnitude(Vector3 vector)
    {
        return Mathf.Sqrt(
            (vector.x * vector.x) +
            (vector.y * vector.y) +
            (vector.z * vector.z));
    }

}