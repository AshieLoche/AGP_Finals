using UnityEngine;

public class SunMovement : MonoBehaviour
{

    #region Attribute Declaration

    #region Component Attributes
    private Rigidbody _sunRB;
    #endregion

    #region Movement Attributes
    private Vector3 _distance;
    private float _horizontalDistance, _verticalDistance;
    private float _velocity;

    [SerializeField] private float _gravityMultiplier;
    private float _customGravity;
    private float _time;
    #endregion

    #region Boolean Attributes
    private bool _isFiring = false;
    #endregion

    #endregion

    #region Method Definition

    #region Native Method Definition
    private void Awake()
    {
        PlayerFire.OnSuntMovementEvent.AddListener(HandleMovement);
    }
    
    private void OnEnable()
    {
        _sunRB = GetComponent<Rigidbody>();
        _sunRB.isKinematic = false;
        _sunRB.useGravity = true;

        _customGravity = Physics.gravity.y * _gravityMultiplier;
    }

    private void FixedUpdate()
    {
        if (_isFiring)
            _sunRB.AddForce(Vector3.up * _customGravity, ForceMode.Acceleration);
    }

    private void OnDisable()
    {
        _isFiring = false;
        _sunRB.useGravity = false;
        _sunRB.velocity = Vector3.zero;
        _sunRB.isKinematic = true;
    }
    #endregion

    #region User-Definged Method Definition

    #region UDM (Event Handler) Definition
    private void HandleMovement(string sunName, Vector3 targetPos)
    {
        if (!_isFiring)
        {
            if (name == sunName)
            {
                Move(targetPos);
                _isFiring = true;
            }
        }
    }
    #endregion

    #region UDM (Movement) Definition
    private void Move(Vector3 targetPos)
    {
        _distance = PhysicsCalculationManager.instance.GetDirection(transform.position, targetPos);

        _horizontalDistance = PhysicsCalculationManager.instance.GetMagnitude(new Vector3(
            _distance.x,
            0f,
            _distance.z));

        _verticalDistance = _distance.y;

        _time = Mathf.Sqrt(2 * Mathf.Abs(_verticalDistance) / Mathf.Abs(_customGravity));

        _velocity = _horizontalDistance / _time;

        _sunRB.velocity = transform.forward * _velocity;
    }
    #endregion

    #endregion

    #endregion

}