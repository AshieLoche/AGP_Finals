using System.Collections;
using UnityEngine;

public class SunCollision : MonoBehaviour
{

    private Transform _target;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sensor"))
        {
            _target = other.transform.parent;

            if(name.Split(" ")[1] == _target.name.Split(" ")[1])
            {
                StartCoroutine(Deactivate());
            }
        }
    }

    private IEnumerator Deactivate()
    {
        _target.GetChild(2).GetComponent<SphereCollider>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        _target.GetChild(2).GetComponent<SphereCollider>().enabled = false;
        _target.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

}