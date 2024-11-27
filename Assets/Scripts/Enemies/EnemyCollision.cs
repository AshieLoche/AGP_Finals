using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{

    #region Attribute Declaration

    #region Component Attributes
    private Collider[] _colliders;
    #endregion

    #region Position Attributes
    private Vector3 _newPosition;
    private float _spawnRadius;
    #endregion

    #region Boolean Attributes
    bool _validPosition = false;
    #endregion

    #endregion

    #region Method Definition

    #region Native Methods
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Buffer"))
        {
            _newPosition = new Vector3(Random.Range(-1170f, 1170f), 140f, Random.Range(-1160f, 1160f));
            _spawnRadius = Random.Range(75, 150);
            _colliders = Physics.OverlapSphere(_newPosition, _spawnRadius);

            _validPosition = !_colliders.Any(collider => collider.CompareTag("Buffer"));

            if (_validPosition)
            {
                transform.position = _newPosition;
            }
        }
        else if (other.CompareTag("Indicator"))
        {
            --EnemySpawnerManager.instance.EnemyCount;
            gameObject.SetActive(false);
        }
    }
    #endregion

    #endregion

}