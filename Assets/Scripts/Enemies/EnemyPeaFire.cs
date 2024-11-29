using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    private Transform _bullet, _bulletSpawnMarker;
    [SerializeField] private float _reachLength;
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

    private void HandleSeePlayer(string enemyName)
    {
        if (name == enemyName)
            FireBullet();
    }

    private void FireBullet()
    {
        if (InReach())
            StartCoroutine(IFireBullet(1.5f));
    }

    private bool InReach()
    {
        Debug.Log("Drawing Ray");
        Debug.DrawRay(transform.position, transform.forward * _reachLength, Color.blue, 0.25f);

        return Physics.Raycast(transform.position, transform.forward, _reachLength, LayerMask.GetMask("Player"));
    }

    private IEnumerator IFireBullet(float delay)
    {
        if (!_isFiring)
        {
            _isFiring = true;

            _bullet = _bulletPools.FirstOrDefault(kvp => name.Contains(kvp.Key)).Value?.Invoke();

            _bullet.position = _bulletSpawnMarker.position;
            _bullet.rotation = transform.rotation;

            _bullet.gameObject.SetActive(true);

            yield return new WaitForSeconds(delay);

            _isFiring = false;
        }
    }

}