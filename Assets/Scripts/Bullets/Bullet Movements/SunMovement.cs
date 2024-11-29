using UnityEngine;

public class SunMovement : MonoBehaviour
{

    #region Attribute Declaration

    #region Component Attributes
    private Rigidbody _sunRB;
    #endregion

    #region Movement Attributes
    [SerializeField] private float _gravityMultiplier;

    private Vector3 _direction;
    private float _horizontalDistance, _verticalDistance;
    private float _velocity;
    private float _customGravity;
    private float _time;
    #endregion

    #endregion

    #region Method Definition

    #region Native Methods
    private void Awake()
    {
        PlayerFire.OnSuntMovementEvent.AddListener(HandleMovement);
        _sunRB = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _sunRB.AddForce(Vector3.up * _customGravity, ForceMode.Acceleration);
    }
    #endregion

    #region User-Definged Methods

    #region UDM (Event Handler)
    private void HandleMovement(string sunName, Vector3 targetPos)
    {
        if (name == sunName)
        {
            _customGravity = Physics.gravity.y * _gravityMultiplier;
            _velocity = 0f;
            _sunRB.velocity = Vector3.zero;
            Move(targetPos);
        }
    }
    #endregion

    #region UDM (Movement)
    private void Move(Vector3 targetPos)
    {
        _direction = PhysicsCalculationManager.instance.GetDirection(transform.position, targetPos);

        _horizontalDistance = PhysicsCalculationManager.instance.GetMagnitude(new Vector3(
            _direction.x,
            0f,
            _direction.z));

        _verticalDistance = _direction.y;

        _time = Mathf.Sqrt(2 * Mathf.Abs(_verticalDistance) / Mathf.Abs(_customGravity));

        _velocity = _horizontalDistance / _time;

        _sunRB.velocity = transform.forward * _velocity; // Horizontal Projectile Motion
    }
    #endregion

    #endregion

    #endregion

}