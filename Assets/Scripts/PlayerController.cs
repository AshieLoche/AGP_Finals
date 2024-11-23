using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float _moveDistancce;
    [SerializeField] private float _moveTime;

    private Rigidbody _playerRB;
    private Vector2 _moveDir;
    private float _moveSpeed;

    private void Awake()
    {
        InputReader.OnMoveEvent.AddListener(HandleMove);
    }

    private void Start()
    {
        _playerRB = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void HandleMove(Vector2 dir)
    {
        _moveDir = dir;
    }

    private void Move()
    {
        _moveSpeed = _moveDistancce / _moveTime;
        _playerRB.velocity = new Vector3(_moveDir.x, 0f, _moveDir.y) * _moveSpeed;
    }

}