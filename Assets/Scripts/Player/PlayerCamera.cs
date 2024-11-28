using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    private void Awake()
    {
        Camera.main.transform.parent = transform;
    }

}