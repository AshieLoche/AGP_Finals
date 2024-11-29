using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sunflower"))
        {
            Camera.main.transform.parent = null;
            other.transform.parent.parent.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }

}
