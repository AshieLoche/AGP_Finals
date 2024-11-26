using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerFire : MonoBehaviour
{

    #region Attribute Declaration

    #region Component Attributes
    [Header("Components")]
    [SerializeField] private Transform _sunParent;
    [SerializeField] private Transform _targetParent;
    [SerializeField] private Transform _bulletSpawnMarker;

    private Transform _sun, _sunModel;
    private Animator _sunAnim;
    private Material _sunMat;

    private Transform _target;
    private Material _borderMat, _borderFlippedMat, _indicatorMat;
    #endregion

    #region Event Attributes
    public static UnityEvent<string, Vector3> OnSuntMovementEvent = new();
    public static UnityEvent<string, float, float, float> OnTargetMovementEvent = new();
    #endregion

    #endregion

    #region Method Definition

    #region Native Method Definition
    private void Awake()
    {
        PlayerAim.OnFireEvent.AddListener(HandleFire);
    }
    #endregion

    #region UDM (Event Handler) Definition
    private void HandleFire(Vector3 position, float scale, List<float> rotSpeeds, float emissionIntensity)
    {
        #region Sun
        _sun = SunPool.instance.GetSun().transform;

        _sun.position = _bulletSpawnMarker.position;
        _sun.localScale = Vector3.one * scale;
        _sun.localRotation = transform.localRotation;

        _sunModel = _sun.GetComponentsInChildren<Transform>()[1];
        _sunAnim = _sunModel.GetComponent<Animator>();
        _sunAnim.speed = rotSpeeds.Last();

        foreach (Renderer sunRenderer in _sunModel.GetComponentsInChildren<Renderer>())
        {
            _sunMat = sunRenderer.material;
            _sunMat.SetColor("_EmissionColor", Color.white * emissionIntensity);
            _sunMat.EnableKeyword("_EMISSION");
        }

        _sun.gameObject.SetActive(true);
        #endregion

        #region Target
        _target = TargetPool.instance.GetTarget().transform;

        _target.position = position;
        _target.localScale = Vector3.one * scale;

        foreach (Renderer child in _target.GetComponentsInChildren<Renderer>())
        {
            if (child.name == "Border")
            {
                _borderMat = child.GetComponent<Renderer>().material;
                _borderMat.SetColor("_EmissionColor", Color.yellow * emissionIntensity);
                _borderMat.EnableKeyword("_EMISSION");
            }
            else if (child.name == "Border Flipped")
            {
                _borderFlippedMat = child.GetComponent<Renderer>().material;
                _borderFlippedMat.SetColor("_EmissionColor", Color.red * emissionIntensity);
                _borderFlippedMat.EnableKeyword("_EMISSION");
            }
            else if (child.name == "Indicator")
            {
                _indicatorMat = child.GetComponent<Renderer>().material;
                _indicatorMat.SetColor("_EmissionColor", Color.white * emissionIntensity);
                _indicatorMat.EnableKeyword("_EMISSION");
            }
        }

        _target.gameObject.SetActive(true);
        #endregion

        OnSuntMovementEvent.Invoke(_sun.name, _target.position);
        OnTargetMovementEvent.Invoke(_target.name, rotSpeeds[0], rotSpeeds[1], rotSpeeds[2]);
    }
    #endregion

    #endregion

}