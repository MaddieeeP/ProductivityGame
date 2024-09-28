using UnityEngine;
using UnityEngine.UI;

public class UIElementAnimator : MonoBehaviour
{
    [SerializeField] private Image _image;
    private float _currentTime;
    private float _currentDuration = 1f;
    private Color _prevColor;
    private Color _nextColor;
    private Vector3 _restPosition;
    private Vector3 _prevPosition;
    private Vector3 _nextPosition;
    private Vector3 _prevScale;
    private Vector3 _nextScale;
    private float _minBobDuration;
    private float _maxBobDuration;
    private float _maxBobMovement;
    private AnimationCurve _currentCurve;
    private AnimationCurve _nextCurve;

    public void Awake()
    {
        _currentTime = 0f;
        _prevColor = _image.color;
        _nextColor = _image.color;
        _restPosition = _image.transform.position;
        _prevPosition = _image.transform.position;
        _nextPosition = _image.transform.position;
        _prevScale = _image.transform.localScale;
        _nextScale = _image.transform.localScale;

        //NextState(new Vector3(0f, 0f, 0f), new Vector3(1000f, 1000f, 1000f), Color.magenta, AnimationCurve.EaseInOut(0f, 0f, 1f, 1f), 10f, 100f, AnimationCurve.EaseInOut(0f, 0f, 1f, 1f), 0.5f, 3f);
    }

    public void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= _currentDuration) 
        {
            _prevPosition = _nextPosition;
            _prevScale = _nextScale;
            _prevColor = _nextColor;

            _nextPosition = _restPosition + _maxBobMovement * Random.insideUnitSphere;
            _currentTime = 0f;
            _currentDuration = Random.Range(_minBobDuration, _maxBobDuration);

            _currentCurve = _nextCurve;
        }

        float t = _currentCurve.Evaluate(_currentTime / _currentDuration);

        _image.transform.position = Vector3.Lerp(_prevPosition, _nextPosition, t);
        _image.transform.localScale = Vector3.Lerp(_prevScale, _nextScale, t);
        _image.color = Color.LerpUnclamped(_prevColor, _nextColor, t);
    }

    public void NextState(Vector3 position, Vector3 scale, Color color, AnimationCurve moveCurve, float lerpDuration, float maxBobMovement, AnimationCurve bobCurve, float minBobDuration, float maxBobDuration)
    {
        _prevPosition = _image.transform.position;
        _prevScale = _image.transform.localScale;
        _prevColor = _image.color;
        
        _nextPosition = position;
        _nextScale = scale;
        _nextColor = color;

        _currentCurve = moveCurve;
        _nextCurve = bobCurve;

        _minBobDuration = minBobDuration;
        _maxBobDuration = maxBobDuration;
        _maxBobMovement = maxBobMovement;
    }
}