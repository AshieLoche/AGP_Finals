using UnityEngine;

public class SunMovement : MonoBehaviour
{

    private Rigidbody _sunRB;
    private Vector3 _targetPos;

    private void Awake()
    {
        PlayerFire.OnFireEvent.AddListener(SetTargetPos);
    }

    private void OnEnable()
    {
        _sunRB = GetComponent<Rigidbody>();

        _sunRB.isKinematic = false;
        _sunRB.useGravity = true;
        Vector3 owo = _targetPos - transform.position;
        Vector3 hehe = owo - transform.forward;
        _sunRB.velocity = hehe / 2.5f;
    }

    private void SetTargetPos(Transform target)
    {
        _targetPos = target.position;
    }

}