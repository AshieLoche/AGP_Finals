using System.Collections;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{

    private GameObject _enemy;

    [SerializeField] private int _maxEnemyCount;
    private int _enemyCount;
    [SerializeField] private int _spawnCount;
    [SerializeField] private int _multiplier;
    [SerializeField] private int _enemyIndex;
    [SerializeField] private float _timer;
    private float _delay = 2.5f;

    private bool _isSpawning;

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

}