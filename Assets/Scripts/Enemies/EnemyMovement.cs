using System.Linq;
using System.Transactions;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMovement : MonoBehaviour
{

    private Transform _player;
    private Rigidbody _enemyRB;

    private Vector3 _playerPos, _enemyPos;
    private Vector3 _origin, _direction;
    private float _originMagnitude, _directionMagnitude;
    private Vector3 _originNormalized, _directionNormalized;

    private float _dotProductNormalized;

    private Vector3 _crossProduct;
    private float _crossProductMagnitude;
    private Vector3 _crossProductNormalized;

    [SerializeField] private float _sightLength;

    [SerializeField] private float _rotAngle;
    [SerializeField] private float _rotSmoothness;
    private float _rotDir, _rotSpeed;
    private Quaternion _rotTarget;

    private RaycastHit[] _hits;

    public static UnityEvent<string> OnSeePlayerEvent = new();

    private void Start()
    {
        _enemyRB = GetComponent<Rigidbody>();
        _player = SunflowerPool.instance.GetObject("Player").transform;
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        if (InPeripheral() && InSight())
        {
            //Debug.Log("Drawing Ray");
            Debug.DrawRay(_enemyPos, _directionNormalized * _sightLength, Color.red, 0.25f);
            _crossProduct = PhysicsCalculationManager.instance.GetCrossProduct(_origin, _direction);

            _crossProductMagnitude = PhysicsCalculationManager.instance.GetMagnitude(_crossProduct);

            _crossProductNormalized = PhysicsCalculationManager.instance.GetNormalized(_crossProduct, _crossProductMagnitude);

            _rotDir = Mathf.Floor(_crossProductNormalized.y * 100) / 100;

            _rotSpeed = _enemyRB.rotation.eulerAngles.y + 
                ((Mathf.Abs(_rotDir) > 0.9f) ? _rotDir : 0f) * 
                _rotAngle;

            _rotTarget = Quaternion.Euler(0f, _rotSpeed, 0f);

            _enemyRB.rotation = Quaternion.Slerp(_enemyRB.rotation, _rotTarget, Time.deltaTime * _rotSmoothness);

            OnSeePlayerEvent.Invoke(name);
        }
        else
        {
            _enemyRB.rotation = _enemyRB.rotation;
        }
    }

    private bool InPeripheral()
    {
        _playerPos = _player.localPosition;
        _enemyPos = _enemyRB.transform.localPosition;
        _origin = transform.forward;
        _direction = _playerPos - _enemyPos;

        _originMagnitude = PhysicsCalculationManager.instance.GetMagnitude(_origin);
        _directionMagnitude = PhysicsCalculationManager.instance.GetMagnitude(_direction);

        _originNormalized = PhysicsCalculationManager.instance.GetNormalized(_origin, _originMagnitude);
        _directionNormalized = PhysicsCalculationManager.instance.GetNormalized(_direction, _directionMagnitude);

        _dotProductNormalized = PhysicsCalculationManager.instance.GetDotProduct(_originNormalized, _directionNormalized);

        return Mathf.Floor(_dotProductNormalized * 100) / 100 > 0f;

    }

    private bool InSight()
    {
        _hits = Physics.RaycastAll(_enemyPos, _direction, _sightLength, LayerMask.GetMask("Player", "Enemy", "Bullet", "Terrain"));

        if (_hits.Length >= 2)
            return LayerMask.LayerToName(_hits[1].collider.gameObject.layer) == "Player";

        return false;
    }

}