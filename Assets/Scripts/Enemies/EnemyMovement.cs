using System.Transactions;
using UnityEngine;

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

    private RaycastHit _hit;

    private void Start()
    {
        _enemyRB = GetComponent<Rigidbody>();
        _player = SunflowerPool.instance.GetObject("Player").transform;
    }

    private void FixedUpdate()
    {
        Rotate();
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.green;
        //Gizmos.DrawLine(_enemyPos, _playerPos);

        //Gizmos.color = Color.yellow;
        //Gizmos.DrawLine(_enemyPos + (transform.forward * 47.5f), _direction);
    }

    private void Rotate()
    {
        if (InPeripheral() && !PlayerHidden())
        {
            _crossProduct = PhysicsCalculationManager.instance.GetCrossProduct(_origin, _direction);

            _crossProductMagnitude = PhysicsCalculationManager.instance.GetMagnitude(_crossProduct);

            _crossProductNormalized = PhysicsCalculationManager.instance.GetNormalized(_crossProduct, _crossProductMagnitude);

            _rotDir = Mathf.Floor(_crossProductNormalized.y * 100) / 100;

            _rotSpeed = _enemyRB.rotation.eulerAngles.y + 
                ((Mathf.Abs(_rotDir) > 0.9f) ? _rotDir : 0f) * 
                _rotAngle;

            _rotTarget = Quaternion.Euler(0f, _rotSpeed, 0f);

            _enemyRB.rotation = Quaternion.Slerp(_enemyRB.rotation, _rotTarget, Time.deltaTime * _rotSmoothness);
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

    private bool PlayerHidden()
    {
        //if (Physics.Raycast(_enemyPos, _direction, out RaycastHit hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Player")))
        //{
        //    Debug.Log(LayerMask.LayerToName(hit.collider.gameObject.layer));
        //    Debug.DrawRay(_enemyPos, _direction, Color.red, 1f);
        //    return LayerMask.LayerToName(hit.collider.gameObject.layer) != "Player" &&
        //        LayerMask.LayerToName(hit.collider.gameObject.layer) != "Bullet";

        //}

        _hit = Physics.RaycastAll(_enemyPos, _direction, Mathf.Infinity,
    LayerMask.GetMask("Player", "Enemy", "Terrain", "Bullet"))[0];

        Debug.Log($"First: {LayerMask.LayerToName(_hit.collider.gameObject.layer)}");

        _hit = Physics.RaycastAll(_enemyPos, _direction, Mathf.Infinity,
    LayerMask.GetMask("Player", "Enemy", "Terrain", "Bullet"))[1];

        Debug.Log($"Second: {LayerMask.LayerToName(_hit.collider.gameObject.layer)}");

        _hit = Physics.RaycastAll(_enemyPos, _direction, Mathf.Infinity,
    LayerMask.GetMask("Player", "Enemy", "Terrain", "Bullet"))[1];

        return LayerMask.LayerToName(_hit.collider.gameObject.layer) != "Player" &&  LayerMask.LayerToName(_hit.collider.gameObject.layer) != "Bullet";

    //    if (Physics.RaycastAll(_enemyPos, _direction, Mathf.Infinity,
    //LayerMask.GetMask("Player", "Enemy", "Terrain", "Bullet"))[1].collider)
    //    {
    //        // Check if the hit object is not the origin object
    //        if (hit.collider.gameObject != this.gameObject)
    //        {
    //            Debug.Log(LayerMask.LayerToName(hit.collider.gameObject.layer));
    //            Debug.DrawRay(_enemyPos, _direction, Color.red, 1f);

    //            // Return true if the hit object is not "Player" or "Bullet"
    //            return LayerMask.LayerToName(hit.collider.gameObject.layer) != "Player" &&
    //                   LayerMask.LayerToName(hit.collider.gameObject.layer) != "Bullet";
    //        }
    //    }

        //return false;

        //Physics.spher
    }

}