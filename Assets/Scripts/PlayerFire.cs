using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerFire : MonoBehaviour
{

    [SerializeField] private Transform _sunParent;
    [SerializeField] private Transform _targetParent;
    [SerializeField] private Transform _bullerSpawnMarker;

    private GameObject _sun;
    private Transform _sunTF;
    private Animator _sunAnim;
    private List<Renderer> _sunRenderers;
    private Material _sunMat;

    private GameObject _target;
    private Transform _targetTF;
    private Vector3 _targetPos;
    private float _targetScale;
    private float _targetRotSpeed;
    private Color _targetEmissionColor;

    private bool _isFiring;

    public static UnityEvent<Transform> OnFireEvent = new();

    private void Awake()
    {
        PlayerAim.OnFireEvent.AddListener(HandleFire);
    }

    private void FixedUpdate()
    {
        if (_isFiring)
            _targetTF.localRotation *= Quaternion.Euler(0f, _targetRotSpeed, 0f);
    }

    private void HandleFire(Vector3 position, float scale, float rotSpeed, Color emissionColor)
    {
        _targetPos = position;
        _targetScale = scale;
        _targetRotSpeed = rotSpeed;
        _targetEmissionColor = emissionColor;

        while (_sun == null)
            _sun = SunPool.instance.GetSun();

        _sunTF = _sun.transform;
        _sunTF.position = _bullerSpawnMarker.position;
        _sunTF.localScale = Vector3.one * 4 * _targetScale;
        _sunTF.localRotation = transform.localRotation;
        _sunAnim = _sunTF.GetChild(0).GetComponent<Animator>();
        _sunAnim.speed = _targetRotSpeed;
        _sunRenderers = _sunTF.GetChild(0).GetComponentsInChildren<Renderer>().ToList();
        _sunRenderers.ForEach(sunRenderer =>
        {
            _sunMat = sunRenderer.material;

            _sunMat.SetColor("_EmissionColor", _targetEmissionColor);
            _sunMat.EnableKeyword("_EMISSION");
        });
        _sun.SetActive(true);

        while (_target == null)
            _target = TargetPool.instance.GetTarget();

        _targetTF = _target.transform;
        _targetTF.position = _targetPos;
        _targetTF.localScale = Vector3.one * _targetScale;
        _targetTF.localRotation *= Quaternion.Euler(0f, _targetRotSpeed, 0f);
        _target.SetActive(true);

        _isFiring = true;
        OnFireEvent.Invoke(_targetTF);
    }

}