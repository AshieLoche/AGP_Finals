using UnityEngine;

public class TargetMovement : MonoBehaviour
{

    #region Attribute Declaration

    #region Component Attributes
    private Transform _border, _borderFlipped, _indicator;
    #endregion

    #region Movement Attributes
    private float _rotSpeedBorder, _rotSpeedBorderFlipped, _rotSpeedIndicator;
    #endregion

    #endregion

    #region Method Definition

    #region Native Methods
    private void Awake()
    {
        PlayerFire.OnTargetMovementEvent.AddListener(HandleMovement);
    }

    private void Start()
    {
        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            if (child.name == "Border")
                _border = child;
            else if (child.name == "Border Flipped")
                _borderFlipped = child;
            else if (child.name == "Indicator")
                _indicator = child;
        }
    }

    private void FixedUpdate()
    {
        _border.localRotation *= Quaternion.Euler(0f, _rotSpeedBorder, 0f);
        _borderFlipped.localRotation *= Quaternion.Euler(0f, _rotSpeedBorderFlipped, 0f);
        _indicator.localRotation *= Quaternion.Euler(0f, _rotSpeedIndicator, 0f);
    }
    #endregion

    #region User-Defined Methods

    #region UDM (Event Handler)
    private void HandleMovement(string targetName, float rotSpeedBorder, float rotSpeedBorderFlipped, float rotSpeedIndicator)
    {
        if (name == targetName)
        {
            _rotSpeedBorder = rotSpeedBorder;
            _rotSpeedBorderFlipped = rotSpeedBorderFlipped;
            _rotSpeedIndicator = rotSpeedIndicator;
        }
    }
    #endregion

    #endregion

    #endregion

}