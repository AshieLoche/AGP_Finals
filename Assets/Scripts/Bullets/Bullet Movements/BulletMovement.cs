using System.Collections;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{

    private Rigidbody _bulletRB;
    [SerializeField] private float _distance, _time;

    private void Awake()
    {
        _bulletRB = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _bulletRB.velocity = transform.forward * _distance / _time;
        StartCoroutine(IDeactivate(2f));
    }

    private void OnDisable()
    {
        _bulletRB.velocity = Vector3.zero;
    }

    private IEnumerator IDeactivate(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }

}