using UnityEngine;

public class SpawnerManager : MonoBehaviour
{

    [SerializeField] private Transform targetTF;
    private Rigidbody rb;

    private void Start()
    {
        Vector3 origin = new Vector3(transform.position.x, 0f, transform.position.z);
        Vector3 target = new Vector3 (targetTF.position.x, 0f, targetTF.position.z);

        Vector3 direction = target - origin;

        rb = GetComponent<Rigidbody>();
        rb.velocity = direction / 2.5f / 2f;
    }

}