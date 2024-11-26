using System.Collections.Generic;
using UnityEngine;

public class SnowPeaPool : MonoBehaviour
{

    public static SnowPeaPool instance;

    [SerializeField] private GameObject _snowPeaPrefab;

    private List<GameObject> _snowPeaPool = new();
    private int poolAmount = 5;
    private GameObject _snowPeaClone;

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
            _snowPeaClone = Instantiate(_snowPeaPrefab, transform);
            //_snowPeaClone.SetActive(false);
            _snowPeaClone.transform.position = new Vector3(Random.Range(-1170f, 1170f), 140f, Random.Range(-1160f, 1160f));
            _snowPeaClone.name = $"snowPea {i}";
            _snowPeaPool.Add(_snowPeaClone);
        }
    }

    public GameObject GetsnowPea()
    {
        for (int i = 0; i < _snowPeaPool.Count; i++)
        {
            if (!_snowPeaPool[i].activeInHierarchy)
            {
                return _snowPeaPool[i];
            }
        }

        return null;
    }

}