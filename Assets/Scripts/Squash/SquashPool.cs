using System.Collections.Generic;
using UnityEngine;

public class SquashPool : MonoBehaviour
{

    public static SquashPool instance;

    [SerializeField] private GameObject _squashPrefab;

    private List<GameObject> _squashPool = new();
    private int poolAmount = 5;
    private GameObject _squashClone;

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
            _squashClone = Instantiate(_squashPrefab, transform);
            //_squashClone.SetActive(false);
            _squashClone.transform.position = new Vector3(Random.Range(-1170f, 1170f), 140f, Random.Range(-1160f, 1160f));
            _squashClone.name = $"squash {i}";
            _squashPool.Add(_squashClone);
        }
    }

    public GameObject Getsquash()
    {
        for (int i = 0; i < _squashPool.Count; i++)
        {
            if (!_squashPool[i].activeInHierarchy)
            {
                return _squashPool[i];
            }
        }

        return null;
    }

}