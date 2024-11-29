using System.Linq;
using UnityEngine;

public class EnemySquashAttack : MonoBehaviour
{

    private Rigidbody _squashRB;
    [SerializeField] private float _reachLength;
    private float _horizontalVelocity, _verticalVelocity;
    private Vector3 _squashPos;
    private Vector3 _playerPos;
    private Vector3 _direction;
    private float _horizontalDistance;
    private float _verticalDistance;
    [SerializeField] private float _maxHeight;
    [SerializeField] private float _gravityMultiplier;
    private float _customGravity;
    [SerializeField] private int _launchAngle;
    private Transform _player;

    private void Awake()
    {
        EnemyMovement.OnSeePlayerEvent.AddListener(HandleSeePlayer);
    }

    private void Start()
    {
        _squashRB = GetComponent<Rigidbody>();
        _squashRB.constraints = RigidbodyConstraints.FreezePosition;
        _player = SunflowerPool.instance.GetObject("Player").transform;
    }

    private void HandleSeePlayer(string enemyName)
    {
        if (name == enemyName)
        {
            _customGravity = Physics.gravity.y * _gravityMultiplier;
            _horizontalVelocity = 0f;
            _verticalVelocity = 0f;
            _squashRB.velocity = Vector3.zero;
            Attack();
        }
    }

    private void Attack()
    {
        if (InReach())
        {
            _squashPos = transform.position;
            _playerPos = _player.position;

            _direction = PhysicsCalculationManager.instance.GetDirection(_squashPos, _playerPos);

            _horizontalDistance = PhysicsCalculationManager.instance.GetMagnitude(new Vector3(
                _direction.x,
                0f,
                _direction.z));

            _verticalDistance = _maxHeight;

            _horizontalVelocity = Mathf.Sqrt((_horizontalDistance * _customGravity) / Mathf.Sin(2 * Mathf.Deg2Rad * _launchAngle));

            _verticalVelocity = Mathf.Sqrt(2 * _verticalDistance * _customGravity) / Mathf.Sin(Mathf.Deg2Rad * _launchAngle);

            _squashRB.velocity = transform.forward * _horizontalVelocity + transform.up * _verticalVelocity;
        }
    }

    private bool InReach()
    {
        //Debug.Log("Drawing Ray");
        Debug.DrawRay(transform.position, transform.forward * _reachLength, Color.blue, 0.25f);

        return Physics.Raycast(transform.position, transform.forward, _reachLength, LayerMask.GetMask("Player"));
    }
}