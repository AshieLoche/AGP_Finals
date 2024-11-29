using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    #region Attribute Declaration

    #region Component Attributes
    private Rigidbody _playerRB;
    #endregion

    #region Movement Attributes

    #region Directional Movement Attributes
    [Header("Directional Movement")]
    [SerializeField] private float _moveDistancce;
    [SerializeField] private float _moveTime;

    private Vector2 _moveDir;
    private float _moveSpeed;
    #endregion

    #region Rotational Movement Attributes
    [Header("Rotational Movement")]
    [SerializeField] private float _rotAngle;
    [SerializeField] private float _rotSmoothness;

    private Vector2 _rotDir;
    private Quaternion _rotTarget;
    private float _rotSpeed;
    #endregion

    #endregion

    #endregion

    #region Method Definition

    #region Native Methods
    private void Awake()
    {
        InputManager.OnMoveEvent.AddListener(HandleMove);
        InputManager.OnRotateEvent.AddListener(HandleRotate);
    }

    private void Start()
    {
        if (_playerRB == null)
            _playerRB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Cursor.visible = false;
        Rotate();
    }

    private void FixedUpdate()
    {
        Move();
    }
    #endregion

    #region User-Defined Methods

    #region UDM (Event Handler)
    private void HandleMove(Vector2 dir)
    {
        _moveDir = dir;
    }

    private void HandleRotate(Vector2 dir)
    {
        _rotDir = dir;
    }
    #endregion

    #region UDM (Movement)
    private void Move()
    {
        _moveSpeed = _moveDistancce / _moveTime;
        _playerRB.velocity = (transform.right * _moveDir.x + transform.forward * _moveDir.y) * _moveSpeed;
    }

    private void Rotate()
    {
        _rotSpeed = _playerRB.rotation.eulerAngles.y + (_rotDir.x * _rotAngle);
        _rotTarget = Quaternion.Euler(0f, _rotSpeed, 0f);
        _playerRB.rotation = Quaternion.Slerp(_playerRB.rotation, _rotTarget, Time.deltaTime * _rotSmoothness);
    }
    #endregion

    #endregion

    #endregion

}