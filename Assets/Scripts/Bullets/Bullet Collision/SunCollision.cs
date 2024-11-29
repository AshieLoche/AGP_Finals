using System.Collections;
using System.Linq;
using UnityEngine;

public class SunCollision : MonoBehaviour
{

    #region Attribute Declaration

    #region Component Attributes
    private Transform _target;
    private SphereCollider _targetCollider;
    #endregion

    #endregion

    #region Method Definition

    #region Native Methods


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sensor"))
        {
            _target = other.transform;
            Debug.Log(_target.name);
            if (name.Split(" ")[1] == other.name.Split(" ")[1])
            {
                StartCoroutine(Deactivate(0.5f));
            }
        }
    }
    #endregion

    #region User-Defined Methods

    #region UDM (Deactivate)
    private IEnumerator Deactivate(float delay)
    {
        _targetCollider = _target.GetComponentsInChildren<SphereCollider>().FirstOrDefault(collider => collider.name == "Indicator");

        _targetCollider.enabled = true;
        yield return new WaitForSeconds(delay);
        _targetCollider.enabled = false;
        _target.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
    #endregion

    #endregion

    #endregion

}