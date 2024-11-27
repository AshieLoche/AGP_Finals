using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool : MonoBehaviour
{

    #region Attribute Declaration

    #region Component Attributes
    [Header("Components")]
    [SerializeField] private GameObject _objectPrefab;

    private readonly List<GameObject> _objectPool = new();
    private GameObject _objectClone;
    #endregion

    #region Pool Attributes
    [Header("Pool")]
    [SerializeField] private int _poolAmount;
    #endregion

    #endregion

    #region Method Definition

    #region Native Methods
    protected abstract void Awake();

    protected abstract void Start();
    #endregion

    #region User-Defined Methods

    #region UDM (Set Up)
    protected void SetObjects(string name)
    {
        for (int i = 0; i < _poolAmount;)
        {
            _objectClone = Instantiate(_objectPrefab, transform);
            _objectClone.SetActive(false);
            _objectClone.name = $"{name} {++i}";
            if (name != "Sun" && name != "Target")
            {
                _objectClone.GetComponent<SphereCollider>().radius = Random.Range(75, 150);
            }
            _objectPool.Add(_objectClone);
        }
    }
    #endregion

    #region UDM (Object)
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