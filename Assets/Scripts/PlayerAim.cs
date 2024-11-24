using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAim : MonoBehaviour
{

    #region Attribute Declaration

    #region Component Attributes
    [Header("Components")]
    [SerializeField] private Transform _sun;
    [SerializeField] private Transform _aim;
    [SerializeField] private Renderer _indicator;
    #endregion

    #region Aim Attributes
    [Header("Aim Time")]
    [SerializeField] private float _aimTime;

    private float _aimTimer;
    private float _aimProgress;

    [Header("Aim Position")]
    [SerializeField] private float _minPos;
    [SerializeField] private float _maxPos;

    private Vector3 _curPos;

    [Header("Aim Scale")]
    [SerializeField] private float _minScale;
    [SerializeField] private float _maxScale;
    
    private Vector3 _curScale;

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
    public static UnityEvent<Transform, Vector3> OnFireEvent = new();
    #endregion

    #endregion

    #region Method Definition

    #region Native Method Definition
    private void Awake()
    {
        InputReader.OnAimEvent.AddListener(HandleAim);
        InputReader.OnAimCancelEvent.AddListener(HandleAimCancel);
        InputReader.OnFireEvent.AddListener(HandleFire);
    }

    private void Start()
    {
        _curPos = new Vector3(0f, 0.5f, _minPos /*150f*/);
        _curScale = Vector3.one;
        _curRotSpeed = _curEmissionIntensity = 1f;
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
        OnFireEvent.Invoke(_sun, _aim.position);
        Debug.Log("Yippie");
    }
    #endregion

    #region UDM (Aim) Definition
    private void SetAim(bool status)
    {
        _aimTimer = 0f;
        _aimProgress = 0f;
        _curPos = _aim.localPosition;
        _curScale = _aim.localScale;
        _curRotSpeed = _rotSpeed;
        _curEmissionIntensity = _emissionIntensity;
        _isAiming = status;
    }

    private void Aim()
    {
        _aimTimer += Time.deltaTime;
        _aimProgress = Mathf.Clamp01(_aimTimer / _aimTime);

        _aim.localPosition = Vector3.Lerp(
            _curPos,
            new Vector3(0f, 0.5f, (_isAiming) ? _maxPos /*450f*/ : _minPos /*150f*/),
            _aimProgress);

        _aim.localScale = Vector3.Lerp(
            _curScale,
            Vector3.one * ((_isAiming) ? _minScale /*0.25f*/ : _maxScale /*1f*/),
            _aimProgress);

        _rotSpeed = Mathf.Lerp(
            _curRotSpeed,
            (_isAiming) ? _maxRotSpeed /*10f*/ : _minRotSpeed /*1f*/,
            _aimProgress);
        _aim.localRotation *= Quaternion.Euler(0f, 0f, _rotSpeed);
        _sun.GetChild(0).rotation = Quaternion.identity;
        _sun.GetChild(0).GetComponent<Animator>().speed = _rotSpeed;

        _emissionIntensity = Mathf.Lerp(
            _curEmissionIntensity,
            (_isAiming) ? _maxEmissionIntensity /*8f*/ : _minEmissionIntensity /*1f*/,
            _aimProgress);
        _indicator.material.SetColor("_EmissionColor", Color.white * _emissionIntensity);
        _indicator.material.EnableKeyword("_EMISSION");
        _sun.GetChild(0).GetComponentsInChildren<Renderer>().ToList().ForEach(renderer =>
        {
            renderer.material.SetColor("_EmissionColor", Color.white * _emissionIntensity);
            renderer.material.EnableKeyword("_EMISSION");
        });
    }
    #endregion

    #endregion

    #endregion

}