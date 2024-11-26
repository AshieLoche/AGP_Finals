using System.Collections.Generic;
using UnityEngine;

public class PeashooterPool : MonoBehaviour
{

    public static PeashooterPool instance;

    [SerializeField] private GameObject _peashooterPrefab;

    private List<GameObject> _peashooterPool = new();
    private int poolAmount = 5;
    private GameObject _peashooterClone;

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
            _peashooterClone = Instantiate(_peashooterPrefab, transform);
            //_peashooterClone.SetActive(false);
            _peashooterClone.transform.position = new Vector3(Random.Range(-1170f, 1170f), 140f, Random.Range(-1160f, 1160f));
            _peashooterClone.name = $"Peashooter {i}";
            _peashooterPool.Add(_peashooterClone);
        }
    }

    public GameObject GetPeashooter()
    {
        for (int i = 0; i < _peashooterPool.Count; i++)
        {
            if (!_peashooterPool[i].activeInHierarchy)
            {
                return _peashooterPool[i];
            }
        }

        return null;
    }

}