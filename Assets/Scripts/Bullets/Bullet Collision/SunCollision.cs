using UnityEngine;
using UnityEngine.Events;

public class SunCollision : MonoBehaviour
{

    #region Attribute Declaration

    #region Event Attributes
    public static UnityEvent<string> OnSunCollision = new();
    #endregion

    #endregion

    #region Method Definition

    #region Native Methods
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sensor"))
        {
            OnSunCollision.Invoke(other.name);
            gameObject.SetActive(false);
        }
    }
    #endregion

    #endregion


}