using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    #region Attribute Declaration

    #region Component Attributes
    private Rigidbody _playerRB;
    #endregion

    #region Directional Movement Attributes
    [Header("Directional Movement")]
    [SerializeField] private float _moveDistancce;
    [SerializeField] private float _moveTime;

    private Vector2 _moveDir;
    private float _moveSpeed;
    #endregion

    #region Rotational Movement Attributes
    [Header("Rotational Movement")]
    [SerializeField] private float _rotationAngle;
    [SerializeField] private float _rotationSmoothness;

    private Vector2 _rotationDir;
    private Quaternion _rotationTarget;
    #endregion

    #endregion

    #region Method Definition

    #region Native Method Definition
    private void Awake()
    {
        InputReader.OnMoveEvent.AddListener(HandleMove);
        InputReader.OnRotateEvent.AddListener(HandleRotation);
    }

    private void Start()
    {
        Cursor.visible = false;
        _playerRB = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }
    #endregion

    #region User-Defined Method Definition

    #region UDM (Event Handler) Definition
    private void HandleMove(Vector2 dir)
    {
        _moveDir = dir;
    }

    private void HandleRotation(Vector2 dir)
    {
        _rotationDir = dir;
    }
    #endregion

    #region UDM (Movement) Definition
    private void Move()
    {
        _moveSpeed = _moveDistancce / _moveTime;
        _playerRB.velocity = (transform.right * _moveDir.x + transform.forward * _moveDir.y) * _moveSpeed;
    }

    private void Rotate()
    {
        _rotationTarget = Quaternion.Euler(0f, _playerRB.rotation.eulerAngles.y + (_rotationDir.x * _rotationAngle), 0f);
        _playerRB.rotation = Quaternion.Slerp(_playerRB.rotation, _rotationTarget, Time.deltaTime * _rotationSmoothness);
    }
    #endregion

    #endregion

    #endregion

}