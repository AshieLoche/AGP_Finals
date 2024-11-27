using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterCollision : MonoBehaviour
{

    private Collider[] _colliders;

    #region Position Attributes
    private Vector3 _newPosition = Vector3.zero;
    private float _spawnRadius;
    private int _maxAttempts = 100;
    private int _attempts = 0;
    #endregion

    #region Boolean Attributes
    bool _validPosition = false;
    bool _findingPosition = false;
    #endregion

    private void OnTriggerStay(Collider other)
    {
        if (!_findingPosition)
        {
            while (!_validPosition && _attempts < _maxAttempts)
            {
                _attempts++;

                _newPosition = new Vector3(Random.Range(-1170f, 1170f), 140f, Random.Range(-1160f, 1160f));
                _spawnRadius = Random.Range(75, 150);
                _colliders = Physics.OverlapSphere(_newPosition, _spawnRadius);

                _validPosition = !_colliders.Any(collider => collider.CompareTag("Buffer"));
            }

            if (_validPosition)
            {
                transform.position = _newPosition;
            }

            _findingPosition = false;
        }

        if (other.CompareTag("Indicator"))
        {
            gameObject.SetActive(false);
        }
    }

}