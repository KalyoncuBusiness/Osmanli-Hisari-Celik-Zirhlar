using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    private Slider _slider;
    private Camera _camera;

    [SerializeField] private Transform _target;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _camera = Camera.main;
    }

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        _slider.value = currentValue / maxValue;
    }

    private void Update()
    {
        transform.rotation = _camera.transform.rotation;
        transform.position = _target.position;
    }
}
