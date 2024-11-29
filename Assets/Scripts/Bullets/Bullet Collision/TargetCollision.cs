using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class TargetCollision : MonoBehaviour
{

    private Transform _indicator;
    private SphereCollider _indicatorCollider;
    private Material _borderMat, _borderFlippedMat, _indicatorMat;

    private void Awake()
    {
        SunCollision.OnSunCollision.AddListener(HandleSunCollision);
    }
    private void Start()
    {
        foreach (Renderer child in GetComponentsInChildren<Renderer>())
        {
            if (child.name == "Border")
                _borderMat = child.material;
            else if (child.name == "Border Flipped")
                _borderFlippedMat = child.material;
            else if (child.name == "Indicator")
                _indicatorMat = child.material;
        }

        _indicator = GetComponentsInChildren<Transform>().FirstOrDefault(child => child.name == "Indicator");
        _indicatorCollider = _indicator.GetComponent<SphereCollider>();
    }

    private void HandleSunCollision(string targetName)
    {
        if (name == targetName)
        {
            StartCoroutine(IDeactivate(0.5f));
        }
    }

    private void SetVisibility(float visibility)
    {
        SetVisibility("Border", _borderMat.color, visibility);
        SetVisibility("Border Flipped", _borderFlippedMat.color, visibility);
        SetVisibility("Indicator", _indicatorMat.color, visibility);
    }

    private void SetVisibility(string mat, Color matColor, float visibility)
    {
        matColor.a = visibility;

        if (mat == "Border")
            _borderMat.color = matColor;
        else if (mat == "Border Flipped")
            _borderFlippedMat.color = matColor;
        else if (mat == "Indicator")
            _indicatorMat.color = matColor;
    }

    private IEnumerator IDeactivate(float delay)
    {
        SetVisibility(0f);
        _indicatorCollider.enabled = true;
        yield return new WaitForSeconds(delay);
        _indicatorCollider.enabled = false;
        SetVisibility(1f);
        gameObject.SetActive(false);
    }

}