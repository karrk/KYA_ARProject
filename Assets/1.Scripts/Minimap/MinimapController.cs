using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.Controls.AxisControl;

public class MinimapController : MonoBehaviour
{
    private Vector3 _viewPoint;
    private Vector3 _clampViewPoint;
    private Vector2 _standardBLPos;
    private Vector2 _standardTRPos;
    private Vector3 _tempPos;
    [SerializeField] private float _offset = 350f;

    [SerializeField] private RectTransform _rt;
    [SerializeField] private RectTransform _tailRt;

    private void Start()
    {
        CanvasScaler canvas = transform.parent.GetComponent<CanvasScaler>();
        Vector2 canvasSize = canvas.referenceResolution;

        _standardTRPos = new Vector2(canvasSize.x / 2, canvasSize.y / 2) + Vector2.one * (_offset * -1);
        _standardBLPos = -1 * _standardTRPos;
    }

    private void Update()
    {
        _viewPoint = Manager.Instance.Data.GroundViewpoint;

        _clampViewPoint.x = Mathf.Clamp(_viewPoint.x, 0, 1);
        _clampViewPoint.y = Mathf.Clamp(_viewPoint.y, 0, 1);

        _tempPos.x = Mathf.Lerp(_standardBLPos.x, _standardTRPos.x, _clampViewPoint.x);
        _tempPos.y = Mathf.Lerp(_standardBLPos.y, _standardTRPos.y, _clampViewPoint.y);

        _rt.localPosition = _tempPos;
        _tailRt.localEulerAngles = Vector3.forward * CalculateAngle(_viewPoint.x, _viewPoint.y);
    }

    private float CalculateAngle(float m_xRate, float m_yRate)
    {
        Vector2 vec = new Vector2(m_xRate, m_yRate);

        float angle = Vector2.Angle(Vector2.down, vec.normalized);

        if (m_xRate < 0)
            angle = 360 - angle;

        return angle;
    }
}
