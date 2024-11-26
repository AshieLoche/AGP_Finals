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
                _target.gameObject.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }

}