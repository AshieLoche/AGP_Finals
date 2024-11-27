using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{

    private Rigidbody _enemyRB;

    private void Start()
    {
        _enemyRB = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Rotate();
    }

    private void Rotate()
    {

    }

}