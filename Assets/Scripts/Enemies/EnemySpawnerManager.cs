using System.Collections;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{

    #region Attribute Declaration

    #region Singleton Attribute
    public static EnemySpawnerManager instance;
    #endregion

    #region Component Attributes
    private GameObject _enemy;
    #endregion

    #region Spawn Attributes
    private int _maxEnemyCount;
    private int _spawnCount;
    private int _enemyCount;
    public int EnemyCount { get { return _enemyCount; } set { _enemyCount = value; } }
    private int _enemyIndex;
    private int _multiplier;
    private float _timer;
    private readonly float _delay = 2.5f;
    #endregion

    #region Boolean Attributes
    private bool _isSpawning;
    #endregion

    #endregion

    #region Method Definition

    #region Native Methods
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void FixedUpdate()
    {
        _timer += Time.deltaTime;

        _multiplier = Mathf.FloorToInt(_timer / 30f);

        if (_timer < 30f + 30f * _multiplier)
        {
            if (_multiplier < 3)
            {
                _enemyIndex = 1 + _multiplier;
                _maxEnemyCount = 5 + 5 * _multiplier;
                _spawnCount = 1;
            }
            else if (_multiplier >= 3 && _multiplier < 5)
            {
                _spawnCount = 1 + (_multiplier - 2);
            }
        }

        for (int i = 0; i < _spawnCount; i++)
        {
            StartCoroutine(ISpawnEnemy());
        }
    }
    #endregion

    #region User-Defined Methods

    #region UDM (SpawnEnemy)
    private IEnumerator ISpawnEnemy()
    {
        if (!_isSpawning)
        {
            _isSpawning = true;
            SpawnEnemy();
            yield return new WaitForSeconds(_delay);
            _isSpawning = false;
        }
    }

    private void SpawnEnemy()
    {
        if (_maxEnemyCount > _enemyCount)
        {
            switch (Random.Range(0, _enemyIndex))
            {
                case 0:
                    _enemy = PeashooterPool.instance.GetObject();
                    break;
                case 1:
                    _enemy = SnowPeaPool.instance.GetObject();
                    break;
                case 2:
                    _enemy = SquashPool.instance.GetObject();
                    break;
            }

            if (_enemy != null)
                Activate();
            else
                SpawnEnemy();
        }
    }

    private void Activate()
    {
        _enemy.SetActive(true);
        _enemyCount++;
    }
    #endregion

    #endregion

    #endregion

}