using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerFire : MonoBehaviour
{

    #region Attribute Declaration

    #region Component Attributes
    private Transform _bulletSpawnMarker;
    private Transform _sun, _sunModel;
    private Animator _sunAnim;

    private Transform _target;
    #endregion

    #region Boolean Attributes
    private bool _isFiring;
    #endregion

    #region Event Attributes
    public static UnityEvent<string, Vector3> OnSuntMovementEvent = new();
    public static UnityEvent<string, float, float, float> OnTargetMovementEvent = new();
    #endregion

    #endregion

    #region Method Definition

    #region Native Methods
    private void Awake()
    {
        PlayerAim.OnFireEvent.AddListener(HandleFire);
    }

    private void Start()
    {
        _bulletSpawnMarker = GetComponentsInChildren<Transform>().FirstOrDefault(child => child.name == "Bullet Spawn Marker");
    }
    #endregion

    #region User-Defined Methods

    #region UDM (Event Handler)
    private void HandleFire(Vector3 position, float scale, List<float> rotSpeeds, float emissionIntensity)
    {
        StartCoroutine(IFire(position, scale, rotSpeeds, emissionIntensity));
    }
    #endregion

    #region UDM (Fire)
    private IEnumerator IFire(Vector3 position, float scale, List<float> rotSpeeds, float emissionIntensity)
    {
        if (!_isFiring)
        {
            _isFiring = true;

            #region Sun
            _sun = SunPool.instance.GetObject("Bullet").transform;

            _sun.position = _bulletSpawnMarker.position;
            _sun.localScale = Vector3.one * scale;
            _sun.localRotation = transform.localRotation;

            _sunModel = _sun.GetComponentsInChildren<Transform>().FirstOrDefault(child => child.name == "Sun Model");
            _sunAnim = _sunModel.GetComponent<Animator>();
            _sunAnim.speed = rotSpeeds.Last();

            foreach (Renderer sunRenderer in _sunModel.GetComponentsInChildren<Renderer>())
            {
                Emission(sunRenderer.material, emissionIntensity * Color.white);
            }

            _sun.gameObject.SetActive(true);
            #endregion

            #region Target
            _target = TargetPool.instance.GetObject("Bullet").transform;

            _target.position = position;
            _target.localScale = Vector3.one * scale;

            foreach (Renderer child in _target.GetComponentsInChildren<Renderer>())
            {
                if (child.name == "Border" || child.name == "Border Flipped" || child.name == "Indicator")
                {
                    Emission(child.GetComponent<Renderer>().material, emissionIntensity * child.name switch
                    {
                        "Border" => Color.yellow,
                        "Border Flipped" => Color.red,
                        "Indicator" => Color.white,
                        _ => Color.white,
                    });
                }
            }

            _target.gameObject.SetActive(true);
            #endregion

            #region Events
            OnSuntMovementEvent.Invoke(_sun.name, _target.position);
            OnTargetMovementEvent.Invoke(_target.name, rotSpeeds[0], rotSpeeds[1], rotSpeeds[2]);
            #endregion

            yield return new WaitForSeconds(0.5f);

            _isFiring = false;
        }
    }

    private void Emission(Material mat, Color color)
    {
        mat.SetColor("_EmissionColor", color);
        mat.EnableKeyword("_EMISSION");
    }
    #endregion

    #endregion

    #endregion

}