using UnityEngine;
using UnityEngine.UI;

public class UIAnimatedBar : UIBar
{
    [SerializeField] private Image _image;
    [SerializeField] private float _animationTime;

    private bool _isAnimating = false;
    private float _currentValue;
    private float _step;

    private void Update()
    {
        if (_isAnimating)
        {
            SetFilness();
        }
    }

    public override void Init(float currentValue)
    {
        _currentValue = currentValue;
        _image.fillAmount = _currentValue;
    }

    public override void ChangeValues(float value)
    {
        _currentValue = Mathf.Clamp01(value);
        float distance = CalculateDistance(_image.fillAmount, _currentValue);
        _step = CalculateStep(distance, _animationTime);
        _isAnimating = true;
    }

    private void SetFilness()
    {
        _image.fillAmount = Mathf.Clamp01(Mathf.MoveTowards(_image.fillAmount, _currentValue, _step * Time.deltaTime));

        if (_image.fillAmount == _currentValue)
        {
            _isAnimating = false;
        }
    }

    private float CalculateDistance(float current, float target)
    {
        return Mathf.Abs(current - target);
    }

    private float CalculateStep(float distance, float animationTime)
    {
        if(animationTime <= 0)
        {
            return 0;
        }

        return distance / animationTime;
    }
}