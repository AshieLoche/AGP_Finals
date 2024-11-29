using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ObjectPool : MonoBehaviour
{

    #region Attribute Declaration

    #region Component Attributes
    [Header("Components")]
    [SerializeField] private GameObject _objectPrefab;

    private readonly List<GameObject> _objectPool = new();
    private List<GameObject> _inactivePool = new();
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
        for (int i = 1; i <= _poolAmount; i++)
        {
            _objectClone = Instantiate(_objectPrefab, transform);
            _objectClone.transform.rotation = Quaternion.Euler(0f, Random.Range(0, 360), 0f);
            _objectClone.SetActive(name == "Sunflower");
            _objectClone.name = (name == "Sunflower") ? name : $"{name} {i}";
            if (name == "Peashooter" || name == "Snow Pea" || name == "Squash")
            {
                _objectClone.GetComponent<SphereCollider>().radius = Random.Range(75, 150);
            }
            _objectPool.Add(_objectClone);
        }
    }
    #endregion

    #region UDM (Object)

    public GameObject GetObject(string name)
    {
        if (name == "Player")
        {
            return _objectPool.Last();
        }
        else if (name == "Enemy")
        {
            _inactivePool = _objectPool.Where(obj => !obj.activeInHierarchy).ToList();

            return _inactivePool[Random.Range(0, _inactivePool.Count)];
        }
        else if (name == "Bullet")
        {
             return _objectPool.FirstOrDefault(obj => !obj.activeInHierarchy);
        }

        return null;
    }
    #endregion

    #endregion

    #endregion

}