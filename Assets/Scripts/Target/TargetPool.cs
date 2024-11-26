using System.Collections.Generic;
using UnityEngine;

public class TargetPool : MonoBehaviour
{

    public static TargetPool instance;

    [SerializeField] private GameObject _aimPrefab;

    private List<GameObject> _targetPool = new();
    private int poolAmount = 10;
    private GameObject _targetClone;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        for (int i = 1; i <= poolAmount; i++)
        {
            _targetClone = Instantiate(_aimPrefab, transform);
            _targetClone.SetActive(false);
            _targetClone.name = $"Target {i}";
            _targetPool.Add(_targetClone);
        }
    }

    public GameObject GetTarget()
    {
        for (int i = 0; i < _targetPool.Count; i++)
        {
            if (!_targetPool[i].activeInHierarchy)
            {
                return _targetPool[i];
            }
        }

        return null;
    }

}