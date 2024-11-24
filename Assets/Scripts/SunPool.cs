using System.Collections.Generic;
using UnityEngine;

public class SunPool : MonoBehaviour
{

    public static SunPool instance;

    [SerializeField] private GameObject _sunPrefab;

    private List<GameObject> _sunPool = new();
    private int poolAmount = 10;
    private GameObject _sunClone;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        for (int i = 0; i < poolAmount; i++)
        {
            _sunClone = Instantiate(_sunPrefab, transform);
            _sunClone.SetActive(false);
            _sunPool.Add(_sunClone);
        }
    }

    public GameObject GetSun()
    {
        for (int i = 0; i < _sunPool.Count; i++)
        {
            if (!_sunPool[i].activeInHierarchy)
            {
                return _sunPool[i];
            }
        }

        return null;
    }

}