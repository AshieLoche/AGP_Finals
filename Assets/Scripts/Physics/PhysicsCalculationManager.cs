using UnityEngine;

public class PhysicsCalculationManager : MonoBehaviour
{

    #region Attribute Declaration

    #region Singleton Attribute
    public static PhysicsCalculationManager instance;
    #endregion

    #endregion

    #region Method Definition

    #region Native Methods
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    #region User-Defined Methods

    #region UDM (Physics)
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

    public Vector3 GetNormalized(Vector3 vector, float magnitude)
    {
        return vector / magnitude;
    }

    public float GetDotProduct(Vector3 origin, Vector3 target)
    {
        return (origin.x * target.x) +
            (origin.y * target.y) +
            (origin.z * target.z);
    }

    public Vector3 GetCrossProduct(Vector3 origin, Vector3 target)
    {
        return new Vector3(
                origin.y * target.z - origin.z * target.y,
                origin.z * target.x - origin.x * target.z,
                origin.x * target.y - origin.y * target.x);
    }
    #endregion

    #endregion

    #endregion

}