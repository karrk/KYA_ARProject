using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStick : MonoBehaviour
{
    [SerializeField] private RectTransform _out;
    [SerializeField] private RectTransform _center;

    private float _limitControllRange;

    private Vector2 _startInputPos;
    private Vector2 _currentInputPos;
    private Vector2 _delta;
    private readonly float MaxDelta = 5f;

    private float _minDistRate = 0.2f;
    public Action<Vector2,float> OnInputed;

    private void Start()
    {
        Manager.Instance.JoyStick = this;
        SetLimitRange();
    }

    private void SetLimitRange()
    {
        _limitControllRange = _out.sizeDelta.x/2;
    }

    private void Update()
    {
        if (Manager.Instance.PlayMode == false)
            return;

        if(Input.touchCount >= 1)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                SetJoystickPos(touch.position);
                SetLimitRange();

                _startInputPos = Vector2.zero;
            }
            else if(touch.phase == TouchPhase.Moved ||
                touch.phase == TouchPhase.Stationary)
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    _out, touch.position, null, out Vector2 tempPos);

                if (tempPos.magnitude > _limitControllRange)
                {
                    tempPos = tempPos.normalized * _limitControllRange;
                }

                _center.anchoredPosition = tempPos;

                float distRate = Mathf.Clamp(tempPos.magnitude / _limitControllRange, _minDistRate, 1);

                OnInputed?.Invoke(tempPos.normalized, distRate);

                //Debug.Log(distRate);

            }
            else if(touch.phase == TouchPhase.Ended)
            {
                SetOffJoyStick();
            }
        }
    }

    private void SetJoystickPos(Vector2 m_pos)
    {
        _out.gameObject.SetActive(true);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _out.parent.GetComponent<RectTransform>(),
            m_pos, null, out Vector2 convert);

        _out.anchoredPosition = convert;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _out,
            m_pos, null, out Vector2 convert2);

        _center.anchoredPosition = convert2;
    }

    private void SetOffJoyStick()
    {
        _out.gameObject.SetActive(false);
    }
}
