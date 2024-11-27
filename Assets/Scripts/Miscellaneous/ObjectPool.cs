using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ObjectPool : MonoBehaviour
{

    #region Attribute Declaration

    #region Component Attributes
    [Header("Components")]
    [SerializeField] private GameObject _objectPrefab;

    private List<GameObject> _objectPool = new();
    private GameObject _objectClone;
    private Collider[] _colliders;
    #endregion

    #region Pool Attributes
    private int _poolAmount = 10;
    #endregion

    #region Position Attributes
    private Vector3 _newPosition = Vector3.zero;
    private float _spawnRadius;
    private int _maxAttempts = 100;
    private int _attempts = 0;
    #endregion

    #region Boolean Attributes
    bool _validPosition = false;
    #endregion

    #endregion

    #region Method Definition

    #region Native Method Definition
    protected abstract void Awake();

    protected abstract void Start();
    #endregion

    #region User-Defined Method Definition

    #region UDM (Objects) Method
    protected void SetObjects(string name)
    {
        for (int i = 1; i <= _poolAmount; i++)
        {
            if (name != "Sun" && name != "Target")
            {
                _validPosition = false;
                _attempts = 0;

                while (!_validPosition && _attempts < _maxAttempts)
                {
                    _attempts++;

                    _newPosition = new Vector3(Random.Range(-1170f, 1170f), 140f, Random.Range(-1160f, 1160f));
                    _spawnRadius = Random.Range(75, 150);
                    _colliders = Physics.OverlapSphere(_newPosition, _spawnRadius);

                    _validPosition = !_colliders.Any(collider => collider.CompareTag("Buffer"));
                }

                if (!_validPosition)
                {
                    continue;
                }

            }

            _objectClone = Instantiate(_objectPrefab, _newPosition, Quaternion.identity, transform);
            _objectClone.SetActive(false);
            _objectClone.name = $"{name} {i}";
            if (name == "Peashooter" ||
                name == "Snow Pea" ||
                name == "Squash")
            {
                _objectClone.GetComponent<SphereCollider>().radius = _spawnRadius;
            }
            _objectPool.Add(_objectClone);
        }
    }

    public GameObject GetObject()
    {
        for (int i = 0; i < _objectPool.Count; i++)
        {
            if (!_objectPool[i].activeInHierarchy)
            {
                return _objectPool[i];
            }
        }

        return null;
    }
    #endregion

    #endregion

    #endregion

}