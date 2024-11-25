using UnityEngine;

public class SpawnerManager : MonoBehaviour
{

    [SerializeField] private float gravityMultiplier, multiplier;
    [SerializeField] private Transform targetTF;
    [SerializeField] private float time;

    private Rigidbody rb;
    private Vector3 CustomGravity;

    private bool stop = false;

    private void Start()
    {

        CustomGravity = new Vector3(
            Physics.gravity.x,
            Physics.gravity.y * gravityMultiplier,
            Physics.gravity.z);

        rb = GetComponent<Rigidbody>();

        Vector3 distance = targetTF.position - transform.position;

        float horizontalDistance = new Vector2(distance.x, distance.z).magnitude;

        float verticalDistance = distance.y;

        time = Mathf.Sqrt(2 * Mathf.Abs(verticalDistance) / Mathf.Abs(CustomGravity.y));

        float horizontalVelocity = horizontalDistance / time;

        rb.velocity = transform.forward * horizontalVelocity;

        Debug.Log($"Horizontal Distance: {horizontalDistance}");

        Debug.Log($"Vertical Distance: {verticalDistance}");

        targetTF.position -= transform.up * targetTF.localScale.y * multiplier;
    }

    private void FixedUpdate()
    {
        if (!stop)
        {
            rb.AddForce(CustomGravity, ForceMode.Acceleration);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sun"))
        {
            stop = true;

            rb.useGravity = false;
            rb.velocity = Vector3.zero;

            Debug.Log($"Horizontal Distance: {targetTF.position.z - transform.position.z}");

            Debug.Log($"Vertical Distance: {targetTF.position.y - transform.position.y + targetTF.localScale.y * multiplier}");

        }
    }

}