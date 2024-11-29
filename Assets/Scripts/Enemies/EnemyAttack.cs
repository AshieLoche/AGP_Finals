using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    private Transform _bullet, _bulletSpawnMarker;
    private Rigidbody _bulletRB;
    [SerializeField] private float _delay;
    private bool _isFiring = false;

    private readonly Dictionary<string, Func<Transform>> _bulletPools = new()
    {
        { "Peashooter", () => PeaPool.instance.GetObject("Bullet").transform },
        { "Snow Pea", () => FrozenPeaPool.instance.GetObject("Bullet").transform }
    };

    private void Awake()
    {
        EnemyMovement.OnSeePlayerEvent.AddListener(HandleSeePlayer);
    }

    private void Start()
    {
        _bulletSpawnMarker = GetComponentsInChildren<Transform>().FirstOrDefault(child => child.name == "Bullet Spawn Marker");
    }

    private void HandleSeePlayer()
    {
        StartCoroutine(IFireBullet());
    }

    private IEnumerator IFireBullet()
    {
        if (!_isFiring)
        {
            _isFiring = true;

            _bullet = _bulletPools.FirstOrDefault(kvp => name.Contains(kvp.Key)).Value?.Invoke();

            _bullet.position = _bulletSpawnMarker.position;

            _bullet.gameObject.SetActive(true);

            yield return new WaitForSeconds(_delay);

            _isFiring = false;
        }
    }

}