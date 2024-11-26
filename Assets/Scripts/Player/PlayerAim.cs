using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAim : MonoBehaviour
{

    #region Attribute Declaration

    #region Component Attributes
    private Transform _target, _border, _borderFlipped, _indicator;
    private Material _borderMat, _borderFlippedMat, _indicatorMat;
    #endregion

    #region Color Attibutes
    private Color _matColor;
    #endregion

    #region Aim Attributes
    [Header("Aim Time")]
    [SerializeField] private float _aimTime;

    private float _aimTimer;
    private float _aimProgress;

    [Header("Target Position")]
    [SerializeField] private float _minPos;
    [SerializeField] private float _maxPos;

    private float _curPos, _pos;

    [Header("Target Scale")]
    [SerializeField] private float _minScale;
    [SerializeField] private float _maxScale;

    private float _curScale, _scale;

    [Header("Target Rotation")]
    [SerializeField] private float _minRotSpeedBorder;
    [SerializeField] private float _maxRotSpeedBorder;
    [SerializeField] private float _minRotSpeedBorderFlipped;
    [SerializeField] private float _maxRotSpeedBorderFlipped;
    [SerializeField] private float _minRotSpeedIndicator;
    [SerializeField] private float _maxRotSpeedIndicator;

    private float _curRotSpeedBorder, _rotSpeedBorder;
    private float _curRotSpeedBorderFlipped, _rotSpeedBorderFlipped;
    private float _curRotSpeedIndicator, _rotSpeedIndicator;

    [Header("Target Emission")]
    [SerializeField] private float _minEmissionIntensity;
    [SerializeField] private float _maxEmissionIntensity;

    private float _curEmissionIntensity, _emissionIntensity;
    #endregion

    #region Boolean Attributes
    private bool _isAiming;
    private bool _isReturning;
    private bool _isVisible;
    #endregion

    #region Event Attributes
    public static UnityEvent<Vector3, float, List<float>, float> OnFireEvent = new();
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
        SetComponents();
        SetVariables();
        SetVisibility(0f);
    }

    private void FixedUpdate()
    {
        Aim();
        AimReset();
    }
    #endregion

    #region User-Defined Method

    #region UDM (Event Handler) Definition
    private void HandleAim()
    {
        SetVisibility(1f);
        SetAim(true);
    }

    private void HandleAimCancel()
    {
        _isReturning = true;
        SetAim(false);
    }

    private void HandleFire()
    {
        if (_isVisible)
            OnFireEvent.Invoke(
                _target.position,
                _scale,
                new List<float>() { _rotSpeedBorder, _rotSpeedBorderFlipped, _rotSpeedIndicator },
                _emissionIntensity);
    }
    #endregion

    #region UDM (Set Up) Definition
    private void SetAim(bool status)
    {
        _aimTimer = 0f;
        _aimProgress = 0f;
        _curPos = _pos;
        _curScale = _scale;
        _curRotSpeedBorder = _rotSpeedBorder;
        _curRotSpeedBorderFlipped = _rotSpeedBorderFlipped;
        _curRotSpeedIndicator = _rotSpeedIndicator;
        _curEmissionIntensity = _emissionIntensity;
        _isAiming = status;
    }

    private void SetComponents()
    {
        foreach (Transform child in transform.GetComponentsInChildren<Transform>())
        {
            if (child.name == "Target")
            {
                _target = child;

                foreach (Transform grandchild in _target.GetComponentsInChildren<Transform>())
                {
                    if (grandchild.name == "Border")
                    {
                        _border = grandchild;
                        _borderMat = _border.GetComponent<Renderer>().material;
                    }
                    else if (grandchild.name == "Border Flipped")
                    {
                        _borderFlipped = grandchild;
                        _borderFlippedMat = _borderFlipped.GetComponent<Renderer>().material;
                    }
                    else if (grandchild.name == "Indicator")
                    {
                        _indicator = grandchild;
                        _indicatorMat = _indicator.GetComponent<Renderer>().material;
                    }
                }

                break;
            }
        }
    }

    private void SetVariables()
    {
        _curPos = _minPos;
        _curScale = _curRotSpeedBorder = _curRotSpeedBorderFlipped = _curRotSpeedIndicator = _curEmissionIntensity = 1f;
    }

    private void SetVisibility(float visibility)
    {
        _matColor = _borderMat.color;
        _matColor.a = visibility;
        _borderMat.color = _matColor;
        _matColor = _borderFlippedMat.color;
        _matColor.a = visibility;
        _borderFlippedMat.color = _matColor;
        _matColor = _indicatorMat.color;
        _matColor.a = visibility;
        _indicatorMat.color = _matColor;
        _isVisible = visibility == 1;
    }
    #endregion

    #region UDM (Aim) Definition
    private void Aim()
    {

        #region Time
        _aimTimer += Time.deltaTime;
        _aimProgress = Mathf.Clamp01(_aimTimer / _aimTime);
        #endregion

        #region Position
        _pos = Mathf.Lerp(
            _curPos,
            (_isAiming) ? _maxPos /*450f*/ : _minPos /*150f*/,
            _aimProgress);
        _target.localPosition = new Vector3(0f, -77.5f, _pos);
        #endregion

        #region Scale
        _scale = Mathf.Lerp(
            _curScale,
            (_isAiming) ? _minScale /*0.25f*/ : _maxScale /*1f*/,
            _aimProgress);
        _target.localScale = Vector3.one * _scale;
        #endregion

        #region Rotation
        _rotSpeedBorder = Mathf.Lerp(
            _curRotSpeedBorder,
            (_isAiming) ? _maxRotSpeedBorder /*10f*/ : _minRotSpeedBorder /*1f*/,
            _aimProgress);
        _border.localRotation *= Quaternion.Euler(0f, _rotSpeedBorder, 0f);

        _rotSpeedBorderFlipped = Mathf.Lerp(
            _curRotSpeedBorderFlipped,
            (_isAiming) ? _maxRotSpeedBorderFlipped /*10f*/ : _minRotSpeedBorderFlipped /*1f*/,
            _aimProgress);
        _borderFlipped.localRotation *= Quaternion.Euler(0f, _rotSpeedBorderFlipped, 0f);

        _rotSpeedIndicator = Mathf.Lerp(
            _curRotSpeedIndicator,
            (_isAiming) ? _maxRotSpeedIndicator /*10f*/ : _minRotSpeedIndicator /*1f*/,
            _aimProgress);
        _indicator.localRotation *= Quaternion.Euler(0f, _rotSpeedIndicator, 0f);
        #endregion

        #region Emission
        _emissionIntensity = Mathf.Lerp(
            _curEmissionIntensity,
            (_isAiming) ? _maxEmissionIntensity /*8f*/ : _minEmissionIntensity /*1f*/,
            _aimProgress);

        _borderMat.SetColor("_EmissionColor", Color.yellow * _emissionIntensity);
        _borderMat.EnableKeyword("_EMISSION");

        _borderFlippedMat.SetColor("_EmissionColor", Color.red * _emissionIntensity);
        _borderFlippedMat.EnableKeyword("_EMISSION");

        _indicatorMat.SetColor("_EmissionColor", Color.white * _emissionIntensity);
        _indicatorMat.EnableKeyword("_EMISSION");
        #endregion
    }

    private void AimReset()
    {
        if (_isReturning && _target.localPosition.z <= _minPos)
        {
            SetVisibility(0f);
            _isReturning = false;
        }
    }
    #endregion

    #endregion

    #endregion

}