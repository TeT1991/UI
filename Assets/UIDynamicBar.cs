using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIDynamicBar : UIBar
{
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private Image _mainImage;
    [SerializeField] private Image _extraImage;
    [SerializeField] private Color _decreaseColor;
    [SerializeField] private Color _increaseColor;
    [SerializeField] private float _delay;
    [SerializeField] private float _animationTime;

    private Image _animatedImage;
    private Image _staticImage;
    bool _isEncreasing;
    private float _step;
    private float _passedAnimationTime = 0;

    private Coroutine _coroutine;

    public override void Init(float currentValue)
    {
        CurrentValue = currentValue;
        _animatedImage = _extraImage;
        _mainImage.fillAmount = CurrentValue;
        _extraImage.fillAmount = CurrentValue;
    }

    public override void ChangeValues(float value)
    {
        SetDirection(value);

        CurrentValue = Mathf.Clamp01(value);
        float distance = CalculateDistance(_animatedImage.fillAmount, CurrentValue);
        _step = CalculateStep(distance, _animationTime);

        ApplyActionsByDirection();

        _passedAnimationTime = 0f;

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _staticImage.fillAmount = CurrentValue;
        _coroutine = StartCoroutine(DelayAnimation());
    }

    private IEnumerator DelayAnimation()
    {
        yield return new WaitForSeconds(_delay);

        float start = _animatedImage.fillAmount;
        float end = CurrentValue;
        float time = 0;

        while (time < _animationTime)
        {
            time += Time.deltaTime;
            float passedTime = Mathf.Clamp01(time / _animationTime);

            float curved = _curve.Evaluate(passedTime);
            float step = Mathf.MoveTowards(start, end, Mathf.Abs(end - start) * curved);

            _animatedImage.fillAmount = step;

            yield return null;
        }

        _animatedImage.fillAmount = end;
    }

    private void ApplyActionsByDirection()
    {
        if (_isEncreasing)
        {
            _animatedImage = _mainImage;
            _staticImage = _extraImage;
            _extraImage.color = _increaseColor;
        }
        else
        {
            _animatedImage = _extraImage;
            _staticImage = _mainImage;
            _extraImage.color = _decreaseColor;
        }
    }

    private void SetDirection(float value)
    {
        _isEncreasing = value >= CurrentValue;
    }

    private float CalculateDistance(float current, float target)
    {
        return Mathf.Abs(current - target);
    }

    private float CalculateStep(float distance, float animationTime)
    {
        if (animationTime <= 0)
        {
            return 0;
        }

        return distance / animationTime;
    }
}