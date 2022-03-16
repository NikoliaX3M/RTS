using System;
using UnityEngine;

[RequireComponent(typeof(UnitsMovement))]
public class Selector : MonoBehaviour
{
    [SerializeField] private float _clickDuration;
    [SerializeField] private Texture _rectGUI;

    private float _totalDownTime;
    private bool _dragMouse;

    private Vector3 _startPoint, _endPoint;
    private UnitsMovement _unitsMovement;

    private void Start()
    {
        _unitsMovement = gameObject.GetComponent<UnitsMovement>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startPoint = Input.mousePosition;            
            _dragMouse = true;
            _totalDownTime = 0;
        }

        if ( Input.GetMouseButton(0))
        {
            _endPoint = Input.mousePosition;
            _dragMouse = _totalDownTime >= _clickDuration;
            _totalDownTime += Time.deltaTime;

            if(_dragMouse)
                _unitsMovement.SelectedUnits.Clear();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_dragMouse)
                Select();

            _dragMouse = false;
        }
    }

    private void OnGUI()
    {
        if (_dragMouse)
        {
            Func<float, float> inverY = y => Screen.height - y;

            float width = _endPoint.x - _startPoint.x;
            float height = inverY(_endPoint.y) - inverY(_startPoint.y);
            Rect selectRectGUI = new(_startPoint.x, inverY(_startPoint.y), width, height);
            GUI.DrawTexture(selectRectGUI, _rectGUI, ScaleMode.StretchToFill, true);
        }
    }

    private void Select()
    {
        if (_endPoint.x < _startPoint.x)
            (_startPoint.x, _endPoint.x) = (_endPoint.x, _startPoint.x);

        if (_endPoint.y > _startPoint.y)
            (_startPoint.y, _endPoint.y) = (_endPoint.y, _startPoint.y);

        Bounds viewportBounds = GetViewportBounds(_startPoint, _endPoint);

        for (int i = 0; i < _unitsMovement.Units.Count; i++)
        {
            if (viewportBounds.Contains(Camera.main.WorldToViewportPoint(_unitsMovement.Units[i].GameObjectUnit.transform.position)))
                _unitsMovement.SelectedUnits.Add(_unitsMovement.Units[i]);
        }
    }

    private Bounds GetViewportBounds(Vector3 startPoint, Vector3 endPoint)
    {
        Vector3 firstVector = Camera.main.ScreenToViewportPoint(startPoint);
        Vector3 secondVector = Camera.main.ScreenToViewportPoint(endPoint);

        Vector3 min = Vector3.Min(firstVector, secondVector);
        Vector3 max = Vector3.Max(firstVector, secondVector);

        min.z = Camera.main.nearClipPlane;
        max.z = Camera.main.farClipPlane;

        Bounds bounds = new();
        bounds.SetMinMax(min, max);
        return bounds;
    }
}