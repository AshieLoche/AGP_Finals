using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class PlayerAim : MonoBehaviour
{

    #region Attribute Declaration

    #region Component Attributes
    [Header("Components")]
    [SerializeField] private Transform _aim;
    [SerializeField] private Renderer _indicator;

    private Material _indicatorMat;
    #endregion

    #region Aim Attributes
    [Header("Aim Time")]
    [SerializeField] private float _aimTime;

    private float _aimTimer;
    private float _aimProgress;

    [Header("Aim Position")]
    [SerializeField] private float _minPos;
    [SerializeField] private float _maxPos;

    private float _curPos, _pos;

    [Header("Aim Scale")]
    [SerializeField] private float _minScale;
    [SerializeField] private float _maxScale;
    
    private float _curScale, _scale;

    [Header("Aim Rotation")]
    [SerializeField] private float _minRotSpeed;
    [SerializeField] private float _maxRotSpeed;

    private float _curRotSpeed, _rotSpeed;

    [Header("Aim Emission")]
    [SerializeField] private float _minEmissionIntensity;
    [SerializeField] private float _maxEmissionIntensity;

    private float _curEmissionIntensity, _emissionIntensity;
    private Color _emissionColor;
    #endregion

    #region Boolean Attributes
    private bool _isAiming;
    #endregion

    #region Event Attributes
    public static UnityEvent<Vector3, float, float, Color> OnFireEvent = new();
    #endregion

    #endregion

    #region Method Definition

    #region Native Method Definition
    private void Awake()
    {
        InputManager.OnAimEvent.AddListener(HandleAim);
        InputManager.OnAimCancelEvent.AddListener(HandleAimCancel);
        InputManager.OnFireEvent.AddListener(HandleFire);
    }

    private void Start()
    {
        _indicatorMat = _indicator.material;
        _curPos = _minPos;
        _curScale = _curRotSpeed = _curEmissionIntensity = 1f;
    }

    private void FixedUpdate()
    {
        Aim();
    }
    #endregion

    #region User-Defined Method

    #region UDM (Event Handler) Definition
    private void HandleAim()
    {
        SetAim(true);
    }

    private void HandleAimCancel()
    {
        SetAim(false);
    }

    private void HandleFire()
    {
        OnFireEvent.Invoke(_aim.position, _scale, _rotSpeed, _emissionColor);
    }
    #endregion

    #region UDM (Aim) Definition
    private void SetAim(bool status)
    {
        _aimTimer = 0f;
        _aimProgress = 0f;
        _curPos = _pos;
        _curScale = _scale;
        _curRotSpeed = _rotSpeed;
        _curEmissionIntensity = _emissionIntensity;
        _isAiming = status;
    }

    private void Aim()
    {
        _aimTimer += Time.deltaTime;
        _aimProgress = Mathf.Clamp01(_aimTimer / _aimTime);

        _pos = Mathf.Lerp(
            _curPos,
            (_isAiming) ? _maxPos /*450f*/ : _minPos /*150f*/,
            _aimProgress);
        _aim.localPosition = new Vector3(0f, 0.5f, _pos);

        _scale = Mathf.Lerp(
            _curScale,
            (_isAiming) ? _minScale /*0.25f*/ : _maxScale /*1f*/,
            _aimProgress);
        _aim.localScale = Vector3.one * _scale;

        _rotSpeed = Mathf.Lerp(
            _curRotSpeed,
            (_isAiming) ? _maxRotSpeed /*10f*/ : _minRotSpeed /*1f*/,
            _aimProgress);
        _aim.localRotation *= Quaternion.Euler(0f, 0f, _rotSpeed);

        _emissionIntensity = Mathf.Lerp(
            _curEmissionIntensity,
            (_isAiming) ? _maxEmissionIntensity /*8f*/ : _minEmissionIntensity /*1f*/,
            _aimProgress);
        _emissionColor = Color.white * _emissionIntensity;
        _indicatorMat.SetColor("_EmissionColor", _emissionColor);
        _indicatorMat.EnableKeyword("_EMISSION");
    }
    #endregion

    #endregion

    #endregion

}