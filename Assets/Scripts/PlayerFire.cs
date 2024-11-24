using UnityEngine;

public class PlayerFire : MonoBehaviour
{

    [SerializeField] private Transform _bullerSpawnMarker;
    [SerializeField] private Transform _sunParent;
    private Transform _sun;
    private Vector3 _targetPosition;

    private void Awake()
    {
        PlayerAim.OnFireEvent.AddListener(HandleFire);
    }

    private void HandleFire(Transform sun, Vector3 position)
    {
        sun.localPosition = _bullerSpawnMarker.position;
        sun.parent = _sunParent;
        sun.localRotation = Quaternion.identity;
        _targetPosition = position;
        _sun = sun;
    }

}