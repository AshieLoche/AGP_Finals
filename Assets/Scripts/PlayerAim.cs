using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAim : MonoBehaviour
{

    #region Attribute Declaration

    #region Component Attributes

    private Transform _target, _aim, _aimFlipped, _indicator;
    private Renderer _indicatorRenderer;
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
        _curPos = _minPos;
        _curScale = _curRotSpeed = _curEmissionIntensity = 1f;

        _target = transform.GetComponentsInChildren<Transform>().FirstOrDefault(child => child.name == "Target");

        _target.GetComponentsInChildren<Transform>().ToList().ForEach(child =>
        {
            if (child.name == "Aim")
                _aim = child;
            else if (child.name == "Aim Flipped")
                _aimFlipped = child;
            else if (child.name == "Indicator")
                _indicator = child;
        });

        foreach (Transform child in transform.GetComponentsInChildren<Transform>())
        {
            if (child.name == "Target")
            {
                _target = child;
                break;
            }
        }


        foreach (Transform child in _target.GetComponentsInChildren<Transform>())
        {
            if (child.name == "Aim")
                _aim = child;
            else if (child.name == "Aim Flipped")
                _aimFlipped = child;
            else if (child.name == "Indicator")
                _indicator = child;
        }

        _indicatorRenderer = _indicator.GetComponent<Renderer>();

        _indicatorMat = _indicatorRenderer.material;
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